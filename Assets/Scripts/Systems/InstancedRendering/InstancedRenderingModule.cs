using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Systems/InstancedRendering/IRender Module")]
public class InstancedRenderingModule : Module
{

    public struct IMeshData : IEquatable<IMeshData>
    {
        public Mesh mesh;
        public int subMeshIndex;
        public Material material;
        public bool recieveShadows;
        public int layer;

        public IMeshData(Mesh mesh, int subMeshIndex, Material material, bool recieveShadows, int layer)
        {
            this.mesh = mesh;
            this.subMeshIndex = subMeshIndex;
            this.material = material;
            this.recieveShadows = recieveShadows;
            this.layer = layer;
        }

        public bool Equals(IMeshData other)
        {
            return (mesh == other.mesh) &&
                   subMeshIndex == other.subMeshIndex &&
                   (material == other.material) &&
                   recieveShadows == other.recieveShadows &&
                   layer == other.layer;
        }

        public override int GetHashCode()
        {
            int hashCode = 233193393;
            hashCode = hashCode * -1521134295 + mesh.GetHashCode();
            hashCode = hashCode * -1521134295 + subMeshIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + material.GetHashCode();
            hashCode = hashCode * -1521134295 + recieveShadows.GetHashCode();
            hashCode = hashCode * -1521134295 + layer.GetHashCode();
            return hashCode;
        }
    }
    
    public struct IRenderData
    {
        public List<GameObject> gameObjects;
        public ComputeBuffer TransformBuffer;

        public IRenderData(List<GameObject> GOS, ComputeBuffer transformBuffer)
        {
            gameObjects = GOS;
            TransformBuffer = transformBuffer;
        }
        public IRenderData(List<GameObject> GOS)
        {
            gameObjects = GOS;
            TransformBuffer = null;
        }
    }
    
    public struct CBufferPhysicsData
    {
        public Matrix4x4 TransformMatrix;
        public Vector3 VelocityVector;
        public Vector3 AngVelocityVector;

        public CBufferPhysicsData(Matrix4x4 transformMatrix, Vector3 velocityVector, Vector3 angVelocityVector)
        {
            TransformMatrix = transformMatrix;
            VelocityVector = velocityVector;
            AngVelocityVector = angVelocityVector;
        }
    }
    
    private Dictionary<IMeshData,IRenderData> RenderData = new Dictionary<IMeshData,IRenderData>();

    private Dictionary<IMeshData,List<Matrix4x4>> ObjectData = new Dictionary<IMeshData, List<Matrix4x4>>();


    private int LastFramecount = 0;//draw once per frame

    public static IMeshData GenerateIMeshData(GameObject Owner,Mesh mesh,MeshRenderer meshRenderer)
    {
        IMeshData temp = new IMeshData(mesh,meshRenderer.subMeshStartIndex,meshRenderer.sharedMaterial,false,Owner.layer);
        return temp;
    }

    public override void Initialize()
    {
        Reset();
    }

    private void UpdateTransformMaxtrices(IMeshData meshData)
    {
        ObjectData[meshData].Clear();
        GameObject GO;
        for (int i = 0; i < RenderData[meshData].gameObjects.Count; i++)
        {
            GO = RenderData[meshData].gameObjects[i];
            if (GO.activeSelf)
            {
                ObjectData[meshData].Add((Matrix4x4.TRS(GO.transform.position,GO.transform.rotation,GO.transform.lossyScale)));
            }
        }

    }

    public void UpdateTransforms()
    {
        foreach (var Data in RenderData)
        {
            UpdateTransformMaxtrices(Data.Key);
        }
    }



    public override void Update()
    {
        if (LastFramecount != Time.frameCount)
        {
        
        //Debug.Log(RenderData.Count);
        foreach (var Data in RenderData)
        {
            UpdateTransformMaxtrices(Data.Key);
            //foreach (var GO in RenderData[Data.Key])
            //{
            //    Debug.Log("Drawing: "+ GO);
            //}


            Graphics.DrawMeshInstanced(
                Data.Key.mesh,
                Data.Key.subMeshIndex,
                Data.Key.material,
                ObjectData[Data.Key],
                new MaterialPropertyBlock(),
                ShadowCastingMode.Off,
                Data.Key.recieveShadows,
                Data.Key.layer
                );
        }
        LastFramecount = Time.frameCount;    
        }



    }



    public void AddInstancedMesh(GameObject Owner,IMeshData MeshData)
    {
        bool foundData = false;
        Matrix4x4 posMatrix;
        Rigidbody OwnerRB = Owner.GetComponentInParent<Rigidbody>();
        foreach (var item in RenderData.ToList())
        {

            posMatrix = Matrix4x4.TRS(Owner.transform.position,Owner.transform.rotation,Owner.transform.localScale);
            if (item.Key.mesh == (MeshData.mesh))
            {
                RenderData[item.Key].gameObjects.Add(Owner);

                ObjectData[item.Key].Add(posMatrix);
                RenderData[item.Key] = new IRenderData(item.Value.gameObjects);
                foundData = true;
            }
        }
        if (!foundData) // this needs to stay outside of the for loop, it initializes the render data if it's not present
            {
                posMatrix = Matrix4x4.TRS(Owner.transform.position,Owner.transform.rotation,Owner.transform.localScale);
                List<GameObject> temp = new List<GameObject>();
                ObjectData.Add(MeshData,new List<Matrix4x4>());
                ObjectData[MeshData].Add(Matrix4x4.TRS(Owner.transform.position,Owner.transform.rotation,Owner.transform.lossyScale));
                temp.Add(Owner);
                RenderData.Add(MeshData,new IRenderData(temp));
            }
        
    }







    public override void Reset()
    {
        RenderData.Clear();
        ObjectData.Clear();
    }
}

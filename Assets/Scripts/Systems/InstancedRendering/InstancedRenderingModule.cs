using System;
using System.Collections;
using System.Collections.Generic;
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
    private Dictionary<IMeshData,HashSet<GameObject>> RenderData = new Dictionary<IMeshData, HashSet<GameObject>>();
    private List<Matrix4x4> TempTransformData = new List<Matrix4x4>();


    private int LastFramecount = 0;

    public static IMeshData GenerateIMeshData(GameObject Owner,Mesh mesh,MeshRenderer meshRenderer)
    {
        IMeshData temp = new IMeshData(mesh,meshRenderer.subMeshStartIndex,meshRenderer.material,false,Owner.layer);
        return temp;
    }

    public override void Initialize()
    {
        Reset();
    }


    private void UpdateTransformMaxtrices(IMeshData meshData)
    {
        TempTransformData.Clear();
        foreach (var GO in RenderData[meshData])
        {
            Debug.Log("Drawing: "+ GO);
            if (GO.activeSelf)
            {
                TempTransformData.Add(Matrix4x4.TRS(GO.transform.position,GO.transform.rotation,GO.transform.lossyScale));
            }
        }

    }





    public override void Update()
    {
        if (LastFramecount != Time.frameCount)
        {
        
        Debug.Log(RenderData.Count);
        foreach (var Data in RenderData)
        {
            UpdateTransformMaxtrices(Data.Key);
            Debug.Log(TempTransformData.Count);
            Graphics.DrawMeshInstanced(
                Data.Key.mesh,
                Data.Key.subMeshIndex,
                Data.Key.material,
                TempTransformData,
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
        Debug.Log("AddingInstance");
        foreach (var item in RenderData)
        {
            if (item.Key.mesh == (MeshData.mesh))
            {
                RenderData[item.Key].Add(Owner);
                foundData = true;
            }
        }
        if (!foundData)
        {
            HashSet<GameObject> temp = new HashSet<GameObject>();
            temp.Add(Owner);
            RenderData.Add(MeshData,temp);
        }
    }







    public override void Reset()
    {
        RenderData.Clear();
        TempTransformData.Clear();
    }
}

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
    }

    public struct IRenderData
    {
        public List<Transform> transforms;

        public MaterialPropertyBlock RenderProperties;

        public IRenderData(List<Transform> t)
        {
            transforms = t;
            RenderProperties = new MaterialPropertyBlock();
        }
        public IRenderData(List<Transform> t, MaterialPropertyBlock MPB)
        {
            transforms = t;
            RenderProperties = MPB;
        }
    }





    private Dictionary<IMeshData, IRenderData> RenderData = new Dictionary<IMeshData, IRenderData>();
    private Dictionary<IMeshData, List<Matrix4x4>> ObjectData = new Dictionary<IMeshData, List<Matrix4x4>>();



    private int LastFramecount = 0;//draw once per frame

    public static IMeshData GenerateIMeshData(GameObject Owner, Mesh mesh, MeshRenderer meshRenderer)
    {
        IMeshData temp = new IMeshData(mesh, meshRenderer.subMeshStartIndex, meshRenderer.material, false, Owner.layer);
        return temp;
    }

    public override void Initialize()
    {
        Reset();
    }


    public override void Update()
    {
        if (LastFramecount != Time.frameCount)
        {

            //Debug.Log(RenderData.Count);
            foreach (var Data in RenderData)
            {
                //foreach (var GO in RenderData[Data.Key])
                //{
                //    Debug.Log("Drawing: "+ GO);
                //}



                Graphics.DrawMeshInstanced(
                    Data.Key.mesh,
                    Data.Key.subMeshIndex,
                    Data.Key.material,
                    ObjectData[Data.Key],
                    Data.Value.RenderProperties,
                    ShadowCastingMode.Off,
                    Data.Key.recieveShadows,
                    Data.Key.layer
                    );
            }
            LastFramecount = Time.frameCount;
        }
    }



    public void AddInstancedMesh(GameObject Owner, IMeshData MeshData)
    {
        for (int i = 0; i < RenderData.Count; i++)
        {
            











        }





    }







    public override void Reset()
    {
        RenderData.Clear();
        ObjectData.Clear();
    }
}

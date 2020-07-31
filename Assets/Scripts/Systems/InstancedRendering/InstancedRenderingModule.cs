using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Systems/InstancedRendering/IRender Module")]
public class InstancedRenderingModule : Module
{

    public struct IMeshData
    {
        public Mesh mesh;
        public int subMeshIndex;
        public Material material;
        public MaterialPropertyBlock properties;
        public bool recieveShadows;
        public int layer;

        public IMeshData(Mesh mesh, int subMeshIndex, Material material, MaterialPropertyBlock properties, bool recieveShadows, int layer)
        {
            this.mesh = mesh;
            this.subMeshIndex = subMeshIndex;
            this.material = material;
            this.properties = properties;
            this.recieveShadows = recieveShadows;
            this.layer = layer;
        }

        public override bool Equals(object obj) //not sure how well this works;
        {
            return obj is IMeshData data &&
                   EqualityComparer<Mesh>.Default.Equals(mesh, data.mesh) &&
                   subMeshIndex == data.subMeshIndex &&
                   EqualityComparer<Material>.Default.Equals(material, data.material) &&
                   EqualityComparer<MaterialPropertyBlock>.Default.Equals(properties, data.properties) &&
                   recieveShadows == data.recieveShadows &&
                   layer == data.layer;
        }

        public override int GetHashCode()
        {
            int hashCode = 233193393;
            hashCode = hashCode * -1521134295 + EqualityComparer<Mesh>.Default.GetHashCode(mesh);
            hashCode = hashCode * -1521134295 + subMeshIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Material>.Default.GetHashCode(material);
            hashCode = hashCode * -1521134295 + EqualityComparer<MaterialPropertyBlock>.Default.GetHashCode(properties);
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
        IMeshData temp = new IMeshData(mesh,meshRenderer.subMeshStartIndex,meshRenderer.material,new MaterialPropertyBlock(),false,Owner.layer);
        return temp;
        throw new System.Exception(Owner+": Mesh Data could not be generated!");
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
        Debug.Log("Frame");
        

        foreach (var Data in RenderData)
        {
            UpdateTransformMaxtrices(Data.Key);
            Graphics.DrawMeshInstanced(
                Data.Key.mesh,
                Data.Key.subMeshIndex,
                Data.Key.material,
                TempTransformData,
                Data.Key.properties,
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
        foreach (var item in RenderData)
        {
            if (item.Key.Equals(MeshData))
            {
                RenderData[item.Key].Add(Owner);
                foundData = true;
                break;
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

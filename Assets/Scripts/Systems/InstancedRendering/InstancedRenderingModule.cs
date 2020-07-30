using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public IMeshData(bool active, Mesh mesh, int subMeshIndex, Material material, MaterialPropertyBlock properties, bool recieveShadows, int layer)
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

            }



    }







    public override void Reset()
    {
        
    }
}

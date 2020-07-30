using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedRenderingModule : Module
{

    struct IMeshData
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

      /*  public override bool Equals(object obj) //not sure how well this works;
        {
            return obj is IMeshData data &&
                   active == data.active &&
                   EqualityComparer<Mesh>.Default.Equals(mesh, data.mesh) &&
                   subMeshIndex == data.subMeshIndex &&
                   EqualityComparer<Material>.Default.Equals(material, data.material) &&
                   EqualityComparer<MaterialPropertyBlock>.Default.Equals(properties, data.properties) &&
                   recieveShadows == data.recieveShadows &&
                   layer == data.layer;
        }*/
    }

    struct IRenderData
    {
        



    }


    private Dictionary<IMeshData,HashSet<GameObject>> RenderData = new Dictionary<IMeshData, HashSet<GameObject>>();

    private Dictionary<GameObject,Matrix4x4 matrix> RenderData = new Dictionary<GameObject,Matrix4x4 matrix>();








    public override void Reset()
    {
        
    }
}

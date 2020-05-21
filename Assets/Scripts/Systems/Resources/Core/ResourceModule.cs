using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Resources/ResourceModule")]
public class ResourceModule : Module
{
    [SerializeField] private List<Resource> ActiveResources = new List<Resource>();

    public delegate void ResourceEvent(ResourceInventory Caller);

    public delegate void ResourceEventDelta(ResourceInventory Caller);

    [System.Serializable]
    public struct ResourceData
    {
        public Resource resource;
        public float min;
        public float max;
        public float value;

        public ResourceData(Resource rs,float mn,float mx, float val)
        {
            resource = rs;
            min = mn;
            max = mx;
            value = val;
        }
    }



    public void CreateResourceInstance(ResourceData resourceData, ResourceInventory owner)
    {

        resourceData.resource.RegisterInstance(resourceData,owner);
    }


    public void CreateResourceInstance(Resource resource, ResourceInventory owner)
    {
        foreach (var EnabledResource in ActiveResources)
        {
            if (EnabledResource == resource)
            {
                resource.RegisterInstance(owner);
                return;
            }
        }
    }

    public override void Initialize()
    {
       
    }

    public override void Update()
    {

    }

    public override void Reset()
    {
        foreach (var ResourceObj in ActiveResources) //this will clear all the instances: TODO: possibly implement new method on modules to free memory (if needed)
        {
            ResourceObj.Reset();
        }
    }

}

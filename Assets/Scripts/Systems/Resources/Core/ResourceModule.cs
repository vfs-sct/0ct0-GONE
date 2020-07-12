//Copyright Jesse Rougeau, 2020 ©


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Resources/ResourceModule")]
public class ResourceModule : Module
{
    [SerializeField] private List<Resource> ActiveResources = new List<Resource>();

    public delegate void ResourceEvent(ResourceInventory Caller);

    public delegate void ResourceEventDelta(Resource resource,float amount);

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


    //Add a new resource type to the inventory (helper for when passing overides)
    public void CreateResourceInstance(ResourceData resourceData, ResourceInventory owner)
    {

        resourceData.resource.RegisterInstance(resourceData,owner);
    }

    public void RegisterOnAddDelegate(Resource resource, ResourceInventory owner, ResourceEventDelta newDelegate)
    {
        resource.RegisterOnAddDelegate(owner,newDelegate);
    }

    public void MoveInventory( ResourceInventory OldInv, ResourceInventory NewInv)
    {
        for (int i = 0; i < OldInv.Resources.Count; i++)
        {
            OldInv.Resources[i].MoveInventory(OldInv,NewInv);
        }
    }

    public void RegisterOnRemoveDelegate(Resource resource, ResourceInventory owner, ResourceEventDelta newDelegate)
    {
        resource.RegisterOnRemoveDelegate(owner,newDelegate);
    }


    public void CreateResourceInstance(Resource resource, ResourceInventory owner)
    {
        foreach (var EnabledResource in ActiveResources)
        {
            if (EnabledResource == resource)
            {
                resource.RegisterInstance(owner);
                return;
            } else
            {
                Debug.LogError("Resource not in active list");
            }
        }
    }

    public ResourceInventory GetInventory(ResourceInventory inventory)
    {
        return inventory;
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

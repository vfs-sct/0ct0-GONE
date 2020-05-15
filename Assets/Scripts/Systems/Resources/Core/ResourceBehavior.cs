using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehavior : MonoBehaviour
{
    [SerializeField] private ResourceModule ResourceManager = null;
    
    [SerializeField] private List<ResourceModule.ResourceData> ActiveResources = new List<ResourceModule.ResourceData>();



    void Start()
    {
        foreach (var resource in ActiveResources)
        {
            ResourceManager.CreateResourceInstance(resource,this);
        }
    }

    public void RemoveResource(Resource resource, float amount)
    {
        resource.SubInstanceValue(this,amount);
    }

    public void AddResource(Resource resource, float amount)
    {
        resource.AddInstanceValue(this,amount);
    }

    public void SetResource(Resource resource, float amount)
    {
        resource.SetInstanceValue(this,amount);
    }

    public float GetResource(Resource resource)
    {
        return resource.GetInstanceValue(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()//possibly implement cleanup in this function (May create race condition/coupling issues)
    {
        
    }
}


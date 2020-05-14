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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()//possibly implement cleanup in this function (May create race condition/coupling issues)
    {
        
    }
}


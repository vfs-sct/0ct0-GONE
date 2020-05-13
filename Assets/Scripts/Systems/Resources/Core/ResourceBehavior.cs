using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehavior : MonoBehaviour
{
    
    [SerializeField] private List<Resource.ResourceData> Resources = new List<Resource.ResourceData>();

    private Resource.ResourceEvent OnResourceDeplete;
    private Resource.ResourceEvent OnResourceFilled;
    private Resource.ResourceEventDelta OnResourceDelta;

    public void AddOnResourceDepleteEvent(Resource.ResourceEvent  newDelegate)
    {
        OnResourceDeplete += newDelegate;
    }

    public void AddOnResourceFilledEvent(Resource.ResourceEvent  newDelegate)
    {
        OnResourceFilled += newDelegate;
    }

    public void AddOnResourceDeltaeEvent(Resource.ResourceEventDelta  newDelegate)
    {
        OnResourceDelta += newDelegate;
    }


    public void Add(int ResourceIndex,float AmountToAdd)
    {
        if (Resources[ResourceIndex].Amount + AmountToAdd >= Resources[ResourceIndex].Max)
        {
            if (Resources[ResourceIndex].Amount != Resources[ResourceIndex].Max)
            {
                Resources[ResourceIndex].AddAmount(Resources[ResourceIndex].Max); 
                OnResourceFilled(this);
            }
        }
        else
        {
            Resources[ResourceIndex].AddAmount(AmountToAdd);
            OnResourceDelta(this, AmountToAdd);
        }
    }


    public void Remove(int ResourceIndex,float AmountToAdd)
    {
        if (Resources[ResourceIndex].Amount - AmountToAdd <= Resources[ResourceIndex].Min)
        {
            if (Resources[ResourceIndex].Amount != Resources[ResourceIndex].Min)
            {
                Resources[ResourceIndex].SetAmount(Resources[ResourceIndex].Min); 
                OnResourceDeplete(this);
            }
        }
        else
        {
            Resources[ResourceIndex].SubAmount(AmountToAdd);
            OnResourceDelta(this, AmountToAdd);
        }
    }


    public void Set(int ResourceIndex,float AmountToAdd)
    {
        Resources[ResourceIndex].SetAmount(AmountToAdd);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


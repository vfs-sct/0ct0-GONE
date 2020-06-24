//Copyright Jesse Rougeau, 2020 ©

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Systems/Resources/New Resource")]
public class Resource : ScriptableObject
{
    struct ResourceData_Internal
    {
        public float Min;
        public float Max;
        public float Value;
        public ResourceModule.ResourceEventDelta OnAddResource;
        public ResourceModule.ResourceEventDelta OnRemoveResource;
        public ResourceData_Internal(float Mn,float val,float Mx,ResourceModule.ResourceEventDelta OA, ResourceModule.ResourceEventDelta OR)
        {
            OnAddResource = OA;
            OnRemoveResource = OR;
            Min = Mn;
            Max = Mx;
            Value = val;
        }
        public void SetValue(float val)
        {
            Value = val;
        }
    }

    [SerializeField] private float Minimum = 0 ;
    [SerializeField] private float DefaultValue = 0;
    [SerializeField] private float Maximum = 100;
    [SerializeField] public string DisplayName = "";
    [SerializeField] public string Abreviation = "";
    [SerializeField] public Color ResourceColor;
    [SerializeField] public Sprite resourceIcon = null;

    Dictionary<ResourceInventory,ResourceData_Internal> Data = new Dictionary<ResourceInventory, ResourceData_Internal>();

    /*  
    //
    //*********** Warning: These functions are internal and should not be called from outside the resource system! ****************
    //                      Use either the ResourceInventory or Resource Module Classes instead!
    */


    //Create a new resource type in an inventory, overriding default values
    public void RegisterInstance(ResourceModule.ResourceData DataIn,ResourceInventory owner)
    {
        Debug.Assert(!Data.ContainsKey(owner));
        Data.Add(owner,new ResourceData_Internal(DataIn.min,DataIn.value,DataIn.max,null,null));
    }

    private void RegisterInstance_internal(ResourceInventory owner,ResourceData_Internal RSData)
    {
        Data.Add(owner,RSData);
    }

    //used for fuel bar HUD
    public float GetMaximum()
    {
        return Maximum;
    }

    //register a new delegate to call when a resource is added
    public void RegisterOnAddDelegate(ResourceInventory owner, ResourceModule.ResourceEventDelta newDelegate)
    {
        Debug.Assert(Data.ContainsKey(owner));
        Data[owner] = new ResourceData_Internal(Data[owner].Min,Data[owner].Value,Data[owner].Max,Data[owner].OnAddResource + newDelegate,Data[owner].OnRemoveResource);
    }

    //register a new delegate to call when a resource is subtracted
    public void RegisterOnRemoveDelegate(ResourceInventory owner, ResourceModule.ResourceEventDelta newDelegate) 
    {
        Debug.Assert(Data.ContainsKey(owner));
        Data[owner] = new ResourceData_Internal(Data[owner].Min,Data[owner].Value,Data[owner].Max,Data[owner].OnAddResource,Data[owner].OnRemoveResource + newDelegate);
    }



    //Create a new resource type in an inventory using default values
    public void RegisterInstance(ResourceInventory owner)
    {
        Debug.Assert(!Data.ContainsKey(owner));
        Data.Add(owner,new ResourceData_Internal(Minimum,DefaultValue,Maximum,null,null));
    }


    //Remove a resource type from an inventory
    public void RemoveInstance(ResourceInventory owner)
    {
        Data.Remove(owner);
    }

    //Get the current amount of a resource in an inventory
    public float GetInstanceValue(ResourceInventory owner)
    {
        Debug.Assert(Data.ContainsKey(owner));
        return Data[owner].Value;
    }

    //Set the current amount of resource in an inventory
    public void SetInstanceValue(ResourceInventory owner,float value)
    {
        Data[owner] = new ResourceData_Internal(Data[owner].Min,value,Data[owner].Max,Data[owner].OnAddResource,Data[owner].OnRemoveResource);
    }

    public void MoveInventory(ResourceInventory OldInv, ResourceInventory NewInv)
    {
        ResourceData_Internal ResourceData= Data[OldInv]; 
        RemoveInstance(OldInv);
        RegisterInstance_internal(OldInv,ResourceData);
    }


    //Does the inventory contain atleast {value} amount
    public bool CanSubtract(ResourceInventory owner,float value)
    {  
        return Data[owner].Value+value <= Data[owner].Max;
    }

    //Does the inventory contain atleast {value} free space
    public bool CanAdd(ResourceInventory owner,float value)
    {
        return Data[owner].Value-value >= Data[owner].Min;
    }

    //Add {valueToAdd} amount of a resource to an inventory
    public void AddInstanceValue(ResourceInventory owner,float valueToAdd)
    {   
        //if(Data[owner].Value == null)
        //{
        //    Debug.LogWarning("The AddInstanceValue function was given a null owner parameter");
        //    return;
        //}
        SetInstanceValue(owner,(Mathf.Clamp(Data[owner].Value + valueToAdd,Minimum,Maximum)));
        if (Data[owner].OnAddResource!=null)  Data[owner].OnAddResource(this,valueToAdd);
    }

     //Subtract {valueToSub} amount of a resource to an inventory (Wrapper for add)
    public void SubInstanceValue(ResourceInventory owner,float valueToSub)
    {
        AddInstanceValue(owner,-valueToSub);
        if (Data[owner].OnRemoveResource !=null) Data[owner].OnRemoveResource(this,valueToSub);
    }

    //Required scriptableModule behavior
    public void Reset()
    {
        Data.Clear();
    }
}

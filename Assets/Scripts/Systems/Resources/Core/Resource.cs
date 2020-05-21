using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Resources/New Resource")]
public class Resource : ScriptableObject
{
    struct ResourceData_Internal
    {
        public float Min;
        public float Max;
        public float Value;
        public ResourceData_Internal(float Mn,float val,float Mx)
        {
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

    Dictionary<ResourceInventory,ResourceData_Internal> Data = new Dictionary<ResourceInventory, ResourceData_Internal>();


    public void RegisterInstance(ResourceModule.ResourceData DataIn,ResourceInventory owner)
    {
        Debug.Assert(!Data.ContainsKey(owner));
        Data.Add(owner,new ResourceData_Internal(DataIn.min,DataIn.value,DataIn.max));
    }


    public void RegisterInstance(ResourceInventory owner)
    {
        Debug.Assert(!Data.ContainsKey(owner));
        Data.Add(owner,new ResourceData_Internal(Minimum,DefaultValue,Maximum));
    }

    public void RemoveInstance(ResourceInventory owner)
    {
        Data.Remove(owner);
    }


    public float GetInstanceValue(ResourceInventory owner)
    {
        Debug.Assert(Data.ContainsKey(owner));
        return Data[owner].Value;
    }

    public void SetInstanceValue(ResourceInventory owner,float value)
    {
        Data[owner] = new ResourceData_Internal(Data[owner].Min,value,Data[owner].Max);
    }

    public bool CanSubtract(ResourceInventory owner,float value)
    {  
        return Data[owner].Value+value <= Data[owner].Max;
    }

    public bool CanAdd(ResourceInventory owner,float value)
    {
        return Data[owner].Value-value >= Data[owner].Min;
    }

    public void AddInstanceValue(ResourceInventory owner,float valueToAdd)
    {   
        SetInstanceValue(owner,(Mathf.Clamp(Data[owner].Value + valueToAdd,Minimum,Maximum)));
    }
    public void SubInstanceValue(ResourceInventory owner,float valueToSub)
    {
        AddInstanceValue(owner,-valueToSub);
    }
    public void Reset()
    {
        Data.Clear();
    }
}

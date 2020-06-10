using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RepairableComponent : MonoBehaviour
{
    public delegate void RepairEvent(RepairableComponent parent);



    [System.Serializable]
    public struct RepairRequirements
    {
        public Resource resource;
        public int requiredAmount;
        public int currentAmount;
        public int amountPerRepair;
        public RepairRequirements(Resource Rs,int RA)
        {
            resource = Rs;
            requiredAmount = RA;
            currentAmount = 0;
            amountPerRepair = 1;
        }
        public RepairRequirements(Resource Rs,int RA,int CA,int APR)
        {
            resource = Rs;
            requiredAmount = RA;
            currentAmount = CA;
            amountPerRepair = APR;
        }
        public RepairRequirements(Resource Rs,int RA,int CA)
        {
            resource = Rs;
            requiredAmount = RA;
            currentAmount = CA;
            amountPerRepair = 1;
        }
    }
    [SerializeField] protected List<RepairRequirements> RequiredResources = new List<RepairRequirements>();

    public UnityEvent OnRepairEvent = new UnityEvent(); 
    protected RepairEvent OnFullRepair;
    protected bool _Repaired = false;
    public bool Repaired{get =>_Repaired;}

    public void AddRepairResource(int index,int amount)
    {
        RequiredResources[index] = new RepairRequirements(RequiredResources[index].resource,RequiredResources[index].requiredAmount,RequiredResources[index].currentAmount+amount);
    }
    public bool DoRepair(ResourceInventory RepairInventory)
    {
        if (Repaired) return false;
        Debug.Log("Repairing "+ this);
        bool RepairCycle = false;
        int MetRequirements = 0;
        for (int i = 0; i < RequiredResources.Count; i++)
        {
            if (RequiredResources[i].requiredAmount > RequiredResources[i].currentAmount)
            {
                if (RepairInventory.GetResource(RequiredResources[i].resource) >= RequiredResources[i].amountPerRepair)
                {
                    RepairCycle = true;
                    RepairInventory.RemoveResource(RequiredResources[i].resource,RequiredResources[i].amountPerRepair);
                    AddRepairResource(i,RequiredResources[i].amountPerRepair);
                }
            }
            else
            {
                MetRequirements ++;
            }
            
        }
        _Repaired = (MetRequirements == RequiredResources.Count -1);
        if (_Repaired)
        {
            Debug.Log(this + " Repaired");
            if (OnFullRepair != null) OnFullRepair(this);
            OnRepairEvent.Invoke();
            OnRepair(this);
            RepairCycle = false;
        }
        return RepairCycle;
    }    

    protected virtual void OnRepair(RepairableComponent parent)
    {

    }
}

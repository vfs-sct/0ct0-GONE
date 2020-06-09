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
    [SerializeField] private List<RepairRequirements> RequiredResources = new List<RepairRequirements>();

    public UnityEvent OnRepair = new UnityEvent(); 
    private RepairEvent OnFullRepair;
    private bool _Repaired = false;
    public bool Repaired{get =>_Repaired;}

    public void AddRepairResource(int index,int amount)
    {
        RequiredResources[index] = new RepairRequirements(RequiredResources[index].resource,RequiredResources[index].requiredAmount,RequiredResources[index].currentAmount+amount);
    }


    public bool DoRepair(ResourceInventory RepairInventory)
    {
        if (_Repaired) return false;
        bool RepairCycle = false;
        int MetRequirements = 0;

        float ResourceStorage = 0;
        for (int i = 0; i < RequiredResources.Count; i++)
        {
            if (RequiredResources[i].currentAmount < RequiredResources[i].requiredAmount)
            {
                ResourceStorage = RepairInventory.GetResource(RequiredResources[i].resource);
                if (ResourceStorage > 0)
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
            OnFullRepair(this);
            OnRepair.Invoke();
        }
        return RepairCycle;
    }    


}

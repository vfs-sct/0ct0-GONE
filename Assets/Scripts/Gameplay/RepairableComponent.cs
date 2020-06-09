using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableComponent : MonoBehaviour
{
    public delegate void RepairEvent(RepairableComponent parent);



    [System.Serializable]
    public struct RepairRequirements
    {
        public Resource resource;
        public int requiredAmount;
        public int currentAmount;
        public RepairRequirements(Resource Rs,int RA)
        {
            resource = Rs;
            requiredAmount = RA;
            currentAmount = 0;
        }
         public RepairRequirements(Resource Rs,int RA,int CA)
        {
            resource = Rs;
            requiredAmount = RA;
            currentAmount = CA;
        }
    }
    [SerializeField] private List<RepairRequirements> RequiredResources = new List<RepairRequirements>();


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
                    RepairInventory.RemoveResource(RequiredResources[i].resource,1);
                    AddRepairResource(i,1);
                }
            }
            else
            {
                MetRequirements ++;
            }
        }
        _Repaired = 
        if (_Repaired) OnFullRepair(this);
        return RepairCycle;
    }    


}

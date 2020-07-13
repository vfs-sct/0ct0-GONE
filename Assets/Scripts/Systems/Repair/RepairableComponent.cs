using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RepairableComponent : MonoBehaviour
{

    [SerializeField] RepairableInfo repairInfo = null;
    [SerializeField] private List<CraftingModule.ItemRecipeData> _RequiredComponents;

    [SerializeField] private float TimeToRepair = 1;
    //turned off goo glue
    [SerializeField] private float GooGluePerRepairCycle = 1;
    [SerializeField] private Resource GoGlueResourceName;

    public RepairObjectEvent LinkedEvent;

    public UnityEvent OnRepairComplete = new UnityEvent();

    public List<CraftingModule.ItemRecipeData> RequiredComponents { get=> _RequiredComponents; }

    private float _RepairPercentage = 0;
    public float RepairPercentage{get=>_RepairPercentage;}
    private float PercentPerTick;

    private float NextRepairTick;
   
    private ResourceInventory resourceInventory;
    private InventoryController itemInventory;

    private const float RepairTickrate = 0.2f;



    private void Start()
    {
        if (LinkedEvent != null)
        {
            LinkedEvent.RegisterNewComponent(this);
        }
    }

    public void SetupRepair(GameObject parent)
    {
        resourceInventory = parent.GetComponent<ResourceInventory>();
        itemInventory = parent.GetComponent<InventoryController>();
        NextRepairTick = Time.time + RepairTickrate;
        PercentPerTick = RepairTickrate/TimeToRepair;
    }

    public bool CanRepair(GameObject parent)
    {
        if (!itemInventory.CheckIfItemBucket()) return false;
        foreach (var ComponentData in _RequiredComponents)
        {
            if (!itemInventory.GetItemBucket()[0].Bucket.ContainsKey(ComponentData.item)) return false;
            if (ComponentData.amount > itemInventory.GetItemBucket()[0].Bucket[ComponentData.item]) return false;
            
        }
        //if (resourceInventory.GetResource(GoGlueResourceName) < GooGluePerRepairCycle*(TimeToRepair/RepairTickrate)) return false;
        if (_RepairPercentage >=  1) 
            {
                CompleteRepair(parent);
                return false; //complete the repair
            }
        return true;
    }



    public void RepairUpdate(GameObject parent)
    {
        if (Time.time > NextRepairTick)
        {
            //resourceInventory.RemoveResource(GoGlueResourceName,GooGluePerRepairCycle);
            _RepairPercentage += PercentPerTick;
            Debug.LogWarning("Repair at " +PercentPerTick);
            NextRepairTick = Time.time + 0.2f;
        }
    }

    protected void CompleteRepair(GameObject parent)
    {
        Debug.LogWarning("Repair Completed");

        repairInfo.SetIsRepaired(true);

        OnRepairComplete.Invoke();
    }
 
}

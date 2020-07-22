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

    //if this object is not already repaired, can't repair this object
    [SerializeField] private RepairableComponent _previousRepair = null;
    public RepairableComponent previousRepair { get => _previousRepair; }
    
    //next repairable object needs to know if this one's been repaired
    private bool _isRepaired;
    public bool isRepaired { get => _isRepaired; }

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
        if (_previousRepair != null && !_previousRepair.isRepaired)
        {
            Debug.Log("Previous repair object not completed");
            return false;
        }
        if(itemInventory == null) return false;
        if (!itemInventory.CheckIfItemBucket()) return false;
        foreach (var ComponentData in _RequiredComponents)
        {
            if (!itemInventory.GetItemBucket()[0].Bucket.ContainsKey(ComponentData.item)) return false;
            if (ComponentData.amount > itemInventory.GetItemBucket()[0].Bucket[ComponentData.item]) return false;   
        }
        //if (resourceInventory.GetResource(GoGlueResourceName) < GooGluePerRepairCycle*(TimeToRepair/RepairTickrate)) return false;
        if (_RepairPercentage >=  1) 
            {
            //EVAN - repair over time complete from in game world (probably repairing normal damage to the station)
            AkSoundEngine.PostEvent("Octo_Tether_Rope", gameObject);
            CompleteRepair(parent);
                return false; //complete the repair
            }
        return true;
    }



    public void RepairUpdate(GameObject parent)
    {
        //EVAN - repairs being done over time in-game
        AkSoundEngine.PostEvent("Octo_Tether_Grab", gameObject);
        if (Time.time > NextRepairTick)
        {
            //resourceInventory.RemoveResource(GoGlueResourceName,GooGluePerRepairCycle);
            _RepairPercentage += PercentPerTick;
            Debug.LogWarning("Repair at " +PercentPerTick);
            NextRepairTick = Time.time + 0.2f;
        }
    }

    public void InstantComplete(GameObject parent)
    {
        //EVAN - repair of major station component completed from within UI, completes event objective
        AkSoundEngine.PostEvent("Octo_Grab", gameObject);
        AkSoundEngine.PostEvent("Octo_Salvage", gameObject);
        AkSoundEngine.PostEvent("Complete_Objective", gameObject);
        CompleteRepair(parent);
    }

    protected void CompleteRepair(GameObject parent)
    {
        //wasn't removing required components before
        foreach (var ComponentData in _RequiredComponents)
        {
            itemInventory.RemoveFromItemBucket(ComponentData.item, ComponentData.amount);
        }

        Debug.LogWarning("Repair Completed");

        _isRepaired = true;

        repairInfo.SetIsRepaired(_isRepaired);

        OnRepairComplete.Invoke();
    }
 
}

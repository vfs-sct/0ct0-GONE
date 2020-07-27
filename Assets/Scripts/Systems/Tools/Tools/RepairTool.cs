using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName = "Systems/Tools/Repair Tool")]
public class RepairTool : Tool
{
    private HealthComponent healthComponent;
    private ResourceInventory inventoryComponent;

    [SerializeField] private GameObject popText = null;

    [SerializeField] private Resource RepairNaniteResource;

    [SerializeField] private float NanitesPerCycle = 5;

    [SerializeField] private float HealthPerCycle = 100;

    [SerializeField] private float CycleInterval = 1;


    private float NextUpdateTime = 0;

    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        if(target.GetComponent<RepairableComponent>() == null)
        {
            Debug.Log("Target in LoopCondition() returned null");
            var popTxt = Instantiate(popText);
            popTxt.GetComponentInChildren<TextMeshProUGUI>().SetText("Cannot repair!");
            return false;
        }
        return true;
    }

    protected override bool DeactivateCondition(ToolController owner, GameObject target)
    {
        return true;
    }

    protected override bool LoopCondition(ToolController owner, GameObject target)
    {
       return healthComponent.CanRepair(owner.gameObject) && inventoryComponent.GetResource(RepairNaniteResource) >= NanitesPerCycle;
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {        
       healthComponent = target.GetComponent<HealthComponent>();
       NextUpdateTime = Time.time + CycleInterval;
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        healthComponent = null;
        NextUpdateTime = 0;
    }

    protected override void OnDeselect(ToolController owner)
    {
        Debug.Log("Repair Tool DeSelected");
        inventoryComponent = null;
    }

    protected override void OnSelect(ToolController owner)
    {
        Debug.Log("Repair Tool Selected");
        inventoryComponent = owner.GetComponent<ResourceInventory>();
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        if (NextUpdateTime <= Time.time)
        {
            healthComponent.Heal(HealthPerCycle);
            inventoryComponent.RemoveResource(RepairNaniteResource,NanitesPerCycle);
            Debug.Log("Repairing Target");
        }

        
        
    }
}

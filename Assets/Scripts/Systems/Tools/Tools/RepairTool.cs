using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName = "Systems/Tools/Repair Tool")]
public class RepairTool : Tool
{
    private RepairableComponent repairableComponent;
    private ResourceInventory inventoryComponent;

    [SerializeField] private GameObject popText = null;

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
       return repairableComponent.CanRepair(owner.gameObject);
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {        
       repairableComponent = target.GetComponent<RepairableComponent>();
       repairableComponent.SetupRepair(owner.gameObject);
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        repairableComponent = null;
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
        repairableComponent.RepairUpdate(owner.gameObject);
        Debug.Log("Repairing Target");
    }
}

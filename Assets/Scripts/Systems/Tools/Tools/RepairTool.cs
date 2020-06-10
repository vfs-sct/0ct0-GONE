using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Tools/Repair Tool")]
public class RepairTool : Tool
{
    private RepairableComponent repairableComponent;
    private ResourceInventory inventoryComponent;
    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        
        return true;
    }

    protected override bool DeactivateCondition(ToolController owner, GameObject target)
    {
        return true;
    }

    protected override bool LoopCondition(ToolController owner, GameObject target)
    {

        repairableComponent = target.GetComponent<RepairableComponent>();
        Debug.Log(repairableComponent);
        return repairableComponent.DoRepair(inventoryComponent);
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {
       
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
        Debug.Log("Repairing Target");
    }
}

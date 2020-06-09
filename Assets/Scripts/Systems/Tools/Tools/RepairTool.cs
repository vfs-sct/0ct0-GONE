﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return repairableComponent.DoRepair(inventoryComponent);
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {
        inventoryComponent = owner.GetComponent<ResourceInventory>();
        repairableComponent = target.GetComponent<RepairableComponent>();
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        repairableComponent = null;
        inventoryComponent = null;
    }

    protected override void OnDeselect(ToolController owner)
    {
        
    }

    protected override void OnSelect(ToolController owner)
    {
        
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        
    }
}

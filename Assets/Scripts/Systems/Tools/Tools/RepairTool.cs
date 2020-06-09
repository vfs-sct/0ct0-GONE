using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTool : Tool
{
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
        return true;
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {

    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        
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

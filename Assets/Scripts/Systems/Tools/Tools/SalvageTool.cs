using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
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
        return false;
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {
        Debug.Log("Salvager Activated");
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        Debug.Log("Salvager Deactivated");
    }

    protected override void OnDeselect(ToolController owner)
    {
        Debug.Log("Salvager Selected");
    }

    protected override void OnSelect(ToolController owner)
    {
        Debug.Log("Salvager DeSelected");
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        
    }
}

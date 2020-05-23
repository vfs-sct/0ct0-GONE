using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/TestTool")]
public class TestTool : Tool
{
    protected override bool ActivateCondition(ToolController owner,GameObject target)
    {
        return true;
    }

    protected override bool DeactivateCondition(ToolController owner,GameObject target)
    {
        return true;
    }

    protected override bool LoopCondition(ToolController owner,GameObject target)
    {
        return true;
    }

    protected override void OnActivate(ToolController owner,GameObject target)
    {
        Debug.Log("Starting Test tool");
    }

    protected override void OnDeactivate(ToolController owner,GameObject target)
    {
        Debug.Log("Stopping Test tool");
    }

    protected override void OnDeselect(ToolController owner)
    {
        Debug.Log("test tool deselected");
    }

    protected override void OnSelect(ToolController owner)
    {
        Debug.Log("test tool selected");
    }

    protected override void OnWhileActive(ToolController owner,GameObject target)
    {
        Debug.Log("Test Tool IsActive");
    }
}

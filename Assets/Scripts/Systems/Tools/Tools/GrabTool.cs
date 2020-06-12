using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Grabber")]
public class GrabTool : Tool
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
        Debug.Log("Grabber Activated");
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        Debug.Log("Grabber Deactivated");
    }

    protected override void OnDeselect(ToolController owner)
    {
        Debug.Log("Grabber Detected");
    }

    protected override void OnSelect(ToolController owner)
    {
        Debug.Log("Grabber DeSelected");
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        throw new System.NotImplementedException();
    }
}

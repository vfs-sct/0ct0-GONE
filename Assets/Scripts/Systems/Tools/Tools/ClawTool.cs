using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Claw Tool")]
public class ClawTool : Tool
{
    private Transform HoldPos;
    private Rigidbody TargetRB;

    [SerializeField] private AK.Wwise.Event PlayGrabSound;
    [SerializeField] private float MaxHoldDistance;

    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        TargetRB = target.GetComponent<Rigidbody>();
        PlayGrabSound.Post(target);
        return (TargetRB != null);
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
        TargetRB = null;
        HoldPos = null;
    }

    protected override void OnSelect(ToolController owner)
    {
        HoldPos = owner.GetComponent<Player>().ObjectHoldPosition;
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        TargetRB.MovePosition(HoldPos.position);
        //TargetRB.MoveRotation(HoldPos.rotation);
    }
}

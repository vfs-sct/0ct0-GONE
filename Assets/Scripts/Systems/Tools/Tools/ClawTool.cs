using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Claw Tool")]
public class ClawTool : Tool
{
    private Transform HoldPos;
    private Rigidbody TargetRB;

    private Rigidbody OwnerRB;

    [SerializeField] private AK.Wwise.Event PlayGrabSound;
    [SerializeField] private float MaxHoldDistance;

    //used by tutorial to see if player has correctly used claw tool
    public bool HasObject()
    {
        if(TargetRB != null)
        {
            return true;
        }
        return false;
    }

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
        OwnerRB = owner.GetComponent<Rigidbody>();
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        if (TargetRB != null)
        {
            TargetRB.MovePosition(HoldPos.position);
            TargetRB.velocity = OwnerRB.velocity;
        }
        else
        {
            Debug.LogError("Clawtool Target has no Rigidbody!");
        }
        //TargetRB.MoveRotation(HoldPos.rotation);
    }
}

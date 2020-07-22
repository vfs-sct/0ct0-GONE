using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Claw Tool")]
public class ClawTool : Tool
{
    private Transform HoldPos;
    private Rigidbody TargetRB;

    private Rigidbody OwnerRB;

    [SerializeField] private AK.Wwise.Event PlayGrabSound;
    [SerializeField] private float MaxHoldDistance;
    [SerializeField] private GameObject popTextGO = null;

    private float LastPressedTime;
    private float AntiSpamDelay = 0.2f;

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
        
        //target has a mesh visible to the player but isn't a grabbable object
        var TargetMesh = target.GetComponent<MeshRenderer>();
        if (TargetMesh != null && TargetRB == null)
        {
            //anti spam
            if (LastPressedTime + AntiSpamDelay <= Time.unscaledTime)
            {
                //EVAN - if there's a "failed tool" sound
                var popText = Instantiate(popTextGO);
                popText.GetComponentInChildren<TextMeshProUGUI>().SetText("Can't Grab");
                LastPressedTime = Time.unscaledTime;
            }
        }

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
        LastPressedTime = Time.unscaledTime;
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

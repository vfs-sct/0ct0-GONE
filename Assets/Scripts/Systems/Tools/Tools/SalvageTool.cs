using UnityEngine;
[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
{
    [SerializeField] public ResourceGainedPopTxt popText = null;
    [SerializeField] private float MaxSalvageDistance = 5.0f;


    private bool finishedSalvage = false;

    private float salvageTime = 1.0f;

    Salvagable SalvComp;

    GameObject OriginalTarget;

    bool OutOfRange = false;
    bool SwitchedTargets = false;
    bool FinishedSalvage = false;

    float SalvageStartTime = 0;
    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        //Debug.Log(target);
        if (target != null && target.GetComponent<Salvagable>() != null)
        {
            //Debug.Log("Salvage Found");
            return true;
        }
        return false;
    }

    protected override bool DeactivateCondition(ToolController owner, GameObject target)
    {
        return true;
    }

    protected override bool LoopCondition(ToolController owner, GameObject target)
    {
        SwitchedTargets = (OriginalTarget != target);
        if (target == null) return false;
        OutOfRange = Vector3.Distance(target.transform.position,owner.transform.position) >= MaxSalvageDistance;
        FinishedSalvage = ((SalvageStartTime+salvageTime) <= Time.unscaledTime);
        
        return !(OutOfRange || SwitchedTargets || FinishedSalvage);
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {
        Debug.Log("Activated");
        SalvComp = target.GetComponent<Salvagable>();
        OriginalTarget = target;
        salvageTime = SalvComp.SalvageItem.SalvageTime;
        SalvageStartTime = Time.unscaledTime;
        if (SalvComp.SalvageItem.IsSalvage)
        {
            if (!owner.PlayerInventory.CanAddToResourceBucket(SalvComp.SalvageItem,SalvComp.Amount))
            {
                Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Full");
                Deactivate(owner,target);
            }
        }
        else 
        {
            //error pop text
            Instantiate(popText).popText.SetText("Cannot Salvage");
            //Debug.Log("Could not salvage");
            //Debug.Log(SalvComp.SalvageItem + " is not a resource");
        }
        
        
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {

        if (SalvComp == null | SwitchedTargets |!FinishedSalvage ) return;
        Debug.Log(OriginalTarget + " " + target );
        Debug.Log("Deactivated");
        
        if (owner.PlayerInventory.AddToResourceBucket(SalvComp.SalvageItem,SalvComp.Amount))
            {
                //Debug.Log(owner.PlayerInventory.GetResourceAmount(SalvComp.SalvageItem.ResourceType));
                Destroy(OriginalTarget);
                //resource gained pop text
                Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Gained");
                //Debug.Log("Salvaged Object");
            } else 
            {
                Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Full");
            }   
            //error pop text
            //Debug.Log("Not enough space");
            SalvComp = null;
            OriginalTarget = null;
    }

    protected override void OnSelect(ToolController owner)
    {
        //Debug.Log("Salvager Selected");
    }

    protected override void OnDeselect(ToolController owner)
    {
        //Debug.Log("Salvager DeSelected");
        SalvComp = null; //cleanup
    }

    

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
    }
}

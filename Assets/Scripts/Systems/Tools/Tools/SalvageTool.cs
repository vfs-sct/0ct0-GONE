using UnityEngine;
[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
{
    [SerializeField] public ResourceGainedPopTxt popText = null;


    private bool finishedSalvage = false;

    private float salvageTime = 1.0f;

    Salvagable SalvComp;

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
        return false;
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {
        SalvComp = target.GetComponent<Salvagable>();
        salvageTime = SalvComp.SalvageItem.SalvageTime;
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
        if (SalvComp == null) return;
        if (owner.PlayerInventory.AddToResourceBucket(SalvComp.SalvageItem,SalvComp.Amount))
            {
                //Debug.Log(owner.PlayerInventory.GetResourceAmount(SalvComp.SalvageItem.ResourceType));
                Destroy(target);
                //resource gained pop text
                Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Gained");
                //Debug.Log("Salvaged Object");
                return;
            }
            //error pop text
            Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Full");
            //Debug.Log("Not enough space");
            SalvComp = null;
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

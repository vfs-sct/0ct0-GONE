using UnityEngine;
[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
{
    [SerializeField] public ResourceGainedPopTxt popText = null;
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
        Salvagable SalvComp = target.GetComponent<Salvagable>();
        if (SalvComp.SalvageItem.IsSalvage)
        {
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
        //Debug.Log("Salvager Deactivated");
    }

    protected override void OnSelect(ToolController owner)
    {
        //Debug.Log("Salvager Selected");
    }

    protected override void OnDeselect(ToolController owner)
    {
        //Debug.Log("Salvager DeSelected");
    }

    

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        
    }
}

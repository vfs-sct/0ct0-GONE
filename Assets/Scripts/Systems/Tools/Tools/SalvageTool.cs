using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
{
    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        if (target.GetComponent<Salvagable>() != null) Debug.Log("Salvage Found");
        return (target.GetComponent<Salvagable>() != null);
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
        if (SalvComp.SalvageItem.IsResourceItem)
        {
            if (owner.PlayerInventory.AddToResourceBucket(SalvComp.SalvageItem.ResourceType,SalvComp.SalvageItem))
            {
                Destroy(target);
                Debug.Log("Salvaged Object");
                return;
            }
            Debug.Log("Not enough space");
        }
        else 
        {
            Debug.Log("Could not salvage");
            Debug.Log(SalvComp.SalvageItem + " is not a resource");
        }
        
        
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        Debug.Log("Salvager Deactivated");
    }

    protected override void OnSelect(ToolController owner)
    {
        Debug.Log("Salvager Selected");
    }

    protected override void OnDeselect(ToolController owner)
    {
        Debug.Log("Salvager DeSelected");
    }

    

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        
    }
}

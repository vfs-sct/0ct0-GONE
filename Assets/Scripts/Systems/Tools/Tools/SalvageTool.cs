using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
{
    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        if (target == null ) return false;
        Salvagable SalvageObj = target.GetComponent<Salvagable>();
        if (SalvageObj == null) return false;
        Debug.Log(SalvageObj.HarvestingTool);
        return (SalvageObj.HarvestingTool == this); //only activate if the target is salvagable
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
        Salvagable SalvageObj = target.GetComponent<Salvagable>();
        float SalvageMultiplier = SalvageObj.LinkedSalvage.BaseMult;
        Debug.Log("Salvager Activated");
        SalvageObj.LinkedSalvage.DoSalvage(SalvageObj,owner.GetComponent<ResourceInventory>(),1.0f,SalvageMultiplier);

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

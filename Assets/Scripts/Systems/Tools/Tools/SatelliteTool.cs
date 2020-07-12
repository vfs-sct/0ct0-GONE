using UnityEngine;



[CreateAssetMenu(menuName = "Systems/Tools/Place Satellite")]
public class SatelliteTool : Tool
{
    SatelliteInventory satInv;
    GameObject SatellitePreview;
    SatelliteBehavior SatBehavior;

    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        return (SatBehavior.PlacementConditionCheck(owner) &&(satInv.StoredSatellites[0] != null));
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
        Debug.Log("Satellite Tool Activated");
        Destroy(SatellitePreview);
        GameObject PlacedSat = GameObject.Instantiate(satInv.StoredSatellites[0].PlacePrefab);
        PlacedSat.transform.position = satInv.SatelliteSpawnPos.position;
        PlacedSat.transform.rotation = satInv.SatelliteSpawnPos.rotation;
        satInv.RemoveSat(0);
        owner.DeselectTool();
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        Debug.Log("Satellite Tool Deactivated");
    }

    protected override void OnDeselect(ToolController owner)
    {
        Debug.Log("Satellite Tool DeSelected");
        //turn off tooltips from satinv
        satInv.NoSatTooltip.SetActive(false);
        satInv.SatNotInCloud.SetActive(false);
        satInv.PlaceSat.SetActive(false);
        //nullify satinv
        satInv = null;
        SatBehavior = null;
        Destroy(SatellitePreview);
    }

    protected override void OnSelect(ToolController owner)
    {
        Debug.Log("Satellite Tool Selected");
        satInv = owner.GetComponent<SatelliteInventory>();
        if (satInv == null || satInv.StoredSatellites[0] == null)
        {
            satInv.NoSatTooltip.SetActive(true);
            Debug.Log("NoSat Found");
            return;
        }
        SatellitePreview = GameObject.Instantiate(satInv.StoredSatellites[0].PreviewPrefab);
        SatellitePreview.transform.position = satInv.SatelliteSpawnPos.position;
        SatellitePreview.transform.rotation = satInv.SatelliteSpawnPos.rotation;
        SatellitePreview.transform.SetParent(owner.GetComponentInChildren<Camera>().transform);
        SatBehavior = SatellitePreview.GetComponent<SatelliteBehavior>();
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        if (satInv == null || satInv.StoredSatellites[0] == null)
        {
            return;
        }
        if(SatBehavior.PlacementConditionCheck(owner) && (satInv.StoredSatellites[0] != null))
        {
            satInv.PlaceSat.SetActive(true);
            satInv.NoSatTooltip.SetActive(false);
            satInv.SatNotInCloud.SetActive(false);
        }
        else
        {
            satInv.PlaceSat.SetActive(false);
            satInv.NoSatTooltip.SetActive(false);
            satInv.SatNotInCloud.SetActive(true);
        }
    }
}

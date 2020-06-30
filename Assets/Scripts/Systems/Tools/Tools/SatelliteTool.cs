using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Systems/Tools/Place Satellite")]
public class SatelliteTool : Tool
{
    SatelliteInventory satInv;
    GameObject SatellitePreview;

    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        return (satInv.StoredSatellites[0] != null);
        

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
        Destroy(SatellitePreview);
        GameObject PlacedSat = GameObject.Instantiate(satInv.StoredSatellites[0].PlacePrefab);
        PlacedSat.transform.position = satInv.SatelliteSpawnPos.position;
        PlacedSat.transform.rotation = satInv.SatelliteSpawnPos.rotation;
        satInv.RemoveSat(0);
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        
    }

    protected override void OnDeselect(ToolController owner)
    {
        satInv = null;
        Destroy(SatellitePreview);
    }

    protected override void OnSelect(ToolController owner)
    {
        satInv = owner.GetComponent<SatelliteInventory>();
        if (satInv == null || satInv.StoredSatellites[0] == null)
        {
            Debug.Log("NoSat Found");
            return;
        }
        SatellitePreview = GameObject.Instantiate(satInv.StoredSatellites[0].PreviewPrefab);
        SatellitePreview.transform.position = satInv.SatelliteSpawnPos.position;
        SatellitePreview.transform.rotation = satInv.SatelliteSpawnPos.rotation;
        SatellitePreview.transform.SetParent(owner.GetComponentInChildren<Camera>().transform);

    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        
    }
}

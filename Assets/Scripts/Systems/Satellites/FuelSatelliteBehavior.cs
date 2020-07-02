using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSatelliteBehavior : SatelliteBehavior
{
    public override bool PlacementConditionCheck(ToolController Owner)
    {
        Debug.Log("Check");
        return true;
        
    }
}

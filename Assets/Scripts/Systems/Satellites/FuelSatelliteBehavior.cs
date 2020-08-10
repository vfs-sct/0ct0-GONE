using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSatelliteBehavior : SatelliteBehavior
{
    [SerializeField] private LayerMask CloudColliderMask;
    

    bool ReturnValue = false;
    Collider[] CloudColliders;
    
    GasCloud FoundCloud = null;

    public override bool PlacementConditionCheck(ToolController Owner)
    {
        ReturnValue = false;
        CloudColliders = Physics.OverlapSphere(transform.position, 0.5f,CloudColliderMask,QueryTriggerInteraction.Collide);
        foreach (var collider in CloudColliders)
        {
            FoundCloud = collider.GetComponentInParent<GasCloud>();
            if (FoundCloud != null)
            {
                if (!FoundCloud.HasNaniteSat)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public override void OnPlacePreview(ToolController Owner)
    {
        FoundCloud.HasNaniteSat = true;
    }
}

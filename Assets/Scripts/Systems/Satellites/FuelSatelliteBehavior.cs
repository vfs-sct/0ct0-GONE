using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSatelliteBehavior : SatelliteBehavior
{
    [SerializeField] private LayerMask CloudColliderMask;
    
        public override bool PlacementConditionCheck(ToolController Owner)
    {
        return Physics.CheckSphere(transform.position, 0.5f,CloudColliderMask,QueryTriggerInteraction.Collide);
    }
}

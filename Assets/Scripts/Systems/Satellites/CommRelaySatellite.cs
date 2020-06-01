using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Satellite/New CommRelay")]
public class CommRelaySatellite : PlayerSatellite
{
    public override bool Pickup(PlayerSatelliteBehavior Parent)
    {
        throw new System.NotImplementedException();
    }

    public override bool Place(PlayerSatelliteBehavior Parent)
    {
        Parent.gameObject.AddComponent<CommunicationZone>(); //check if in range
        return true;
    }

    public override bool PlacementCondition(PlayerSatelliteBehavior Parent)
    {
        throw new System.NotImplementedException();
    }

    public override void Start(PlayerSatelliteBehavior Parent)
    {
        
    }

    public override void Update(PlayerSatelliteBehavior Parent)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Satellite/New CommRelay")]
public class CommRelaySatellite : PlayerSatellite
{
    public override void Pickup(PlayerSatelliteBehavior Parent,GameObject NewSat)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject Place(PlayerSatelliteHolder Parent)
    {
        return null;
    }

    public override bool PlacementCondition(PlayerSatelliteBehavior Parent)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(PlayerSatelliteBehavior Parent)
    {
    }

    public override void UpdateSat(PlayerSatelliteBehavior Parent)
    {
        
    }
}

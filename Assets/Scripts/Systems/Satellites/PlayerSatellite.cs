using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSatellite : ScriptableObject
{
    [SerializeField] protected GameObject Prefab;
    public abstract void Update(PlayerSatelliteBehavior Parent);
    public abstract void Start(PlayerSatelliteBehavior Parent);
    public abstract bool PlacementCondition(PlayerSatelliteBehavior Parent);
    public abstract bool Place(PlayerSatelliteBehavior Parent);
    public abstract bool Pickup(PlayerSatelliteBehavior Parent);
}

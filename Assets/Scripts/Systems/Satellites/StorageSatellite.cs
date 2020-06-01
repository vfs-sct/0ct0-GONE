using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Satellite/New Storage")]

public class StorageSatellite : PlayerSatellite
{
    [SerializeField] private ResourceModule ResourceManager;
    public override void Pickup(PlayerSatelliteBehavior Parent,GameObject NewSat)
    {
        ResourceInventory NewInv = NewSat.AddComponent<ResourceInventory>();
        ResourceManager.MoveInventory(Parent.GetComponentInChildren<ResourceInventory>(),NewInv);
    }

    public override GameObject Place(PlayerSatelliteHolder Parent)
    {
        return null;
    }

    public override bool PlacementCondition(PlayerSatelliteBehavior Parent)
    {
        return true;
    }

    public override void Start(PlayerSatelliteBehavior Parent)
    {
    }

    public override void Update(PlayerSatelliteBehavior Parent)
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatelliteHolder : MonoBehaviour
{
    [SerializeField] private GameObject BaseSatellitePrefab;

    [SerializeField] private PlayerSatellite HeldSatellite = null;

    [SerializeField] private Transform DefaultPlacementPos;

    public bool PickupSatellite(PlayerSatelliteBehavior Target)
    {
        if (HeldSatellite == null | Target.SatelliteType == null)
        {
            return false;
        }
        if (Target.CanPickup)
        {
            GameObject NewSat = CreateNewHeldSatellite(Target.SatelliteType);
            Target.Pickup(this,NewSat);
            return true;
        }
        return false;
    }

    public GameObject CreateNewHeldSatellite(PlayerSatellite NewSat)
    {
        HeldSatellite = NewSat;
        GameObject SatModel = GameObject.Instantiate(NewSat.Prefab);
        return SatModel;
    }



    public void Place(Transform PlacementTransform)
    {
        HeldSatellite.Place(this);
        GameObject NewSat = GameObject.Instantiate(BaseSatellitePrefab);
        
        NewSat.transform.position = PlacementTransform.position;
        NewSat.transform.rotation = PlacementTransform.rotation;
    }



}

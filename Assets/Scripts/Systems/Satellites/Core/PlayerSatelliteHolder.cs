using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatelliteHolder : MonoBehaviour
{
    [SerializeField] private GameObject BaseSatellitePrefab;
    [SerializeField] private PlayerSatellite HeldSatellite = null;

    [SerializeField] private GameObject HeldSatModel;

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
            gameObject.GetComponent<Rigidbody>().mass += Target.SatelliteType.Mass;
            return true;
        }
        return false;
    }

    public GameObject CreateNewHeldSatellite(PlayerSatellite NewSat)
    {
        HeldSatellite = NewSat;
        HeldSatModel = GameObject.Instantiate(NewSat.CarryingPrefab);

        HeldSatModel.transform.position = DefaultPlacementPos.position;
        HeldSatModel.transform.rotation = DefaultPlacementPos.rotation;
        HeldSatModel.transform.parent = gameObject.transform;
        
        return HeldSatModel;
    }

    public void Place()
    {
        Place(DefaultPlacementPos);
    }

    public void Place(Transform PlacementTransform)
    {
        
        HeldSatellite.Place(this);
        Destroy(HeldSatModel);
        GameObject NewSatBase = GameObject.Instantiate(BaseSatellitePrefab);
        GameObject NewSat = GameObject.Instantiate(HeldSatellite.PlacementPrefab);
        NewSatBase.transform.position = PlacementTransform.position;
        NewSatBase.transform.rotation = PlacementTransform.rotation;
        
        NewSat.transform.rotation = PlacementTransform.rotation;
        NewSat.transform.position = PlacementTransform.position;
        
        NewSat.transform.parent = NewSatBase.transform;

        HeldSatellite.Init(NewSatBase.GetComponentInChildren<PlayerSatelliteBehavior>());
        gameObject.GetComponent<Rigidbody>().mass -= HeldSatellite.Mass;
        HeldSatellite = null;
    }



}

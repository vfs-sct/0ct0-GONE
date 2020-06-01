using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatelliteBehavior : MonoBehaviour
{
    [SerializeField] private PlayerSatellite _SatelliteType;
    public PlayerSatellite SatelliteType{get => _SatelliteType;}

    public bool CanPickup{get => SatelliteType.CanPickup;}

    private void Start()
    {
    }

    private void SetType(PlayerSatellite Type)
    {
        _SatelliteType = Type;
    }


    private void Update()
    {
        if (_SatelliteType == null) return;
        if (SatelliteType.CanUpdate) SatelliteType.UpdateSat(this);
    }

    public bool Interact()
    {
        if (SatelliteType.CanInteract) return SatelliteType.Interact(this);
        return false;
    }



    public void Pickup(PlayerSatelliteHolder Parent,GameObject NewSat)
    {
        SatelliteType.Pickup(this,NewSat);
        Destroy(gameObject);
    }
}

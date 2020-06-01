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
        if (SatelliteType == null)
        {
            Debug.LogAssertion(this.name +" is without a type!");
            return;
        }
        SatelliteType.Start(this);

    }

    private void SetType(PlayerSatellite Type)
    {
        _SatelliteType = Type;
    }


    private void Update()
    {
        if (SatelliteType.CanUpdate) SatelliteType.Update(this);
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

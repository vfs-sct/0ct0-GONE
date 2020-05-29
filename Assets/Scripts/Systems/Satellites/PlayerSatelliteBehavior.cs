using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatelliteBehavior : MonoBehaviour
{
    [SerializeField] private PlayerSatellite SatelliteType;

    private void Start()
    {
        if (SatelliteType == null)
        {
            Debug.LogAssertion(this.name +" is without a type!");
            return;
        }
        SatelliteType.Start(this);
        SatelliteType.Place(this);
    }
    private void Update()
    {
        SatelliteType.Update(this);
    }

    public void Pickup()
    {
        SatelliteType.Pickup(this);
        Destroy(gameObject);
    }
}

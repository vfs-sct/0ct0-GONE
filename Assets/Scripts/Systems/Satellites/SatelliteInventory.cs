using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteInventory : MonoBehaviour
{
    [SerializeField] private Transform _SatelliteSpawnPos;
    public Transform SatelliteSpawnPos{get=>_SatelliteSpawnPos;}
    [SerializeField] private List<Satellite> _StoredSatellites = new List<Satellite>();

    public List<Satellite> StoredSatellites {get=>_StoredSatellites;}

    [SerializeField] private int SatelliteCapactiy = 1;

    public int Capacity {get=> SatelliteCapactiy;}

    public void RemoveSat(int index)
    {
        _StoredSatellites.RemoveAt(index);
    }

    public void SetSatellite(Satellite sat, int index = 0 )
    {
        _StoredSatellites[index] = sat;
    }
}

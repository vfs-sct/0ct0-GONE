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

    [Header("Tooltips")]
    [SerializeField] public GameObject NoSatTooltip = null;
    [SerializeField] public GameObject SatNotInCloud = null;
    [SerializeField] public GameObject PlaceSat = null;

    public int Capacity {get=> SatelliteCapactiy;}

    public void RemoveSat(int index)
    {
        _StoredSatellites.RemoveAt(index);
    }

    public void SetSatellite(Satellite sat, int index = 0 )
    {
        if (_StoredSatellites != null && index < _StoredSatellites.Count)
        {
            _StoredSatellites[index] = sat;
        }
        else
        {
            Debug.Log("Provided index invalid, creating new satellite list entry");
            _StoredSatellites.Add(sat);
        }
    }

    public Satellite GetSatellite()
    {
        //assumes you can only ever have 1 satellite in inventory
        if (_StoredSatellites.Count == 0)
        {
            return null;
        }
        else if(_StoredSatellites[0] != null)
        {
            return _StoredSatellites[0];
        }
        return null;
    }

    public bool CheckIfSat()
    {
        if(_StoredSatellites == null || _StoredSatellites.Count == 0)
        {
            return false;
        }
        return true;
    }
}

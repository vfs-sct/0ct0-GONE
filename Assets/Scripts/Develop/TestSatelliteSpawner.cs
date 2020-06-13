using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSatelliteSpawner : MonoBehaviour
{
    [SerializeField] private PlayerSatellite SatelliteToSpawn;

    [SerializeField] private PlayerSatelliteHolder SatelliteHolder;


    // Start is called before the first frame update
    void Start()
    {
        //SatelliteHolder.CreateNewHeldSatellite(SatelliteToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

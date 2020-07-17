using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float PlanetRotationRate = 0.05f;
    [SerializeField] private float CloudRotationRate = 0.05f;
    

    [SerializeField] private GameObject PlanetSphere;
    [SerializeField] private GameObject CloudSphere;

    [SerializeField] private GameObject AtmoSphere1;

    [SerializeField] private GameObject AtmoSphere2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlanetSphere.transform.Rotate(0,0,PlanetRotationRate,Space.Self);
        
        CloudSphere.transform.Rotate(0,0,CloudRotationRate,Space.Self);
        AtmoSphere1.transform.Rotate(0,0,PlanetRotationRate,Space.Self);
        AtmoSphere2.transform.Rotate(0,0,PlanetRotationRate,Space.Self);

    }
}

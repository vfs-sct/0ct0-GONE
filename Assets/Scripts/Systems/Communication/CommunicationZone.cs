using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommunicationZone : MonoBehaviour
{
    [SerializeField] private CommunicationModule CommunicationManager = null;

    [SerializeField] private float _Radius = 5000;
    public float Radius{get=>_Radius;}

    private int ZoneIndex = -1;
    private void OnEnable()
    {

    }

    void Start()
    {
        ZoneIndex = CommunicationManager.AddZone(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

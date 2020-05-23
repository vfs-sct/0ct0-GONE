using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommTest : MonoBehaviour
{
   
   [SerializeField] private CommunicationModule RelayController;


   [SerializeField] private GameObject playerObj;
    void Start()
    {
        RelayController.SetPlayer(playerObj);
        RelayController.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

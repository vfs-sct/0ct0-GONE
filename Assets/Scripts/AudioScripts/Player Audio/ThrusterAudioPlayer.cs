// Evan Landry | Copyright 2020 © All rights reserved

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAudioPlayer : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event PlayThrusterAudio;
    [SerializeField] private AK.Wwise.Event StopThrusterAudio;
    [SerializeField] private AK.Wwise.RTPC ThrusterImpulseRTPC;
    [SerializeField] private MovementController Movement;

    private Vector3 throttle;
    float ThrusterVolume;
    private void Start()
    {
        // I play the sound to the Object this script is attatched to here. 
        PlayThrusterAudio.Post(gameObject);
    }

    private void Update()
    {
        throttle = (Movement.ActiveMode as SpaceMovement).Throttle;
        ThrusterVolume = 0;

        for (int i = 0; i < 3; i++) //use the largest throttle value for the sound
        {
            throttle[i] = Mathf.Abs(throttle[i]);
            if (ThrusterVolume < throttle[i]) ThrusterVolume = throttle[i];
        }
        //Debug.Log(ThrusterVolume);
        ThrusterImpulseRTPC.SetGlobalValue(ThrusterVolume);
    }

    private void OnDestroy()
    {
        // When the player dies I need to stop the sound.
        StopThrusterAudio.Post(gameObject);
    }
}

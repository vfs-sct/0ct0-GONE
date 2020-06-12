// Evan Landry | Copyright 2020 © All rights reserved

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAudioPlayer : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event PlayThrusterAudio;
    [SerializeField] private AK.Wwise.Event StopThrusterAudio;
    [SerializeField] private AK.Wwise.RTPC ThrusterImpulseRTPC;

    [SerializeField] private SpaceMovement Movement;

    private void Start()
    {
        // I play the sound to the Object this script is attatched to here. 
        PlayThrusterAudio.Post(gameObject);
    }

    private void Update()
    {
        // Calculations for how loud the sound will be based on how long the player is holding the movement key down is handled here.
        ThrusterImpulseRTPC.SetGlobalValue(Mathf.Abs(Movement.Throttle.x) + Mathf.Abs(Movement.Throttle.y) + Mathf.Abs(Movement.Throttle.z) / 3);
        //Debug.Log(ThrusterImpulseRTPC.GetGlobalValue());
        //Debug.Log("==== Setting RTCP ====");
    }

    private void OnDestroy()
    {
        // When the player dies I need to stop the sound.
        StopThrusterAudio.Post(gameObject);
    }
}

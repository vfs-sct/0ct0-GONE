// Evan Landry | Copyright 2020 © All rights reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Step 1
[RequireComponent(typeof(Rigidbody))]   // Makes sure a Rigidbody is available for the AK Object

public class SoundLoopPlayer : MonoBehaviour
{
    // Step 2
    // ---------------------------------------
    //              Properties
    // ---------------------------------------

    // ! SP properties referencing the events within the Wwise Launcher
    [SerializeField] private AK.Wwise.Event Placeholder;
    [SerializeField] private AK.Wwise.Event Env_01_Start;
    [SerializeField] private AK.Wwise.Event Env_01_Stop;
    
    // Step 3
    // ! Ensure that the tabs in Editor match the methods in this script
    private void Awake()
    {
        // ? This is for calling the sound from Wwise
        // gameObject in this context references the Object this script is attatched to.
        AkBankManager.LoadBank("Wwise_Octo_Main", true, false);
        Env_01_Start.Post(gameObject);

    }

    // ---------------------------------------
    //              Conditions
    // ---------------------------------------
    
    // ! Stop the Sound in Editor
    [ContextMenu("Stop the Sound")]
    private void StopSound()
    {
        // ? This is for calling the sound to stop in Wwise
        Env_01_Stop.Post(gameObject);
    }

    // ! Play the Sound in Editor
    [ContextMenu("Play the Sound")]
    private void PlaySound()
    {
        // ? This is for calling the sound to stop in Wwise
        Env_01_Start.Post(gameObject);  
    }
}

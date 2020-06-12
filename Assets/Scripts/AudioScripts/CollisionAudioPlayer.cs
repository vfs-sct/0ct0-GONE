// Evan Landry | Copyright 2020 © All rights reserved

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudioPlayer : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event CollisionAudioEvent;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            CollisionAudioEvent.Post(gameObject);
        } 
    }
}

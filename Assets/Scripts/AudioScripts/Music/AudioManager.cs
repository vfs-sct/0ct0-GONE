using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Start()
    {
        var MusicManager = GameObject.Find("MusicManager");
        if (MusicManager != null)
        {
            MusicManager.transform.SetParent(gameObject.transform);
        }
        else
        {
            Debug.LogWarning("Music manager was null!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("MusicManager").transform.SetParent(gameObject.transform);
    }
}

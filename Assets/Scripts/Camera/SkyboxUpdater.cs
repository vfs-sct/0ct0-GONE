using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxUpdater : MonoBehaviour
{
    [SerializeField] private Camera SkyboxCamera;

    [SerializeField] private PlayerCamera CameraScript;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SkyboxCamera.transform.rotation = CameraScript.Rotation;
    }
}

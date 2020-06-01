using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxUpdater : MonoBehaviour
{
    [SerializeField] private Camera SkyboxCamera;

    private Camera PlayerCam = null;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerCam  = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        SkyboxCamera.transform.rotation = PlayerCam.transform.rotation;
    }
}

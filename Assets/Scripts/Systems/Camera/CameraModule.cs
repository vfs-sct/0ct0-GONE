using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScriptableGameFramework;

[CreateAssetMenu(menuName = "Systems/Camera/Camera Module")]
public class CameraModule : Module
{
    [SerializeField] private GameObject SkyboxPrefab;


    //skybox stuff
    public GameObject SkyboxObj;
    public Camera SkyboxCam;
    private Camera _MainCamera;
    public Camera MainCamera{get =>_MainCamera;}

    public override void Start()
    {
        SkyboxObj = GameObject.Instantiate(SkyboxPrefab);
        SkyboxObj.transform.position = new Vector3(10000,10000,10000);
        SkyboxCam = SkyboxObj.GetComponentInChildren<Camera>();
    }

    public void SetMainCamera(Camera NewCamera)
    {
        _MainCamera = NewCamera;
    }


    public override void Reset()
    {
        
    }
}

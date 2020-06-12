using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Quaternion CameraRotation = new Quaternion();
    [SerializeField] private CameraModule CameraManager;

    [SerializeField] private Camera PlayerCam;

    [SerializeField] private GameObject CameraRoot;
    public GameObject Root{get=>CameraRoot;}
    

    public Vector3 RotationVector{get=>CameraRotation.eulerAngles;}
    public Quaternion Rotation{get=>CameraRotation;}

    //gets the transform of the camera root
    public Transform GetRootTransform()
    {
        CameraRoot.transform.rotation = CameraRotation;
        return CameraRoot.transform;
    }
    //gets the transform of the camera root with the position shifted to NewPos

    public void RotateCamera(Vector3 NewRotation)
    {
        Quaternion AddRot = new Quaternion();
        AddRot.eulerAngles = NewRotation;
        CameraRotation *= AddRot;
    }

   
    public void SetRotation(Quaternion NewRotation)
    {
        CameraRotation = NewRotation;
    }
    public void SetRotation(Vector3 EulerAngles)
    {
        CameraRotation.eulerAngles = EulerAngles;
    }

    private void Awake()
    {
        CameraRotation = CameraRoot.transform.rotation;
    }

    private void Update()
    {
        CameraRoot.transform.rotation = CameraRotation;
    }



}

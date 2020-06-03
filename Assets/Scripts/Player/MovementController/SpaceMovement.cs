﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Systems/Movement/SpaceMovement-Old")]
public class SpaceMovement : MovementComponent
{
    
    [SerializeField] private float ThrusterImpulse = 10;

    [SerializeField] private float ThrusterTorque = 10f;
    [SerializeField] private float VelocityMax = -1.0f;
    [SerializeField] private float ThrottleSensitivity = 1; //meters per s per frame
    [SerializeField] private Resource FuelResource = null;
    [SerializeField] private float FuelPerImpulseUnit = 0.2f;
    [SerializeField] private float FuelPerTorqueUnit = 0.2f;
    [SerializeField] private float FuelEfficency = 100;
    private float _VelocityMax;
    Rigidbody AnchorTarget = null;

    Vector3 Throttle = new Vector3();

    private Rigidbody _Rigidbody;
    private float Mass;

    private ResourceInventory LinkedResourceBehavior;

    public override void Initialize(MovementController Controller)
    {
        if (VelocityMax <= 0) _VelocityMax = 99999;
        LinkedResourceBehavior = Controller.gameObject.GetComponent<ResourceInventory>();
        if (ThrottleSensitivity <=0) ThrottleSensitivity = 0.001f;//minimum throttle sensitivity that can be set
        _Rigidbody = Controller.gameObject.GetComponent<Rigidbody>();
        Debug.Assert(_Rigidbody != null); //Assert if rigid body is undefined
    }

    public void SetAnchorTarget(GameObject Target)
    {
        AnchorTarget = Target.GetComponent<Rigidbody>();
    }

    public void ClearAnchorTarget()
    {
        SetAnchorTarget(null);
    }

    private void CoupledTranslate(MovementController Controller, Vector3 Input)
    {
        TargetVelocityWorld = Input * _VelocityMax;
    }

    private void CruiseTranslate(MovementController Controller, Vector3 Input)
    {
        TargetVelocityWorld += Input *ThrottleSensitivity;
    }


    

    private Vector3 CalculateImpulse(MovementController Controller)
    {
        Vector3 Impulse = new Vector3();
       
        //get the difference between our desired velocity and our current velocity
        Vector3 DeltaV = (Controller.CameraScript.Root.transform.TransformDirection(TargetVelocityWorld))-_Rigidbody.velocity; 

        //add our target's velocity onto our desired velocity if we have a target
        if (AnchorTarget != null)  DeltaV += AnchorTarget.velocity;
        
        //calculate unadjusted impulse
        Impulse = _Rigidbody.mass * DeltaV;

       
        for (int i = 0; i < 3; i++)
        { //componentwise clamp of impulse to maximum thruster values
            Impulse[i] = Mathf.Clamp(Impulse[i],-ThrusterImpulse,ThrusterImpulse);

            Throttle[i] = Impulse[i]/ThrusterImpulse; //get the throttle value for sound and vfx
        }

        return Impulse;
    }

    public override void Rotate(MovementController Controller, Vector3 Angle, byte MovementSubMode)
    {
        TargetAngle = new Vector3(0,0,0);
    }


    private Vector3 CalculateRotation(MovementController Controller)
    {
        Vector3 AngularVelocity = new Vector3();
        Vector3 DeltaAngle = (TargetAngle - Controller.transform.rotation.eulerAngles) * Mathf.Deg2Rad;
        AngularVelocity = (DeltaAngle * ThrusterTorque)/50;
        return AngularVelocity;
    }


    //private CalculateVelocityChange(MovementController Controller)
    //{
    //}

    private void ApplyTorque(MovementController Controller)
    {
        Vector3 Torques = new Vector3();
        float AngleDelta = 0;
        for (int i = 0; i < 3; i++)
        {
            AngleDelta = Mathf.Clamp(Mathf.DeltaAngle(TargetAngle[i], _Rigidbody.rotation.eulerAngles[i]),-20,20);
            Torques[i] = Mathf.Clamp(AngleDelta,-ThrusterTorque,ThrusterTorque);
        }

        _Rigidbody.AddRelativeTorque(new Vector3(-1,0,0) * Torques.x,ForceMode.Impulse);
        _Rigidbody.AddRelativeTorque(new Vector3(0,-1,0) * Torques.y,ForceMode.Impulse);
        _Rigidbody.AddRelativeTorque(new Vector3(0,0,-1) * Torques.z,ForceMode.Impulse);
    }


    public override void Translate(MovementController Controller,Vector3 Input,byte MovementSubMode)
    {
        switch (MovementSubMode)
        {
            case 0://coupled mode
            {
                CoupledTranslate(Controller,Input);
                break;
            }
            case 1://cruise mode
            {
                CruiseTranslate(Controller,Input);
                break;
            }
        }
    }
    public override void MovementUpdate(MovementController Controller,byte MovementSubMode)
    {
//        Debug.Log(CalculateImpulse(Controller));
        _Rigidbody.AddForce(CalculateImpulse(Controller), ForceMode.Impulse);
        //_Rigidbody.angularVelocity = CalculateRotation(Controller);
        ApplyTorque(Controller);
     
        
    }
}

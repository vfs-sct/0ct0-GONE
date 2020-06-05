using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementComponent : ScriptableObject
{

    protected Vector3 TargetVelocityLocal  = new Vector3(); //target velocity in m/s in world space

    protected Vector3 TargetVelocityWorld = new Vector3(); //target velocity in m/s in local space

    protected Vector3 TargetAngle  = new Vector3(); //target angular in degrees

    public virtual void Translate(MovementController Controller,Vector3 Input,byte MovementSubMode){}
    public virtual void Rotate(MovementController Controller,Vector3 Input,byte MovementSubMode){}


    public virtual void OnFrameUpdate(MovementController Controller){}
    
    public abstract void MovementUpdate(MovementController Controller,byte MovementSubMode);

    public virtual void Initialize(MovementController Controller){}

    protected Vector3 VectorToLocalSpace(MovementController Controller,Vector3 InputVector)
    {
        Vector3 LocalVector = new Vector3();
        LocalVector += InputVector.x * Controller.transform.right;
        LocalVector += InputVector.y * Controller.transform.up;
        LocalVector += InputVector.z * Controller.transform.forward;
        return LocalVector;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementComponent : ScriptableObject
{

    protected Vector3 TargetVelocity; //target velocity in m/s
    protected Vector3 TargetAngularVelocity; //target angular velocity in rad/s

    public virtual void Translate(MovementController Controller,Vector3 Input,byte MovementSubMode){}
    public virtual void Rotate(MovementController Controller,Vector3 Input,byte MovementSubMode){}
    
    public abstract void MovementUpdate(MovementController Controller,byte MovementSubMode);

    public virtual void Initialize(MovementController Controller){}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Systems/Movement/SpaceMovement")]
public class SpaceMovement : MovementComponent
{
    
    [SerializeField] private float ThrusterImpulse = 10;
    [SerializeField] private float VelocityMax = -1.0f;

    [SerializeField] private float ThrottleSensitivity = 1; //meters per s per frame

    private float _VelocityMax;

    Rigidbody AnchorTarget = null;


    private Rigidbody _Rigidbody;
    private float Mass;


    public void SetAnchorTarget(GameObject Target)
    {
        AnchorTarget = Target.GetComponent<Rigidbody>();
    }

    public void ClearAnchorTarget()
    {
        SetAnchorTarget(null);
    }

    private void CoupledTranslate(MovementController controller, Vector3 Input)
    {
        
        TargetVelocity = Input * _VelocityMax;
    }

    private void CruiseTranslate(MovementController controller, Vector3 Input)
    {
        TargetVelocity += Input *ThrottleSensitivity;
    }


    public override void Initialize(MovementController Controller)
    {
        if (VelocityMax <= 0){
            _VelocityMax = 99999;
        }
        if (ThrottleSensitivity <=0) ThrottleSensitivity = 0.001f;//minimum throttle sensitivity that can be set
        _Rigidbody = Controller.gameObject.GetComponent<Rigidbody>();
        Debug.Assert(_Rigidbody != null); //Assert if rigid body is undefined

    }

    private Vector3 CalculateImpulse(MovementController Controller)
    {
        Vector3 Impulse = new Vector3();
        float MaxDeltaV = ThrusterImpulse /_Rigidbody.mass;

        //------velocity anchoring---------
        Vector3 DeltaV = (TargetVelocity)-_Rigidbody.velocity; 
        if (AnchorTarget != null) 
        {
            DeltaV = (TargetVelocity+AnchorTarget.velocity)-_Rigidbody.velocity;
        }



        for (int i = 0; i < 3; ++i)
	    {
		    if (DeltaV[i] == 0) //prevent division by 0
		    {
			    Impulse[i] = 0;
		    }
		    else
		    {
			    Impulse[i] = Mathf.Clamp((DeltaV[i] / MaxDeltaV), -1f, 1)* ThrusterImpulse;	//calculate target throttle
		    }
	    }
        return Impulse;
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
        Debug.Log(CalculateImpulse(Controller));
        _Rigidbody.AddForce(CalculateImpulse(Controller), ForceMode.Impulse);
    }
}

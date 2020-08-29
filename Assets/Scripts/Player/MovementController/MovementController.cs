using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

    [System.Serializable]
    public struct ThrusterData
    {
        public List<ParticleSystem> XThrustersPos;
        public List<ParticleSystem> XThrustersNeg;
        public List<ParticleSystem> YThrustersPos;
        public List<ParticleSystem> YThrustersNeg;
        public List<ParticleSystem> ZThrustersPos;
        public List<ParticleSystem> ZThrustersNeg;

        public ThrusterData(List<ParticleSystem> xThrustersPos, List<ParticleSystem> xThrustersNeg, List<ParticleSystem> yThrustersPos, List<ParticleSystem> yThrustersNeg, List<ParticleSystem> zThrustersPos, List<ParticleSystem> zThrustersNeg)
        {
            XThrustersPos = xThrustersPos;
            XThrustersNeg = xThrustersNeg;
            YThrustersPos = yThrustersPos;
            YThrustersNeg = yThrustersNeg;
            ZThrustersPos = zThrustersPos;
            ZThrustersNeg = zThrustersNeg;
        }

        private List<List<ParticleSystem>> GetValue(int key)
        {
            List<List<ParticleSystem>> temp =   new List<List<ParticleSystem>>();
            switch (key)
            {
                case 0: {
                    
                    temp.Add(XThrustersPos);
                    temp.Add(XThrustersNeg);
                    break;
                    }
                case 1: {
                    temp =   new List<List<ParticleSystem>>();
                    temp.Add(YThrustersPos);
                    temp.Add(YThrustersNeg);
                    break;
                    }
                case 2: {
                    temp =   new List<List<ParticleSystem>>();
                    temp.Add(ZThrustersPos);
                    temp.Add(ZThrustersNeg);
                    break;
                    }
                default: {return null;}
            }
            return temp;
        }

        private void SetValue(int key,List<List<ParticleSystem>>  value)
        {
            switch (key)
            {
                case 0: {
                    XThrustersPos = value[0]; 
                    XThrustersNeg = value[1];
                    break;
                    }
                case 1: {
                    YThrustersPos = value[0]; 
                    YThrustersNeg = value[1];
                    break;
                }
                case 2: {
                    ZThrustersPos = value[0]; 
                    ZThrustersNeg = value[1];
                    break;
                    }
                default: {break;}
            }
        }

        public List<List<ParticleSystem>> this[int key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }
    }
    


    private Vector3 _RawInput = new Vector3();

    private Vector3  _RotationTarget = new Vector3();
    private Vector3 _NormalizedInput= new Vector3();

    private Vector3 TargetVelocityLocal  = new Vector3(); //target velocity in m/s in world space

    private Vector3 TargetVelocityWorld = new Vector3(); //target velocity in m/s in local space

    private Vector3 TargetAngle  = new Vector3(); //target angular in degrees

    private byte MovementMode = 0;

    [SerializeField] private bool NormalizeInput = true;

    [SerializeField] private float ThrusterImpulse = 10;
    //modified thruster is the Thruster * _InventoryWeight
    private float _ModifiedThrusterImpulse;

    [SerializeField] private float ThrusterTorque = 10f;
    [SerializeField] private float VelocityMax = -1.0f;
    [SerializeField] private float ThrottleSensitivity = 1; //meters per s per frame

    
    [SerializeField] private float FuelPerImpulseUnit = 0.2f;
    [SerializeField] private float FuelPerTorqueUnit = 0.2f;
    [SerializeField] private float FuelEfficency = 100;
    [SerializeField] private float ScrollMult = 0.05f;

    [SerializeField] private Throttle ThrottleUI;
    
    [SerializeField] private Animator OctoAnimator;

    [SerializeField]  private float AnimatorLerpSpeed = 20;

    
    private float _VelocityMax;
    private float _SetVelocityMax;
    //_InventoryWeight is used for slowing octo down when he's carrying a lot
    //It's based on chunks so it's not really "mass"
    private float _InventoryWeight = 1;
    private float speedModifier;
    public Vector3 Throttle = new Vector3();

  
    private float Mass;

    private float ScrollValue = 100;

    [SerializeField] private PlayerCamera _CameraScript;
    public PlayerCamera CameraScript{get=>_CameraScript;}
    [SerializeField] private Rigidbody _Rigidbody;
    [SerializeField] private ResourceInventory LinkedResourceBehavior;
    [SerializeField] private Resource FuelResource = null;

    [SerializeField] private ThrusterData _TranslationThrusters;
    [SerializeField] private ThrusterData _RotationThrusters;

    [SerializeField] private float ThrusterCutoff = 0.05f;
    private Vector3 AngularThrottle = new Vector3();



    public Vector3 RawInput{get => _RawInput;}
    public Vector3 NormalizedInput{get => _NormalizedInput;}


    public int GetSpeedModifier()
    {
        //Debug.LogWarning("Speed modifier " + speedModifier * 100);
        return (int)(speedModifier * 100);
    }

    //used to change octo's speed depending how much stuff he's holding
    public void SetInventoryWeight(float newWeight)
    {
        _InventoryWeight = newWeight;
        CalculateModifiedThruster();
        //Debug.LogWarning(_InventoryWeight);
    }

    //used to upgrade thruster power via events
    public void AddThrusterImpulse(float addImpulse)
    {
        ThrusterImpulse += addImpulse;
        CalculateModifiedThruster();
    }
    //calculate octo's speed modifier
    public void CalculateModifiedThruster()
    {
        if(_InventoryWeight == 0)
        {
            _InventoryWeight = 1;
        }
        speedModifier = 1 - (_InventoryWeight / 500);
        if(speedModifier < 0.5)
        {
            speedModifier = 0.5f;
        }
        _ModifiedThrusterImpulse = ThrusterImpulse * speedModifier;
        //Debug.LogWarning(_ModifiedThrusterImpulse);
    }

    public void SetRotationTarget(Vector3 NewRotationIn)
    {
        _RotationTarget = NewRotationIn;
    }

    public void OnHorizontalTranslate(InputAction.CallbackContext context)
    {
        _RawInput.x = context.action.ReadValue<Vector2>().x;
        _RawInput.z = context.action.ReadValue<Vector2>().y;
    }
    public void OnVerticalTranslate(InputAction.CallbackContext context)
    {
        _RawInput.y = context.action.ReadValue<float>();
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        
        ScrollValue += context.action.ReadValue<float>()* ScrollMult;
        ScrollValue = Mathf.Clamp(ScrollValue,5,100);
        _SetVelocityMax = _VelocityMax * (ScrollValue/100);
        ThrottleUI.UpdateUI(ScrollValue, _SetVelocityMax); 
    }

    private void NormalizeInputs()
    {
        _NormalizedInput = _RawInput.normalized;
    }

    private void Start()
    {
        if (VelocityMax <= 0) 
        {
            _VelocityMax = 99999;
        }
        else
        {
            _VelocityMax = VelocityMax;
        }
        _SetVelocityMax = _VelocityMax;
        //used to get the starting maxvelocity - do not alter max velocity
        ThrottleUI.SetMaxVelocity(_VelocityMax);
        if (ThrottleSensitivity <=0) ThrottleSensitivity = 0.001f;//minimum throttle sensitivity that can be set
        Debug.Assert(_Rigidbody != null); //Assert if rigid body is undefined

        CalculateModifiedThruster();

    }

    private void Update()
    {
        OctoAnimator.SetFloat("X",(_Rigidbody.transform.InverseTransformVector(_Rigidbody.velocity)).x/AnimatorLerpSpeed);
        OctoAnimator.SetFloat("Y",(_Rigidbody.transform.InverseTransformVector(_Rigidbody.velocity)).z/AnimatorLerpSpeed);
        OctoAnimator.SetFloat("Speed",Mathf.Clamp(_Rigidbody.velocity.magnitude/AnimatorLerpSpeed,-1,1) );
    }

    private void FixedUpdate()
    {
        NormalizeInputs();
        if (NormalizeInput)
        {
            Translate(_NormalizedInput,MovementMode);
        }
        else
        {
            Translate(_RawInput,MovementMode);
        }
        Rotate(_RotationTarget);
        MovementUpdate(MovementMode);
    }


    private void CoupledTranslate( Vector3 Input)
    {
        TargetVelocityLocal = Input * _SetVelocityMax;
    }

    private void CruiseTranslate(Vector3 Input)
    {
        TargetVelocityLocal += Input *ThrottleSensitivity;

        for (int i = 0; i < 3; i++) //componentwise max speed clamp
        {
              TargetVelocityLocal[i] = Mathf.Clamp(TargetVelocityLocal[i], -_SetVelocityMax, _SetVelocityMax);
        }
      
    }



      private Vector3 CalculateImpulse()
    {
        Vector3 Impulse = new Vector3();
       
        //get the difference between our desired velocity and our current velocity (relative to the camera)
        Vector3 DeltaV = TargetVelocityLocal - CameraScript.GetRootTransform().InverseTransformVector(_Rigidbody.velocity); 
        float fuelUsage = 0;
        
        //calculate unadjusted impulse
        Impulse = _Rigidbody.mass * DeltaV;

       
        for (int i = 0; i < 3; i++)
        { //componentwise clamp of impulse to maximum thruster values
            Impulse[i] = Mathf.Clamp(Impulse[i],-_ModifiedThrusterImpulse, _ModifiedThrusterImpulse);

            Throttle[i] = Impulse[i]/ _ModifiedThrusterImpulse; //get the throttle value for sound and vfx
            fuelUsage += Mathf.Abs(Impulse[i])*(FuelPerImpulseUnit/FuelEfficency);
        }
        LinkedResourceBehavior.RemoveResource(FuelResource,fuelUsage);
        return Impulse;
    }

    public void Rotate(Vector3 Angle)
    {
        TargetAngle = Angle;
    }

    private Vector3 CalculateRotation()
    {
        Vector3 AngularVelocity = new Vector3();
        Vector3 DeltaAngle = (TargetAngle - transform.rotation.eulerAngles) * Mathf.Deg2Rad;
        AngularVelocity = (DeltaAngle * ThrusterTorque)/50;
        return AngularVelocity;
    }
    private void ApplyTorque()
    {
        Vector3 Torques = new Vector3();
        float AngleDelta = 0;
        float fuelUsage = 0;        
        var TargetRot = new Quaternion();
        TargetRot.eulerAngles = TargetAngle;
        var DeltaRot = Quaternion.Inverse(_Rigidbody.rotation)*TargetRot;

        for (int i = 0; i < 3; i++)
        {
            AngleDelta = DeltaRot.eulerAngles[i];
            if (AngleDelta > 180) AngleDelta = -((AngleDelta)-180);
            Torques[i] = Mathf.Clamp(AngleDelta,-ThrusterTorque,ThrusterTorque);
            AngularThrottle[i] = Torques[i]/ThrusterTorque;
            fuelUsage += Mathf.Abs(Torques[i])*(FuelPerTorqueUnit/FuelEfficency);
        }

        _Rigidbody.AddRelativeTorque(new Vector3(1,0,0) * Torques.x,ForceMode.Impulse);
        _Rigidbody.AddRelativeTorque(new Vector3(0,1,0) * Torques.y,ForceMode.Impulse);
        _Rigidbody.AddRelativeTorque(new Vector3(0,0,1) * Torques.z,ForceMode.Impulse);
        LinkedResourceBehavior.RemoveResource(FuelResource,fuelUsage);
    }


    private void UpdateThrusters()
    {
        
        for (int i = 0; i < 3; i++)
        {

                if (Mathf.Abs(AngularThrottle[i]) > ThrusterCutoff)
                {
                if (AngularThrottle[i] > 0)
                {
                    foreach (var item in _RotationThrusters[i][0])
                    {
                        item.gameObject.SetActive(true);
                    }
                    foreach (var item in _RotationThrusters[i][1])
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                else
                {
                    foreach (var item in _RotationThrusters[i][1])
                    {
                        item.gameObject.SetActive(true);
                    }
                    foreach (var item in _RotationThrusters[i][0])
                    {
                        item.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (var item in _RotationThrusters[i][0])
                {
                        item.gameObject.SetActive(false);
                }
                foreach (var item in _RotationThrusters[i][1])
                {
                    item.gameObject.SetActive(false);
                }
            }



            if (Mathf.Abs(Throttle[i]) > ThrusterCutoff)
                {
                if (Throttle[i] > 0)
                {
                    foreach (var item in _TranslationThrusters[i][0])
                    {
                        item.gameObject.SetActive(true);
                    }
                    foreach (var item in _TranslationThrusters[i][1])
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                else
                {
                    foreach (var item in _TranslationThrusters[i][1])
                    {
                        item.gameObject.SetActive(true);
                    }
                    foreach (var item in _TranslationThrusters[i][0])
                    {
                        item.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (var item in _TranslationThrusters[i][0])
                {
                        item.gameObject.SetActive(false);
                }
                foreach (var item in _TranslationThrusters[i][1])
                {
                    item.gameObject.SetActive(false);
                }
            }
        }


    }


    public void Translate(Vector3 Input,byte MovementSubMode)
    {
        switch (MovementSubMode)
        {
            case 0://coupled mode
            {
                CoupledTranslate(Input);
                break;
            }
            case 1://cruise mode
            {
                CruiseTranslate(Input);
                break;
            }
        }
    }

    public void OnFrameUpdate()
    {
    
    }


    public void MovementUpdate(byte MovementSubMode)
    {
        _Rigidbody.AddRelativeForce(CalculateImpulse(), ForceMode.Impulse);
        ApplyTorque();
        UpdateThrusters();
    }

}

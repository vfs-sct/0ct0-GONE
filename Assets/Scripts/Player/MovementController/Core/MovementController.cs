using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private List<MovementComponent> MovementModes = new List<MovementComponent>();
    [SerializeField] private byte MovementMode;
    [SerializeField] private bool NormalizeInput = true;
    

    private MovementComponent ActiveMode = null;

    private Vector3 _RawInput;
    private Vector3 _NormalizedInput;

    public Vector3 RawInput{get => _RawInput;}
    public Vector3 NormalizedInput{get => _NormalizedInput;}

    public void SwitchMode(byte NewMovementMode)
    {
        Debug.Assert((NewMovementMode < MovementModes.Count)| MovementModes[NewMovementMode]!= null);
        MovementMode = NewMovementMode;
        ActiveMode = MovementModes[NewMovementMode];
    }

    public void OnHorizontalTranslate(InputValue value)
    {
        _RawInput.x = value.Get<Vector2>().x;
        _RawInput.z = value.Get<Vector2>().y;
    }
    public void OnVerticalTranslate(InputValue value)
    {
        _RawInput.y = value.Get<float>();
    }

    private void NormalizeInputs()
    {
        _NormalizedInput = _RawInput.normalized;
    }

    private void Start()
    {
        //register input listeners
        
        ActiveMode = MovementModes[MovementMode];
        foreach (var MovementMode in MovementModes)
        {
            MovementMode.Initialize(this);
        }
    }

    private void Update()
    {
        NormalizeInputs();
        if (NormalizeInput)
        {
            ActiveMode.Translate(this,_NormalizedInput,MovementMode);
        }
        else
        {
            ActiveMode.Translate(this,_RawInput,MovementMode);
        }
        ActiveMode.MovementUpdate(this,MovementMode);
    }
}

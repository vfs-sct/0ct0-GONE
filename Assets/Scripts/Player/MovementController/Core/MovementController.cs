using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private List<MovementComponent> MovementModes = new List<MovementComponent>();
    [SerializeField] private byte MovementMode;
    [SerializeField] private bool NormalizeInput = true;

    [SerializeField] private string TranslateXInput = "";

    [SerializeField] private string TranslateYInput = "";

    [SerializeField] private string TranslateZInput = "";


    private MovementComponent ActiveMode = null;

    private InputModule InputManager;

    private Vector3 _RawInput;
    private Vector3 _NormalizedInput;

    public Vector3 RawInput{get => _RawInput;}
    public Vector3 NormalizedInput{get => _NormalizedInput;}

    public void SetInputManager(InputModule newManager)
    {
        InputManager = newManager;
    }
    public void SwitchMode(byte NewMovementMode)
    {
        Debug.Assert((NewMovementMode < MovementModes.Count)| MovementModes[NewMovementMode]!= null);
        MovementMode = NewMovementMode;
        ActiveMode = MovementModes[NewMovementMode];
    }

    private void UpdateTranslateX(float delta)
    {
        _RawInput.x = Input.GetAxis(TranslateXInput);
    }
    private void UpdateTranslateY(float delta)
    {
        _RawInput.y = Input.GetAxis(TranslateYInput);
    }
    private void UpdateTranslateZ(float delta)
    {
        _RawInput.z = Input.GetAxis(TranslateZInput);
    }

    private void NormalizeInputs()
    {
        _NormalizedInput = _RawInput.normalized;
    }

    private void Start()
    {
        //register input listeners
        InputManager.AddAxisListener(TranslateXInput,UpdateTranslateX);
        InputManager.AddAxisListener(TranslateYInput,UpdateTranslateY);
        InputManager.AddAxisListener(TranslateZInput,UpdateTranslateZ);
        
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

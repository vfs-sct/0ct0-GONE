using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "GameFramework/SubSystems/InputModule")]
public class InputModule : Module
{

    public delegate void AxisDelegate(float DeltaValue);
    public delegate void ButtonDelegate();
    private struct AxisData
    {
        public float AxisValue;
        public bool IsActive;
        public AxisDelegate DeltaEvent;

        public void UpdateAxis(float newValue)
        {
            AxisValue = newValue;
        }
        public void AddListener(AxisDelegate newDelagate)
        {
            DeltaEvent +=newDelagate;
        }
        public void RemoveListener(AxisDelegate newDelagate)
        {
            DeltaEvent -=newDelagate;
        }
    }
    private struct ButtonData
    {
        public bool Pressed;
        public bool Released;
        public bool Held;
        public bool IsActive;
        public ButtonDelegate PressedEvent;
        public ButtonDelegate HeldEvent;
        public ButtonDelegate ReleasedEvent;
        public ButtonData(ButtonDelegate E1,ButtonDelegate E2,ButtonDelegate E3)
        {
            Pressed = false;
            Released = false;
            Held = false;
            IsActive = true;
            PressedEvent = E1;
            HeldEvent = E2;
            ReleasedEvent = E3;
        }

        public void UpdateButtonData(bool p, bool r, bool h)
        {
            Pressed = p;
            Released = r;
            Held = h;
        }
        public void AddPressedListener(ButtonDelegate newDelagate)
        {
            PressedEvent +=newDelagate;
        }
        public void RemovePressedListener(ButtonDelegate newDelagate)
        {
            PressedEvent -=newDelagate;
        }
        public void AddHeldListener(ButtonDelegate newDelagate)
        {
            HeldEvent +=newDelagate;
        }
        public void RemoveHeldListener(ButtonDelegate newDelagate)
        {
            HeldEvent -=newDelagate;
        }


        public void AddReleasedListener(ButtonDelegate newDelagate)
        {
            ReleasedEvent +=newDelagate;
        }
        public void RemoveReleasedListener(ButtonDelegate newDelagate)
        {
            ReleasedEvent -=newDelagate;
        }
    }

    [Header("Input Settings")]
    [SerializeField] private bool InputEnabled = true;

    [Header("Axises")]
    [SerializeField] private List<string> AxisList = new List<string>();

    [Header("Buttons")]
    [SerializeField] private List<string> ButtonList = new List<string>();

    private Dictionary<string,AxisData> Axises = new Dictionary<string,AxisData>();
    private Dictionary<string,ButtonData> Buttons = new Dictionary<string,ButtonData>();

    public void InputUpdate()
    {
        foreach (var AxisData in Axises)
        {
            if (!AxisData.Value.IsActive)
            {
                (AxisData.Value).UpdateAxis(0);
            }
            else
            {
                float OldAxisValue = AxisData.Value.AxisValue;
                (AxisData.Value).UpdateAxis(Input.GetAxis(AxisData.Key));
                if (AxisData.Value.DeltaEvent != null && AxisData.Value.AxisValue != 0)
                {
                    AxisData.Value.DeltaEvent(OldAxisValue-AxisData.Value.AxisValue);
                }
            }
        }
        foreach (var ButtonData in Buttons)
        {
            if (!ButtonData.Value.IsActive)
            {
                ButtonData.Value.UpdateButtonData(false,false,false); //may need to add a conditional check here otherwise this fires every frame when disabled
            }
            else
            {
                ButtonData.Value.UpdateButtonData(Input.GetButtonDown(ButtonData.Key),Input.GetButtonUp(ButtonData.Key),Input.GetButton(ButtonData.Key));
                if (ButtonData.Value.Pressed && ButtonData.Value.PressedEvent !=null)
                {
                    ButtonData.Value.PressedEvent();
                }
                if (ButtonData.Value.Released && ButtonData.Value.ReleasedEvent !=null)
                {
                    ButtonData.Value.ReleasedEvent();
                }
                if (ButtonData.Value.Held && ButtonData.Value.HeldEvent !=null)
                {
                    ButtonData.Value.HeldEvent();
                }
            }
           
        }
    }

    public override void Update()
    {
        InputUpdate();
    }

    private void OnEnable()
    {
        Reset();
    }
    public void AddAxisListener(string AxisName,AxisDelegate Listener)
    {
        Axises[AxisName].AddListener(Listener);
    }

    public void AddButtonPressedListener(string ButtonName,ButtonDelegate Listener)
    {
        Buttons[ButtonName].AddPressedListener(Listener);
    }
    public void AddButtonReleaseListener(string ButtonName,ButtonDelegate Listener)
    {
        Buttons[ButtonName].AddReleasedListener(Listener);
    }

    public void AddButtonHeldListener(string ButtonName,ButtonDelegate Listener)
    {
        Buttons[ButtonName].AddHeldListener(Listener);
    }

    public float GetAxis(string AxisName)
    {
        return Axises[AxisName].AxisValue;
    }

    public bool GetButtonDown(string ButtonName)
    {
        return Buttons[ButtonName].Pressed;
    }
    public bool GetButtonUp(string ButtonName)
    {
        return Buttons[ButtonName].Released;
    }
    public bool GetButtonHeld(string ButtonName)
    {
        return Buttons[ButtonName].Held;
    }

    public override void Reset()
    {
        Axises.Clear();
        Buttons.Clear();

        foreach (var AxisName in AxisList)
        {
            AxisData temp = new AxisData();
            temp.IsActive = true;
            Axises.Add(AxisName,temp);

        }
        foreach (var ButtonName in ButtonList)
        {
            ButtonData temp = new ButtonData();
            temp.IsActive = true;
            Buttons.Add(ButtonName,temp);
        }
    }
}

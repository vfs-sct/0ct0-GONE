//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] UIAwake UIAwake = null;
    [SerializeField] Button AudioTabButton = null;
    [SerializeField] Button VideoTabButton = null;
    [SerializeField] Button ControlTabButton = null;

    [SerializeField] GameObject AudioTabPanel = null;
    [SerializeField] GameObject VideoTabPanel = null;
    [SerializeField] GameObject ControlTabPanel = null;

    [SerializeField] Toggle InvertCamToggle = null;
    [SerializeField] public Slider lookSensitivitySlider;
    [SerializeField] TextMeshProUGUI ControlsText = null;

    Dictionary<GameObject, Button> PanelToButton = new Dictionary<GameObject, Button>();

    public System.Action closeCallback;

    public string[] controls = new string[]
    {
        "<b>CONTROLS</b>\n-------",
        "<b>MOUSE</b> - Look",
        "<b>W/S</b> - Move forward/back",
        "<b>A/D</b> - Move left/right",
        "<b>Q/E</b> - Rotate left/right",
        "<b>SPACE/CTRL</b> - Move up/down",
        "<b>1</b> - Select salvage tool",
        "<b>TAB</b> - Target hovered object",
        "<b>LEFT CLICK</b> - Salvage target",
        "<b>ESC</b> - Pause"
    };

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    public void OnEsc(InputValue value)
    {
        Close();
    }

    public void Close()
    {
        closeCallback();

        closeCallback = null;
    }

    void Awake()
    {
        if(PlayerPrefs.GetInt("InvertedCam") == -1)
        {
            InvertCamToggle.isOn = false;
        }
        else if (PlayerPrefs.GetInt("InvertedCam") == 1)
        {
            InvertCamToggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("InvertedCam", 1);
            PlayerPrefs.Save();
            InvertCamToggle.isOn = false;
        }

        lookSensitivitySlider.value = PlayerPrefs.GetFloat("LookSensitivity");

        PanelToButton[AudioTabPanel] = AudioTabButton;
        PanelToButton[VideoTabPanel] = VideoTabButton;
        PanelToButton[ControlTabPanel] = ControlTabButton;

        SwitchActiveTab(AudioTabPanel);
    }

    private void Start()
    {
        string text = "";
        foreach (var control in controls)
        {
            text = text + $"{control}\n";
        }
        ControlsText.SetText(text);
    }

    public void ClickAudioTab()
    {
        SwitchActiveTab(AudioTabPanel);
        AkSoundEngine.PostEvent("Options_Button_Audio", gameObject);
    }

    public void ClickVideoTab()
    {
        SwitchActiveTab(VideoTabPanel);
        AkSoundEngine.PostEvent("Options_Button_Video", gameObject);
    }

    public void ClickControlsTab()
    {
        SwitchActiveTab(ControlTabPanel);
        AkSoundEngine.PostEvent("Options_Button_Controls", gameObject);
    }

    public void SetSensitivitySlider()
    {
        PlayerPrefs.SetFloat("LookSensitivity", lookSensitivitySlider.value);
        PlayerPrefs.Save();
        //Debug.LogError(PlayerPrefs.GetFloat("LookSensitivity"));
        UIAwake.UpdateLookSensitivity();
    }

    public void SetInvertCam()
    {
        AkSoundEngine.PostEvent("Octo_System_Text", gameObject);
        if (InvertCamToggle.isOn)
        {
            //1 makes the camera inverted
            PlayerPrefs.SetInt("InvertedCam", 1);
        }
        else
        {
            //-1 makes the camera not inverted
            PlayerPrefs.SetInt("InvertedCam", -1);
        }
        //Debug.LogError(PlayerPrefs.GetInt("InvertedCam"));
        PlayerPrefs.Save();
        UIAwake.UpdateInvertCam();
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SwitchActiveTab(GameObject active_panel)
    {
        foreach (var kvp in PanelToButton)
        {
            if (kvp.Key == active_panel)
            {
                kvp.Key.SetActive(true);
                kvp.Value.GetComponentInChildren<Button>().interactable = false;
            }
            else
            {
                kvp.Key.SetActive(false);
                kvp.Value.GetComponentInChildren<Button>().interactable = true;
            }
        }
    }
}
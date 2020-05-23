//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] Button AudioTabButton = null;
    [SerializeField] Button VideoTabButton = null;
    [SerializeField] Button ControlTabButton = null;

    [SerializeField] GameObject AudioTabPanel = null;
    [SerializeField] GameObject VideoTabPanel = null;
    [SerializeField] GameObject ControlTabPanel = null;

    //[SerializeField] GameObject PausePrefab;

    Dictionary<GameObject, Button> PanelToButton = new Dictionary<GameObject, Button>();

    public System.Action closeCallback;

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
        PanelToButton[AudioTabPanel] = AudioTabButton;
        PanelToButton[VideoTabPanel] = VideoTabButton;
        PanelToButton[ControlTabPanel] = ControlTabButton;

        SwitchActiveTab(AudioTabPanel);
    }

    public void ClickAudioTab()
    {
        SwitchActiveTab(AudioTabPanel);
    }

    public void ClickVideoTab()
    {
        SwitchActiveTab(VideoTabPanel);
    }

    public void ClickControlsTab()
    {
        SwitchActiveTab(ControlTabPanel);
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
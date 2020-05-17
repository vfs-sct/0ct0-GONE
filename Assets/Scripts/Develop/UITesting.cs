using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITesting : MonoBehaviour
{
    [SerializeField] private UIModule UiManager = null;
    [SerializeField] private ScriptedUI MenuUI = null;
    [SerializeField] private ScriptedUI CreditsUI = null;
    [SerializeField] private ScriptedUI OptionsUI = null;
    void Start()
    {
        //UiManager.CreateInstance(MenuUI);
        //UiManager.Show(MenuUI,0);
        //UiManager.Hide(CreditsUI, 0);
        //UiManager.Hide(OptionsUI, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

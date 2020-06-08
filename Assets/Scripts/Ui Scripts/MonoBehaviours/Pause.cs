//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Pause : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject CodexPrefab = null;
    [SerializeField] GameObject OptionsPrefab = null;
    [SerializeField] GameObject ConfirmationPrefab = null;
    [SerializeField] GameObject AudioReferences = null;
    [SerializeField] GameObject AudioReferences2 = null;

    [SerializeField] string menuScene = null;

    public void OnClickResume()
    {
        GameManager.UnPause();
        gameObject.SetActive(false);
        Debug.Log("Unpaused");
    }

    public void OnClickCodex()
    {
        SwitchViewTo(CodexPrefab);
    }

    public void OnClickOptions()
    {
        var options = OptionsPrefab.GetComponent<Options>();

        options.closeCallback = DoClose;

        SwitchViewTo(OptionsPrefab);
    }

    //used by the Options screen to close itself and reopen the pause menu
    void DoClose()
    {
        gameObject.SetActive(true);
        OptionsPrefab.SetActive(false);
    }

    public void OnClickMainMenu()
    {
        var confirmation = ConfirmationPrefab.GetComponent<Confirmation>();

        confirmation.titleText.SetText("Main Menu?");

        confirmation.bodyText.GetComponent<TMPro.TMP_Text>().SetText("Are you sure you want to return to the main menu?");

        confirmation.clickConfirmCallback = DoMainMenu;

        ConfirmationPrefab.SetActive(true);
    }

    //used by the Confirmation screen
    void DoMainMenu()
    {
        // ========================
        //          AUDIO
        // ========================
        if (AudioReferences == null || AudioReferences2 == null)
        {
            Debug.LogError("One or both sound references has not been hooked up on the GameOver prefab");
        }
        else
        {
            AkSoundEngine.PostEvent("Env_01_Stop", AudioReferences2);
            AkSoundEngine.PostEvent("Communications_Array_Stop", AudioReferences);
        }

        GameManager.LoadScene($"{menuScene}");
        GameManager.UnPause();
    }

    public void OnClickQuit()
    {
        var confirmation = ConfirmationPrefab.GetComponent<Confirmation>();

        confirmation.titleText.SetText("Quit?");

        confirmation.bodyText.GetComponent<TMPro.TMP_Text>().SetText("Are you sure you want to quit to desktop?");

        confirmation.clickConfirmCallback = DoQuit;

        ConfirmationPrefab.SetActive(true);
    }

    //used by the Confirmation screen
    void DoQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();   
#endif

    }

    public void OnEsc(InputValue value)
    {
         GameManager.UnPause();
         gameObject.SetActive(false);
         Debug.Log("Unpaused");
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

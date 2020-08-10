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
    [SerializeField] Tutorial TutorialPrefab = null;
    [SerializeField] GameObject CodexPrefab = null;
    [SerializeField] GameObject OptionsPrefab = null;
    [SerializeField] GameObject ConfirmationPrefab = null;
    [SerializeField] GameObject AudioReferences = null;
    [SerializeField] GameObject AudioReferences2 = null;

    [SerializeField] string menuScene = null;

    private void OnEnable()
    {
        Cursor.visible = true;
        AkSoundEngine.PostEvent("MainMenu_All_Button_Hover", gameObject);
    }

    public void OnClickResume()
    {
        Cursor.visible = false;
        GameManager.UnPause();
        gameObject.SetActive(false);
        AkSoundEngine.PostEvent("MainMenu_All_Button_Hover", gameObject);
        Debug.Log("Unpaused");
    }

    public void OnEsc(InputValue value)
    {
        OnClickResume();
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
        TutorialPrefab.enabled = false;

        // ========================
        //          AUDIO
        // ========================
        if (AudioReferences == null || AudioReferences2 == null)
        {
            Debug.LogError("One or both sound references has not been hooked up on the GameOver prefab");
        }
        else
        {
            AkSoundEngine.PostEvent("White_Noise_Env_Stop", AudioReferences2);
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

        AkSoundEngine.PostEvent("Not_Enough_Resources", gameObject);

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

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

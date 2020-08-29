//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScriptableGameFramework;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    [Header("Next Scene")]
    [SerializeField] string nextScene = null;

    [Header("System")]
    [SerializeField] private GameState MainMenuState = null;
    [SerializeField] private SaveFile saveFile = null;

    [Header("Screens")]
    [SerializeField] GameObject OptionsPrefab = null;
    [SerializeField] GameObject CreditsPrefab = null;
    [SerializeField] GameObject ConfirmationPrefab = null;

    [Header("Buttons")]
    [SerializeField] GameObject continueButton = null;

    [Header("Sound")]
    [SerializeField] GameObject AudioReferences = null;

    public void OnClickContinue()
    {
        if(AudioReferences == null)
        {
            Debug.LogError("A sound reference has not been hooked up on the UI Main Menu prefab");
        }
        else
        {
            AkSoundEngine.PostEvent("MUS_Stop", AudioReferences);
        }
        Game.Manager.LoadScene($"{nextScene}");
    }

    public void OnClickNewGame()
    {
        saveFile.Reset();
        if (AudioReferences == null)
        {
            Debug.LogError("A sound reference has not been hooked up on the UI Main Menu prefab");
        }
        else
        {
            AkSoundEngine.PostEvent("MUS_Stop", AudioReferences);
        }
        Game.Manager.LoadScene($"{nextScene}");
    }

    public void OnClickOptions()
    {
        var options = OptionsPrefab.GetComponent<Options>();

        options.closeCallback = DoClose;

        SwitchViewTo(OptionsPrefab);
    }

    //used by the Options screen to close itself and reopen the main menu
    void DoClose()
    {
        gameObject.SetActive(true);
        OptionsPrefab.SetActive(false);
    }

    public void OnClickCredits()
    {
        SwitchViewTo(CreditsPrefab);
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

    public void OnClickQuit()
    {
        var confirmation = ConfirmationPrefab.GetComponent<Confirmation>();

        confirmation.titleText.SetText("Quit?");

        confirmation.bodyText.SetText("Are you sure you want to quit to desktop?");

        confirmation.clickConfirmCallback = DoQuit;

        ConfirmationPrefab.SetActive(true);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(saveFile.HasSaveGame())
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

        if((MainMenuState as MainMenuState).IsGameCompleted())
        {
            CreditsPrefab.SetActive(true);
            (MainMenuState as MainMenuState).SetGameCompleted(false);
        }
        Debug.Log("HEY - MENU STATE:" + MainMenuState);
        Game.Manager.ChangeGameState(MainMenuState);
        Cursor.visible = true;
    }
}

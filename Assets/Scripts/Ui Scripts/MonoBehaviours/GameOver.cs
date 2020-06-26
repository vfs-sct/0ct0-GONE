//Kristin Ruff-Frederickson | Copyright 2020©
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOver : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject ConfirmationPrefab = null;
    [SerializeField] GameObject AudioReferences = null;
    [SerializeField] GameObject AudioReferences2 = null;

    [SerializeField] string menuScene = null;

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }

    public void OnClickLoad()
    {
        Debug.Log("No save data to load");
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

        if (GameManager.isPaused)
        {
            GameManager.UnPause();
        }

        GameManager.LoadScene($"{menuScene}");
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

        confirmation.bodyText.GetComponent<TMPro.TMP_Text>().SetText("Are you sure you want to quit to desktop?");

        confirmation.clickConfirmCallback = DoQuit;

        ConfirmationPrefab.SetActive(true);
    }
}

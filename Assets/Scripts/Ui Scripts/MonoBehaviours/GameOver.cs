//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject ConfirmationPrefab;

    [SerializeField] string menuScene;

    public void OnClickLoad()
    {
        Debug.Log("No save data to load");
    }

    //used by the Confirmation screen
    void DoMainMenu()
    {
        SceneManager.LoadScene($"{menuScene}");
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

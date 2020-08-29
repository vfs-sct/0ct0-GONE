using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableGameFramework;
public class PostPreload : MonoBehaviour
{


    private void Start()
    {
        // Loads main menu after Awake
        // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Game.Manager.LoadScene("MainMenu");
    }
}
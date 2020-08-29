using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostPreload : MonoBehaviour
{    [SerializeField] private GameFrameworkManager gameManager;

    private void Start()
    {
        // Loads main menu after Awake
        // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        gameManager.LoadScene("MainMenu");
    }
}
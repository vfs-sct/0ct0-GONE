using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostPreload : MonoBehaviour
{
    private void Start()
    {
        // Loads main menu after Awake
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

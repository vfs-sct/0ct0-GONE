using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameHUD : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab;
    public void OnEsc(InputValue value)
    {
        //TODO: Needs a check for if gamestate is currently paused
        PausePrefab.SetActive(true);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

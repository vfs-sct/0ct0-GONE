//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Codex : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab;

    public void OnEsc(InputValue value)
    {
        SwitchViewTo(PausePrefab);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

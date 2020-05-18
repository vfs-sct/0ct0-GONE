//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Codex : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab;
    [SerializeField] VerticalLayoutGroup content;
    [SerializeField] GameObject defaultHeader;
    [SerializeField] GameObject defaultButton;
    [SerializeField] TextMeshProUGUI entryTitleText;
    [SerializeField] TextMeshProUGUI entryBodyText;


    Dictionary<string, string> logEntries = new Dictionary<string, string>
    {
        {"Log One", "Log one text"},
        {"Log Two", "Log two text"},
        {"Log Three", "Log three text"},
        {"Log Four", "Log four text"},
        {"Log Five", "Log five text"},
        {"Log Six", "Log six text"},
        {"Log Seven", "Log seven text"},
        {"Log Eight", "Log eight text"},
    };

    public Button[] tutorialEntries;

    private void Start()
    {
        
    }

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

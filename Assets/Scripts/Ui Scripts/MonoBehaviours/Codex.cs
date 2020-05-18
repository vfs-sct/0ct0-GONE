//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Codex : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab;
    [SerializeField] VerticalLayoutGroup contentGroup;
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

    Dictionary<string, string> tutorialEntries = new Dictionary<string, string>
    {
        {"Movement", "Log one text"},
        {"Tools", "Log two text"},
        {"Resources", "Log three text"},
        {"Etc", "Log four text"},
    };

    private void Start()
    {
        AddNewHeader("Memory Logs");

        foreach (var kvp in logEntries)
        {

        }

        AddNewHeader("User Manual");

        foreach (var kvp in logEntries)
        {

        }
    }

    public void AddNewHeader(string headerText)
    {
        var newHeader = Instantiate(defaultHeader);
        newHeader.transform.SetParent(contentGroup.transform);
        newHeader.GetComponentInChildren<TextMeshProUGUI>().SetText(headerText);
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

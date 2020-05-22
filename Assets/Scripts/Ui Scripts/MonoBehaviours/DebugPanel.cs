﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup vLayoutGroup = null;
    [SerializeField] GameObject defaultText = null;
    [SerializeField] GameObject defaultButton = null;

    [SerializeField] Resource[] resourceList = null;
    [SerializeField] TMP_Dropdown resourceDropDown = null;


    [SerializeField] List<string> resourceNamesList = new List<string>();

    void Awake()
    {
        foreach (var resource in resourceList)
        {
            resourceNamesList.Add(resource.DisplayName);
        }
        resourceDropDown.options.Clear();


        resourceDropDown.AddOptions(resourceNamesList);


        
        //dropDown..AddOptions(resourceNamesList);

        //EXAMPLE
        AddNewText("Debug info here!");
        AddNewText("Stat:" + " 400");
        AddNewButton("Do thing!", () => { });
        AddNewButton("Something else!", () => { });
    }

    public void AddNewText(string headerText)
    {
        var newHeader = Instantiate(defaultText);
        newHeader.transform.SetParent(vLayoutGroup.transform);
        newHeader.GetComponentInChildren<TextMeshProUGUI>().SetText(headerText);
    }

    //add a button with a label and onclick function
    public void AddNewButton(string buttonText, System.Action buttonFunction)
    {
        var newButton = Instantiate(defaultButton);

        newButton.transform.SetParent(vLayoutGroup.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

        newButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            //pass in the function you want the button to do when clicked
            buttonFunction();
        });
    }

    //tilde to close
    public void OnDebug(InputValue value)
    {
        gameObject.SetActive(false);
    }
}

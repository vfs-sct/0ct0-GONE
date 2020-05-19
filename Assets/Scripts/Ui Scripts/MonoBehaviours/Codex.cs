//Kristin Ruff-Frederickson | Copyright 2020©

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Codex : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab;
    //contentGroup is a navigation bar I have on the side to contain all my buttons to entries
    [SerializeField] VerticalLayoutGroup contentGroup;

    //defaultHeader and defaultButton are game object prefabs I have in unity with all the style settings I want for a button or section header
    [SerializeField] GameObject defaultHeader;
    [SerializeField] GameObject defaultButton;

    //entryTitleText and entryBodyText are the text areas where I'm displaying the codex entry content.
    //I'll just rewrite the text here whenever the user clicks a new entry button to display that entry.
    [SerializeField] TextMeshProUGUI entryTitleText;
    [SerializeField] TextMeshProUGUI entryBodyText;

    //Here we'll put all our codex entries in a string Dictionary. The first string will be the title of the entry,
    //and the second will be the body
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

    //I also have tutorial entries that I want to keep separate from story entries.
    //We can create a dictionary for each section.
    Dictionary<string, string> tutorialEntries = new Dictionary<string, string>
    {
        {"Movement", "Log one text"},
        {"Tools", "Log two text"},
        {"Resources", "Log three text"},
        {"Etc", "Log four text"},
    };

    private void Start()
    {
        //I have a scrollable content view in my codex that creates all the buttons to specific entries. Here I have a function
        //that creates an unclickable header to break up the sections. Code for header down below.
        AddNewHeader("Memory Logs");
        
        //Now I'm going to populate the Memory Logs section with buttons to the content in the logEntries dictionary
        //Using a foreach we'll loop through every pair in the logEntries dictionary and create a button for it
        foreach (var kvp in logEntries)
        {
            AddNewButton(logEntries, kvp.Key);
        }

        //New section header
        AddNewHeader("User Manual");

        //Populate the tutorial section
        foreach (var kvp in tutorialEntries)
        {
            AddNewButton(tutorialEntries, kvp.Key);
        }
    }

    //Code to create a new section header (unclickable)
    public void AddNewHeader(string headerText)
    {
        //Here we create an instance of the template header we serialized at the top, and save it into a variable
        var newHeader = Instantiate(defaultHeader);

        //Now using that variable we put the header into the navigation bar, which has settings in the editor
        //to automatically layout and space any buttons it contains

        newHeader.transform.SetParent(contentGroup.transform);
        //Finally we get the text on the object from its children and set the text to the header title we passed in
        newHeader.GetComponentInChildren<TextMeshProUGUI>().SetText(headerText);
    }

    //Code to create a button to a codex entry
    public void AddNewButton(Dictionary<string, string> dict, string buttonText)
    {
        //Here we create an instance of the template button we serialized at the top and save it into a variable
        var newButton = Instantiate(defaultButton);

        //Now using that variable we put the button into the navigation bar, which has settings in the editor
        //to automatically layout and space any buttons it contains
        newButton.transform.SetParent(contentGroup.transform);

        //Finally we get the text on the button from its children and set the text to the name of the codex entry
        newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

        //Unlike the header, we must also add functionality to the button. My prefab is actually a game object, so we have to get the button from it 
        //first using GetComponent.
        //Buttons have a built-in "onClick" function and we'll add a listener to wait for the player to click the button to pop up our codex entry text.
        newButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            //Use the dictionary key to set the entry title
            entryTitleText.SetText(buttonText);
            //Use the dictionary key to get the dictionary value, then set the body text
            entryBodyText.SetText(dict[buttonText]);
        });
    }

    //NOTE: This setup is good for getting a codex into a game quick, but is not a good final setup if the game requires localization.
    //This is because the player-facing title string is doubled up as the key in the dictionary's key value pair.
    //For a game with localization, create a unique, non-player facing key for an entry, then create a class for that entry that contains any information
    //pertaining to it, ie, title string, body text string, unlock status bool. The dictionary used for this setup will be Dictionary<string, class>

    //One last thing is that loose strings like the "Memory Logs" and "User Manual" header titles we passed in should be moved out of code and into a 
    //localization file so we can plug them in using a variable instead

    //Below is just stuff to open/close the codex
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

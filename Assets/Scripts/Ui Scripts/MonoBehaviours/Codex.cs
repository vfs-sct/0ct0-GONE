//Kristin Ruff-Frederickson | Copyright 2020©

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;

public class Codex : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab = null;
    //contentGroup is a navigation bar I have on the side to contain all my buttons to entries
    [SerializeField] VerticalLayoutGroup contentGroup = null;

    //defaultHeader and defaultButton are game object prefabs I have in unity with all the style settings I want for a button or section header
    [SerializeField] GameObject defaultHeader = null;
    [SerializeField] GameObject defaultButton = null;

    //entryTitleText and entryBodyText are the text areas where I'm displaying the codex entry content.
    //I'll just rewrite the text here whenever the user clicks a new entry button to display that entry.
    [SerializeField] TextMeshProUGUI entryTitleText = null;
    [SerializeField] TextMeshProUGUI entryBodyText = null;

    //used to start/stop audio logs from a particular entry's codex page
    [SerializeField] GameObject buttonContainer = null;
    [SerializeField] Button playButton = null;
    [SerializeField] Button stopButton = null;

    [Header("Audio Log Sound Files")]
    [SerializeField] PlayButtonSound soundScript = null;
    public string[] audioLogFile = null;
    public string[] stopAudioLogFile = null;

    //Here we'll put all our codex entries in a string Dictionary. The first string will be the title of the entry,
    //and the second will be the body
    Dictionary<string, string> logEntries = new Dictionary<string, string>
    {
        {"Log One", "ROBOT VOICE:\nDiagnostics complete. Unit identified as model 0CT0-314. Tool and movement systems functional.\n\nROBOT VOICE:\nWARNING. Memory corruption at 77.5%. Please return unit to user for further instruction."},
        {"Log Two", "OAKLEY:\nHmm, where's that screwdriver...\n\nOCTO:\nWrrr bwu!\n\nOAKLEY:\nThanks, Octo.\n\nRUSTLING. BOLTS TURNING. A metal panel being SHUT.\n\nOAKLEY:\nThere we go, that should get you moving again... But what you really need is a new gyroscope.\n\nOCTO:\n(Inquisitive whir)\n\nOAKLEY:\nYou up for some scavenging today?\n\nOCTO:\nBwee!"},
        {"Log Three", "OAKLEY:\n(speaking on the phone)\nMhm. Mhm. The power grid-- Then how are they recycling air?\n\nMURMURING.\n\nOAKLEY:\nThat's a lot of people.\n\nSound of an electronic door OPENING and CLOSING.\n\nOCTO:\nBWEE-BUH-WEE.\n\nOAKLEY:\nOh, hey buddy, just give me a sec to finish up.\n\nOAKLEY:\nIf I'm not there in two minutes you can have my dessert.\n\nOCTO:\nBWUUU!\n\nSound of an electronic door OPENING and CLOSING.\n\nOAKLEY:\nYeah. I'll take the job."},
        {"Log Four", "OAKLEY:\nBetter start packing, we head out bright and early tomorrow.\n\nOCTO:\nBwu?\n\nOAKLEY:\nWe're going to the Exodus Field!\n\nOCTO:\nZZRT.\n\nOAKLEY:\nHey! Don't tell me you're worried.\n\nOAKLEY:\nOck, look... I know it's not the safest place in the galaxy, but this job's different from our usual gigs.\n\nOAKLEY:\nIt's... important.\n\nOCTO:\n(skeptical whir)\n\nOAKLEY:\nC'mon, think about it for a sec. The scrap there hasn’t been touched in decades.\n\nOCTO: ...\n\n\nOAKLEY:\nYou never did get a new gyroscope.\n\nOCTO:\n...BWEE!"},
        {"Log Five", "OAKLEY:\nWould you look at these sensors! The whole panel's lit up like a Christmas tree.\n\nOAKLEY:\nWhy don't we scavenge out here more often, hey pal?\n\nOCTO:\nB-bwee.\n\nOCTO:\nbbbbBBWU-BWU-BWU-BWU!\n\nOAKLEY:\nHuh? Oh you're right, I bet we could pull a whole ship's worth of metal outta that thing! Hmm, let's not forget why we're here, though.\n\nOAKLEY:\nJust about... and... there! There's the reactor. Alright, let's head in."},
        {"Log Six", "OAKLEY:\n(filtered)\nCan you laser that bit free for me?\n\nSound of a LASER CUTTING.\n\nOAKLEY:\n(filtered)\nThanks. Ok, that should about do it, and-- wait, what's that on the sensor?\n\nTHUNK.\n\nOCTO:\nBwuh?\n\nTHUNK... THUNK.\n\nTHUNK.\n\nOAKLEY:\n(filtered)\nOCTO--!\n\nCRUNCH."},
        {"Log Seven", "OAKLEY:\n(filtered)\nOck, can you hear me?\n\nOAKLEY:\n(filtered)\nDon't worry, I'll get you fixed up. Listen, you’re gonna hard reset in a moment and you'll lose memory, but you have to remember one thing.\n\nOAKLEY:\n(filtered)\nGet a message home. A lot of people are depending on the reactor on that ship, and they need to know where to find it.\n\nA COUGH.\n\nOAKLEY:\n(filtered)\nGet the message home, pal. I know I can count on you.\n\nOakley pats Octo's hull."},
        {"Log Eight", "OAKLEY:\nI know the galaxy can seem pretty broken at times, pal...\n\nOAKLEY:\nbut you just gotta keep fixin' what's in front of you.\n\nOAKLEY:\nThe rest will follow."},
    };

    private bool[] isLocked = new bool[]
    {
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true
    };

    List<GameObject> entryButtons = new List<GameObject>();

    //enable to debug unlock all entries with "C"
    //public void OnCraftHotkey(InputValue value)
    //{
    //    UnlockAllCodex();
    //}

    //make all entries locked
    public void ResetCodexLocks()
    {
        for (int i = 0; i < isLocked.Length; i++)
        {
            isLocked[i] = true;
        }
        UpdateButtons();
    }

    //make all entries unlocked
    public void UnlockAllCodex()
    {
        for (int i = 0; i < isLocked.Length; i++)
        {
            isLocked[i] = false;
        }
        UpdateButtons();
    }

    //unlock a specific entry using the index (0-7)
    public void UnlockSpecificEntry(int index)
    {
        if(isLocked[index] == true)
        {
            Debug.Log($"Codex entry {index} already unlocked");
        }
        else
        {
            isLocked[index] = true;
            UpdateButtons();
        }
    }

    //find the next entry that is locked and unlocked it
    public void UnlockNextEntry()
    {
        int unlockedCount = 0;
        for (int i = 0; i < isLocked.Length; i++)
        {
            //find a locked entry
            if(isLocked[i] == true)
            {
                //make it unlocked
                isLocked[i] = false;

                //play audiolog associated with that entry
                soundScript.OnClickPlayDialogue(audioLogFile[i]);

                //UpdateButtons();
                //exit out of function
                return;
            }
            unlockedCount++;
        }
        if(unlockedCount == isLocked.Length - 1)
        {
            Debug.LogWarning("UnlockNextEntry was called, but all codex entries are already unlocked.");
        }
    }

    private void UpdateButtons()
    {
        if (entryButtons.Count != 0)
        {
            for (int i = 0; i < isLocked.Length; i++)
            {
                if (isLocked[i] == false)
                {
                    entryButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(logEntries.Keys.ElementAt(i));
                    entryButtons[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    entryButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText("MEMORY CORRUPT");
                    entryButtons[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        else
        {
            Debug.LogWarning("Codex buttons have not yet been created");
        }
    }

    //I also have tutorial entries that I want to keep separate from story entries.
    //We can create a dictionary for each section.
    Dictionary<string, string> tutorialEntries = new Dictionary<string, string>
    {
        {"Movement", "Log one text"},
        {"Tools", "Log two text"},
        {"Resources", "Log three text"},
        {"Etc", "Log four text"},
    };

    //called by UIAwake on scene start
    private void Awake()
    {
        //I have a scrollable content view in my codex that creates all the buttons to specific entries. Here I have a function
        //that creates an unclickable header to break up the sections. Code for header down below.
        AddNewHeader("Memory Logs");

        //Now I'm going to populate the Memory Logs section with buttons to the content in the logEntries dictionary
        //Using a foreach we'll loop through every pair in the logEntries dictionary and create a button for it
        int audioLog = 0;
        foreach (var kvp in logEntries)
        {
            var newButton = AddNewButton(logEntries, kvp.Key, audioLog);
            entryButtons.Add(newButton);
            
            audioLog++;
        }
        
        //makesure buttons are properly locked/unlock
        UpdateButtons();
        
        //Debug.Log("Number of buttons created" + entryButtons.Count);

        //New section header
        AddNewHeader("User Manual");

        //Populate the tutorial section
        foreach (var kvp in tutorialEntries)
        {
            AddNewButton(tutorialEntries, kvp.Key, -1);
        }

        //hide sound buttons in main entry page bc no entry has been selected yet
        buttonContainer.SetActive(false);
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
    public GameObject AddNewButton(Dictionary<string, string> dict, string buttonText, int audioLog)
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
            //EVAN - not sure if buttons need a click sound here or if thats done in editor
            
            //Use the dictionary key to set the entry title
            entryTitleText.SetText(buttonText);
            //Use the dictionary key to get the dictionary value, then set the body text
            entryBodyText.SetText(dict[buttonText]);

            if (audioLog != -1)
            {
                playButton.onClick.RemoveAllListeners();
                playButton.onClick.AddListener(() =>
                {
                    //EVAN - not sure if buttons need a click sound here or if thats done in editor


                    //EVAN
                    //type the names of the events in the serialized field on Codex in the UI and
                    //clips should be automatically associated with the correct entry (hopefully lol)
                    soundScript.OnClickPlayDialogue(audioLogFile[audioLog]);
                });

                stopButton.onClick.RemoveAllListeners();
                stopButton.onClick.AddListener(() =>
                {
                    //EVAN - not sure if buttons need a click sound here or if thats done in editor


                    //EVAN
                    //stop sound function
                    soundScript.OnClickPlayDialogue(stopAudioLogFile[audioLog]);
                });

                buttonContainer.SetActive(true);
            }
            else
            {
                buttonContainer.SetActive(false);
            }
        });
        return newButton;
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
        Close();
    }

    public void Close()
    {
        SwitchViewTo(PausePrefab);
    }

    private void OnEnable()
    {
        UpdateButtons();
        Cursor.visible = true;
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

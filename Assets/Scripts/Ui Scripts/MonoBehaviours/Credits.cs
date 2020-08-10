//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPrefab = null;
    [SerializeField] TextMeshProUGUI rowOne = null;
    [SerializeField] TextMeshProUGUI rowTwo = null;

    //type credits in here to add to game. Can be any size, so add as many as you want
    public string[] creditNames = new string[] 
    { 
        "Josh Paquette",
        "Jesse Rougeau",
        "Kristin Ruff-Frederickson",
        "Evan Landry",
        "Andrew Icardi",
        "Roger Crusafon-Pont",
    };

    //role has to match up with the correct name entry bc dictionaries arent serializable
    public string[] roles = new string[]
    {
        "Josh Paquette",
        "Jesse Rougeau",
        "Kristin Ruff-Frederickson",
        "Evan Landry",
        "Andrew Icardi",
        "Roger Crusafon-Pont",
    };

    private void Start()
    {
        //goes through the screen and adds each one to the text object in the credits screen
        string name = "";
        string role = "";
        for(int i = 0; i < creditNames.Length; i++)
        {
            name = name + $"\n{creditNames[i]}";
            role = role + $"\n{roles[i]}";
        }
        rowOne.SetText(name);
        rowTwo.SetText(role);
    }

    public void OnEsc(InputValue value)
    {
        Close();
    }

    public void Close()
    {
        SwitchViewTo(MainMenuPrefab);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }
}

//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPrefab = null;
    [SerializeField] TextMeshProUGUI content = null;

    //type credits in here to add to game. Can be any size, so add as many as you want
    public string[] credits = new string[] 
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
        string text = "";
        foreach (var credit in credits)
        {
            text = text + $"\n{credit}";
        }
        content.SetText(text);
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

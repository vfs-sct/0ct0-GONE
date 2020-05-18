using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPrefab;
    [SerializeField] TextMeshProUGUI content;

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
        foreach (var credit in credits)
        {
            content.SetText(content.text + $"\n{credit}");
        }
    }

    public void OnEsc(InputValue value)
    {
        SwitchViewTo(MainMenuPrefab);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

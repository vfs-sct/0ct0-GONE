using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPrefab;
    [SerializeField] TextMeshProUGUI content;

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

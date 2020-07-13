using UnityEngine;

public class EndInventoryTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    public void OnInventoryHotkey()
    {
        tutorialController.NextPrompt();
    }
}

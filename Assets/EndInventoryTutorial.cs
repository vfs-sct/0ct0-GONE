using UnityEngine;

public class EndInventoryTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;
    public void OnInventoryHotkey()
    {
        tutorialController.NextPrompt(bufferTime);
    }
}

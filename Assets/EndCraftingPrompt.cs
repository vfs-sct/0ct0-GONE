using UnityEngine;

public class EndCraftingPrompt : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] GameObject craftMenu = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;
    private void Update()
    {
        if(craftMenu.activeSelf)
        {
            tutorialController.NextPrompt(bufferTime);   
        }
    }
}

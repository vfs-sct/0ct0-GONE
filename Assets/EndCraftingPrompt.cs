using UnityEngine;

public class EndCraftingPrompt : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;

    public bool craftMenuOpened = false;
    private void Update()
    {
        if(craftMenuOpened)
        {
            tutorialController.NextPrompt(bufferTime);   
        }
    }
}

using UnityEngine;

public class EndCraftingPrompt : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] GameObject craftMenu = null;
    private void Update()
    {
        if(craftMenu.activeSelf)
        {
            tutorialController.NextPrompt();   
        }
    }
}

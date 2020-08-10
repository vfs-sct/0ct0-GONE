using UnityEngine;

public class EndSalvageTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] HUDInventoryWidget inventory = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;

    private void OnEnable()
    {
        if (inventory.CalculateTotalMass() > 0)
        {
            tutorialController.NextPrompt(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.CalculateTotalMass() > 0)
        {
            tutorialController.NextPrompt(bufferTime);
        }
    }
}

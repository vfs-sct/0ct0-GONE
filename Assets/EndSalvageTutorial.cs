using UnityEngine;

public class EndSalvageTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] HUDInventoryWidget inventory = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;

    private int startMass;

    void OnEnable()
    {
        startMass = inventory.CalculateTotalMass();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.CalculateTotalMass() > startMass)
        {
            tutorialController.NextPrompt(bufferTime);
        }
    }
}

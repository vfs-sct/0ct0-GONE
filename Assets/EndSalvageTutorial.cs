using UnityEngine;

public class EndSalvageTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] HUDInventoryWidget inventory = null;

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
            tutorialController.NextPrompt();
        }
    }
}

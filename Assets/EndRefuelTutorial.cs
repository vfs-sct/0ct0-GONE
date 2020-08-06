
using UnityEngine;

public class EndRefuelTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] private UIAwake UIRoot = null;
    [SerializeField] private FillBar fuelFillBar = null;
    [SerializeField] Resource fuel;
    [SerializeField] float bufferTime = 5f;
    private ResourceInventory playerInventory;
    private float startFuel;

    void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
        startFuel = fuelFillBar.startAmount;
        if (fuel.GetInstanceValue(playerInventory) > startFuel)
        {
            tutorialController.NextPrompt(3f);
        }
    }

    private void Update()
    {
        if (fuel.GetInstanceValue(playerInventory) > startFuel)
        {
            tutorialController.NextPrompt(bufferTime);
        }
    }
}

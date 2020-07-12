
using UnityEngine;

public class EndRefuelTutorial : MonoBehaviour
{
    [SerializeField] private UIAwake UIRoot = null;
    [SerializeField] private FillBar fuelFillBar = null;
    [SerializeField] Resource fuel;
    private ResourceInventory playerInventory;
    private float startFuel;

    void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
        startFuel = fuelFillBar.startAmount;
    }

    private void Update()
    {
        if (fuel.GetInstanceValue(playerInventory) > startFuel)
        {
            this.gameObject.SetActive(false);
        }
    }
}

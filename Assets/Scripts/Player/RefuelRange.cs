using UnityEngine;
using UnityEngine.InputSystem;

public class RefuelRange : MonoBehaviour
{
    [SerializeField] private Playing playing;
    [SerializeField] private LowFuel lowFuelOverlay;
    [SerializeField] public Resource fuel = null;
    [SerializeField] public float amountAdd = 30;

    private float refuelTimer;

    private bool canRefuel = true;
    private bool isFueling = false;

    public void OnRefuelHotkey(InputAction.CallbackContext context)
    {
        isFueling = context.performed;
        refuelTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        canRefuel = playing.ActivePlayer.RefuelInRange();
        if (isFueling == true)
        {
            if (canRefuel)
            {
                refuelTimer -= Time.deltaTime;
                if (refuelTimer < 0)
                {
                    //EVAN - refuelling sound
                    var playerInv = playing.ActivePlayer.Inventory;
                    playerInv.AddResource(fuel, amountAdd);

                    var newHealth = fuel.GetInstanceValue(playerInv) / fuel.GetMaximum();
                    if (newHealth >= 0.25f)
                    {
                        lowFuelOverlay.TurnOff();
                    }

                    refuelTimer = 0.1f;
                }
            }
        }
    }
}

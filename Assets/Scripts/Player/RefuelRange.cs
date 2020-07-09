using UnityEngine;
using UnityEngine.InputSystem;

public class RefuelRange : MonoBehaviour
{

    [SerializeField] private Playing playing;
    [SerializeField] public Resource fuel = null;
    [SerializeField] public float amountAdd = 30;

    private float refuelTimer;

    private bool canRefuel = true;
    private bool isFueling = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnRefuelHotkey(InputValue value)
    {
        isFueling = value.isPressed;
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
                    playing.ActivePlayer.Inventory.AddResource(fuel, amountAdd);
                    refuelTimer = 0.1f;
                }
            }
        }
    }
}

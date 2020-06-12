using UnityEngine;
using UnityEngine.InputSystem;

public class RefuelRange : MonoBehaviour
{
    [SerializeField] public Player player = null;
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
        if(isFueling == true)
        {
            if (canRefuel)
            {
                refuelTimer -= Time.deltaTime;
                if (refuelTimer < 0)
                {
                    player.Inventory.AddResource(fuel, amountAdd);
                    refuelTimer = 0.1f;
                }
            }
        }
    }
}

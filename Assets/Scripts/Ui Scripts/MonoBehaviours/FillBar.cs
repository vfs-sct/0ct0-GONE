using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public UIAwake UIRoot = null;
    [SerializeField] public Resource fuel = null;

    [SerializeField] public Image barFill = null;
    [SerializeField] public Image barOverlay = null;
    [SerializeField] public float startHealth = 100;
    [SerializeField] public float maxHealth = 100;

    public ResourceInventory playerInventory;

    private void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
        fuel.SetMaximum(maxHealth);
        fuel.SetInstanceValue(playerInventory, startHealth);
    }

    private void Update()
    {
        float charStat = fuel.GetInstanceValue(playerInventory) / fuel.GetMaximum();

        // set fill to health percentage
        //needs a way to get the max value from the resource instead of hardcoding division number
        barFill.fillAmount = charStat;
        barOverlay.fillAmount = charStat + 1;
    }
}

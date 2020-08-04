using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public UIAwake UIRoot = null;
    [SerializeField] public Resource fuel = null;

    [SerializeField] public Image barFill = null;
    [SerializeField] public Image barOverlay = null;
    [SerializeField] public float startAmount = 100;
    [SerializeField] public float maxAmount = 100;

    public ResourceInventory playerInventory;

    private void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
        fuel.SetMaximum(maxAmount);
        fuel.SetInstanceValue(playerInventory, startAmount);
    }

    private void Update()
    {
        float charStat = fuel.GetInstanceValue(playerInventory) / fuel.GetMaximum();

        // set fill to health percentage
        //needs a way to get the max value from the resource instead of hardcoding division number
        barFill.fillAmount = charStat;
        barOverlay.fillAmount = charStat;
    }
}

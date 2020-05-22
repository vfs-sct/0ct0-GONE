using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public UIAwake UIRoot = null;
    [SerializeField] public Resource fuel = null;

    [SerializeField] public Image barFill = null;

    private ResourceInventory playerInventory;

    private void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
    }

    private void Update()
    {
        float charStat = fuel.GetInstanceValue(playerInventory);

        // set fill to health percentage
        barFill.fillAmount = charStat;
    }
}

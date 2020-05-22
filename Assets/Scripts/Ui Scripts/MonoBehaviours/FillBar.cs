using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public ResourceInventory playerInventory = null;
    [SerializeField] public Resource fuel = null;

    // the stat we're displaying
    //[SerializeField] private float charStat;

    [SerializeField] public Image barFill = null;

    private void Start()
    {
    }

    private void Update()
    {
        float charStat = fuel.GetInstanceValue(playerInventory);

        // set fill to health percentage
        barFill.fillAmount = charStat;
    }
}

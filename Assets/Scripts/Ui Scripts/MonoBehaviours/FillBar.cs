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

    [SerializeField] private Color32 white;
    [SerializeField] private Color32 damagedCol;
    private Color32 normalCol;

    //stops the update if the damaged anim is playing
    private bool isDamaged = false;
    //stop updating before the damage animation plays
    private bool preDamaged = false;

    public ResourceInventory playerInventory;

    private void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
        normalCol = barFill.color;
        fuel.SetMaximum(maxAmount);
        fuel.SetInstanceValue(playerInventory, startAmount);
    }

    private void Update()
    {
        if(preDamaged)
        {
            return;
        }

        if (isDamaged == false)
        {
            float charStat = fuel.GetInstanceValue(playerInventory) / fuel.GetMaximum();

            // set fill to health percentage
            //needs a way to get the max value from the resource instead of hardcoding division number
            barFill.fillAmount = charStat;
            barOverlay.fillAmount = charStat;
        }
        else
        {
            float charStat = fuel.GetInstanceValue(playerInventory) / fuel.GetMaximum();

            float lerp = Mathf.Lerp(barFill.fillAmount, charStat, Time.deltaTime * 1f / .3f);

            // set fill to health percentage
            //needs a way to get the max value from the resource instead of hardcoding division number
            barFill.fillAmount = lerp;
            barOverlay.fillAmount = lerp;
        }
    }

    public void Damaged()
    {
        preDamaged = true;
        StartCoroutine(FlashColor(damagedCol));
    }

    System.Collections.IEnumerator FlashColor(Color32 col)
    {
        float origBarFill = barFill.fillAmount;

        //barFill.fillAmount = fuel.GetInstanceValue(playerInventory) / fuel.GetMaximum();
        barFill.color = col;

        yield return new WaitForSeconds(.1f);

        //barFill.fillAmount = origBarFill;
        preDamaged = false;
        isDamaged = true;
        StartCoroutine(FlashWhite());
    }

    System.Collections.IEnumerator FlashWhite()
    {
        barFill.color = white;
        yield return new WaitForSeconds(.1f);
        StartCoroutine(AnimateDamage());
    }

    System.Collections.IEnumerator AnimateDamage()
    {
        barFill.color = normalCol;
        yield return new WaitForSeconds(.4f);
        isDamaged = false;
    }

}

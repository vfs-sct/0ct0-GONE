using UnityEngine;
using UnityEngine.UI;

public class GooGlueCraft : MonoBehaviour
{
    [SerializeField] GameObject tooltip = null;
    [SerializeField] ResourceInventory stationInv = null;
    [SerializeField] ResourceInventory playerInv = null;
    [SerializeField] Button naniteCraftButton = null;

    [Header("Produces:")]
    [SerializeField] Resource nanites = null;
    [SerializeField] float productAmount;

    [Header("Requires:")]
    [SerializeField] Resource ingredient = null;
    [SerializeField] float ingredientAmount;

    [Header("Sound Related")]
    public bool isSoundPlayed;

    private bool isCrafting = false;
    public void ShowTooltip()
    {
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrafting == true)
        {
            UpdateTimer();
        }
        if (playerInv.GetResource(ingredient) >= ingredientAmount)
        {
            naniteCraftButton.interactable = true;
        }
        else
        {
            naniteCraftButton.interactable = false;
        }
    }

    public void UpdateTimer()
    {
        //EVAN - a little timer dial pops up and might need a sound for when it's filling
        //if you click & hold something to craft you'll see it
        if (!isSoundPlayed)
        {
            AkSoundEngine.PostEvent("Octo_Tether_Grab", gameObject);
            isSoundPlayed = true;
        }

        //if (craftTimer != 0)
        //{
        //    timerDial.gameObject.SetActive(true);
        //    craftTimer -= Time.unscaledDeltaTime;
        //    timerDial.fillAmount = (buttonHoldTime - craftTimer) / buttonHoldTime;
        //    if (craftTimer <= 0)
        //    {
        //        DoCraft();
        //        craftTimer = 0;
        //        timerDial.gameObject.SetActive(false);
        //        timerDial.fillAmount = 0f;
        //        isCrafting = false;
        //    }
        //}
    }
}

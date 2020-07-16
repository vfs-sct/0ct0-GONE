using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GooGlueCraft : MonoBehaviour
{
    [SerializeField] CraftingModule CraftingModule = null;
    [SerializeField] GameObject tooltip = null;
    [SerializeField] ResourceInventory stationInv = null;
    [SerializeField] InventoryController playerInv = null;
    [SerializeField] ResourceInventory playerResourceInv = null;
    [SerializeField] Button naniteCraftButton = null;
    [SerializeField] GameObject poptext = null;
    private string popTextMSG = null;

    [Header("Recipe:")]
    [SerializeField] ConsumableRecipe naniteRecipe = null;

    [Header("Sound Related")]
    public bool isSoundPlayed;

    [SerializeField] Image timerDial = null;
    [SerializeField] float buttonHoldTime;
    private float craftTimer = 0f;
    private bool isCrafting = false;

    private void Start()
    {
        EventTrigger trigger = naniteCraftButton.GetComponent<EventTrigger>();

        //clear any triggers from previous recipes
        trigger.triggers.Clear();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) =>
        {
            if (!CraftingModule.CanCraftConsumable(stationInv, playerInv, playerResourceInv, naniteRecipe))
            {
                return;
            }
            popTextMSG = $"{naniteRecipe.DisplayName} crafted";
            var pointerData = (PointerEventData)eventData;
            timerDial.transform.position = pointerData.position;
            HoldTimer();
        });
    }
    public void HoldTimer()
    {
        isCrafting = true;
        craftTimer = buttonHoldTime;
    }
    public void ShowTooltip()
    {
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrafting == true)
        {
            UpdateTimer();
        }
        if (stationInv.GetResource(naniteRecipe.ResourceInput[0].resource) >= naniteRecipe.ResourceInput[0].amount)
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

        if (craftTimer != 0)
        {
            timerDial.gameObject.SetActive(true);
            craftTimer -= Time.unscaledDeltaTime;
            timerDial.fillAmount = (buttonHoldTime - craftTimer) / buttonHoldTime;
            if (craftTimer <= 0)
            {
                //DoCraft();
                craftTimer = 0;
                timerDial.gameObject.SetActive(false);
                timerDial.fillAmount = 0f;
                isCrafting = false;
            }
        }
    }
}

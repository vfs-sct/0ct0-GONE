using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GooGlueCraft : MonoBehaviour
{
    [SerializeField] CraftingModule CraftingModule = null;
    [SerializeField] GameObject tooltip = null;
    [SerializeField] ResourceInventory stationInv = null;
    [SerializeField] InventoryController playerInv = null;
    [SerializeField] ResourceInventory playerResourceInv = null;
    [SerializeField] Button naniteCraftButton = null;
    [SerializeField] GameObject poptextGO = null;
    private string popTextMSG = null;

    [Header("Recipe:")]
    [SerializeField] ConsumableRecipe naniteRecipe = null;

    [Header("Sound Related")]
    public bool isSoundPlayed;

    [SerializeField] Image timerDial = null;
    [SerializeField] float buttonHoldTime = 0.5f;
    private float craftTimer = 0f;
    private bool isCrafting = false;
    private TextMeshProUGUI tooltipText = null;

    private void Start()
    {
        tooltipText = tooltip.GetComponent<TextMeshProUGUI>();
        EventTrigger trigger = naniteCraftButton.GetComponent<EventTrigger>();

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

        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((eventData) =>
        {
            ReleaseTimer();
        });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) =>
        {
            ShowTooltip();
        });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) =>
        {
            HideTooltip();
        });
        trigger.triggers.Add(entry);
    }
    public void HoldTimer()
    {
        isCrafting = true;
        craftTimer = buttonHoldTime;
    }
    public void ReleaseTimer()
    {
        //Debug.Log("Release Timer");
        craftTimer = 0;
        timerDial.gameObject.SetActive(false);
        timerDial.fillAmount = 0f;
        isCrafting = false;
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
        if (CraftingModule.CanCraftConsumable(stationInv, playerInv, playerResourceInv, naniteRecipe))
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
                DoCraft();
                var popText = Instantiate(poptextGO);
                popText.GetComponentInChildren<TextMeshProUGUI>().SetText("+30 Repair Paste");
                popText.transform.SetParent(naniteCraftButton.transform);
                popText.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                craftTimer = 0;
                timerDial.gameObject.SetActive(false);
                timerDial.fillAmount = 0f;
                isCrafting = false;
            }
        }
    }

    private void DoCraft()
    {
        //EVAN - some sort of ding or "crafting complete!" sound
        AkSoundEngine.PostEvent("Crafting_Success", gameObject);
        CraftingModule.CraftConsumable(stationInv, playerInv, playerResourceInv, naniteRecipe);
        //var poptext = Instantiate(popText);
        //poptext.popText.SetText($"{queuedRecipe.DisplayName} crafted");
        //poptext.gameObject.transform.SetParent(CraftButton.transform);
        //poptext.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        //var amountOwned = playerInventory.GetItemAmount(queuedRecipe.Output.item);
        //if (amountOwned < 0)
        //{
        //    amountOwned = 0;
        //}
        ////show the player how many of the output item they already have
        //amountInInventory.SetText($"{queuedRecipe.Output.item.Name} in inventory: {amountOwned}");

        //storageDials.UpdateDials();
        //UpdateOwnedAmounts();

        //queuedRecipe = null;
        popTextMSG = null;
        isSoundPlayed = false;
    }
}

//Kristin Ruff-Frederickson | Copyright 2020©
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class CraftingSatellite : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] CraftingModule CraftingModule = null;
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] GameObject HUDPrefab = null;
    [SerializeField] ResourceInventory shipInventory = null;
    [SerializeField] ShipStorageHUD storageDials = null;

    [SerializeField] GameObject[] contentGroups = null;

    [Header("Crafting Panel")]
    [SerializeField] Crafting mainCrafting = null;
    [SerializeField] Button CraftButton = null;
    [SerializeField] TextMeshProUGUI TitleText = null;
    [SerializeField] HorizontalLayoutGroup ProductGroup = null;
    [SerializeField] GameObject RequiresText = null;
    [SerializeField] HorizontalLayoutGroup IngredientGroup = null;
    [SerializeField] Image timerDial = null;
    [SerializeField] float buttonHoldTime;

    [Header("Recipe Categories")]
    [SerializeField] SatelliteRecipe[] Satellites = null;

    [Header("Code Generated Object Templates")]
    //default button used to make all the buttons in the recipe tabs
    [SerializeField] Button RecipeButton = null;
    [SerializeField] GameObject Product = null;
    [SerializeField] GameObject Ingredient = null;
    [SerializeField] TextMeshProUGUI amountInInventory = null;
    [SerializeField] ResourceGainedPopTxt popText = null;

    //associate number of owned ingredients with resource/ingredient
    Dictionary<TextMeshProUGUI, Resource> TextToResource = new Dictionary<TextMeshProUGUI, Resource>();
    Dictionary<TextMeshProUGUI, Item> TextToItem = new Dictionary<TextMeshProUGUI, Item>();

    private SatelliteRecipe currentRecipe;
    private TextMeshProUGUI craftButtonText = null;
    private Color interactableTextCol;
    private Color uninteractableTextCol;

    private InventoryController playerInventory;
    private SatelliteInventory satInventory;

    private float craftTimer = 0f;
    private string popTextMSG = null;
    private SatelliteRecipe queuedRecipe = null;
    public bool isCrafting = false;

    private void OnEnable()
    {
        UpdateOwnedAmounts();
    }

    void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();
        satInventory = UIRoot.GetPlayer().GetComponent<SatelliteInventory>();

        //save craft text and set up craft button as uninteractable
        craftButtonText = CraftButton.GetComponentInChildren<TextMeshProUGUI>();

        //set up text colours
        interactableTextCol = craftButtonText.color;
        uninteractableTextCol = new Color(interactableTextCol.r, interactableTextCol.g, interactableTextCol.b, 0.3f);

        //fill in each of the panels
        PopulateRecipePanel(Satellites, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrafting == true)
        {
            UpdateTimer();
        }
        if (contentGroups[0].activeSelf)
        {
            if (currentRecipe != null && CraftingModule.CanCraftSatellite(shipInventory, playerInventory, satInventory, currentRecipe))
            {
                mainCrafting.canSatCraft = true;
            }
            else
            {
                mainCrafting.canSatCraft = false;
            }

            mainCrafting.UpdateCraftButton();
        }
        else
        {
            mainCrafting.canSatCraft = false;
        }
    }

    //crafting screen can either be closed with ESC or the hotkey to open it (or clicking the close button on the panel)
    public void OnEsc(InputValue value)
    {
        Close();
    }

    public void OnCraftHotkey(InputValue value)
    {
        Close();
    }

    public void Close()
    {
        GameManager.UnPause();
        SwitchViewTo(HUDPrefab);
    }

    //use an array of recipe objects to generate crafting screen buttons & their associated functionality
    public void PopulateRecipePanel(SatelliteRecipe[] recipeList, int contentGroup)
    {
        foreach (var recipe in recipeList)
        {
            var newButton = AddNewButton(recipe.DisplayName, contentGroups[contentGroup].GetComponent<VerticalLayoutGroup>());

            //set up what the recipe button does when you click it - used to fill in all the
            //recipe info on the crafting panel (# of ingredients, names, amount needed, etc)
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                TitleText.SetText(recipe.DisplayName);
                if (RequiresText.activeSelf != true)
                {
                    TitleText.gameObject.SetActive(true);
                    RequiresText.SetActive(true);
                    amountInInventory.gameObject.SetActive(true);
                }

                int childCount = ProductGroup.transform.childCount;

                //REFRESH CRAFTING PANEL AFTER CLICKING A RECIPE
                //remove the "products" of the previous recipe
                for (int i = 0; i < childCount; i++)
                {
                    Destroy(ProductGroup.transform.GetChild(i).gameObject);
                }

                childCount = IngredientGroup.transform.childCount;

                //remove the "ingredients" of the previous recipe
                for (int i = 0; i < childCount; i++)
                {
                    //Debug.Log("INGREDIENT:" + IngredientGroup.transform.GetChild(0).GetComponentsInChildren<TextMeshProUGUI>()[0].text);
                    Destroy(IngredientGroup.transform.GetChild(i).gameObject);
                }

                //add new products and ingredients
                var product = Instantiate(Product);
                product.transform.SetParent(ProductGroup.transform);
                product.GetComponentInChildren<Image>().sprite = recipe.Output.satIcon;
                //there's two text portions on the UI element, the name and the amount
                var outputText = product.GetComponentsInChildren<TextMeshProUGUI>();
                outputText[0].SetText(recipe.DisplayName);
                outputText[1].SetText("x1");
                outputText[2].SetText(recipe.ItemDesc);

                //show the player how many of the output item they already have
                var ownedSat = satInventory.GetSatellite();
                if (ownedSat != null && ownedSat == recipe.Output)
                {
                    amountInInventory.SetText($"{recipe.DisplayName} in inventory: 1");
                }
                else
                {
                    amountInInventory.SetText($"{recipe.DisplayName} in inventory: 0");
                }

                foreach (var input in recipe.ResourceInput)
                {
                    //create ingredient box
                    var ingredient = Instantiate(Ingredient);
                    ingredient.transform.SetParent(IngredientGroup.transform);

                    //set the icon on the box to the resource icon
                    var resourceIcon = input.resource.resourceIcon;
                    if (resourceIcon != null)
                    {
                        var image = ingredient.GetComponentInChildren<Image>();
                        image.sprite = resourceIcon;
                        image.color = input.resource.ResourceColor;
                    }

                    var inputText = ingredient.GetComponentsInChildren<TextMeshProUGUI>();

                    inputText[0].SetText(input.resource.DisplayName);
                    inputText[0].color = input.resource.ResourceColor;
                    //adding owned amount to dictionary for updating
                    TextToResource.Add(inputText[1], input.resource);
                    inputText[2].SetText($"/ {input.amount}");
                }

                foreach (var input in recipe.ItemInput)
                {
                    //create ingredient box
                    var ingredient = Instantiate(Ingredient);
                    ingredient.transform.SetParent(IngredientGroup.transform);

                    //set the icon on the box to the resource icon
                    var itemIcon = input.item.Icon;
                    if (itemIcon != null)
                    {
                        ingredient.GetComponentInChildren<Image>().sprite = itemIcon;
                    }

                    var inputText = ingredient.GetComponentsInChildren<TextMeshProUGUI>();

                    inputText[0].SetText(input.item.Name);
                    //adding owned amount to dictionary for updating
                    TextToItem.Add(inputText[1], input.item);
                    inputText[2].SetText($"/ {input.amount}");
                }

                //show how much of each mat they have
                UpdateOwnedAmounts();

                CraftButton.gameObject.SetActive(true);
                currentRecipe = recipe;
                //change what the craft button does
                CraftButton.onClick.RemoveAllListeners();

                EventTrigger trigger = CraftButton.GetComponent<EventTrigger>();

                //clear any triggers from previous recipes
                trigger.triggers.Clear();

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) =>
                {
                    if(!CraftingModule.CanCraftSatellite(shipInventory, playerInventory, satInventory, currentRecipe))
                    {
                        return;
                    }
                    queuedRecipe = recipe;
                    popTextMSG = $"{recipe.DisplayName} crafted";
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
            });
        }
    }

    public void UpdateOwnedAmounts()
    {
        foreach (var kvp in TextToResource)
        {
            kvp.Key.SetText(shipInventory.GetResource(kvp.Value).ToString());
        }
        foreach (var kvp in TextToItem)
        {
            int amount = playerInventory.GetItemAmount(kvp.Value);
            if (amount < 0) amount = 0;
            kvp.Key.SetText(amount.ToString());
        }
    }

    public void UpdateTimer()
    {
        if (craftTimer != 0)
        {
            timerDial.gameObject.SetActive(true);
            craftTimer -= Time.unscaledDeltaTime;
            timerDial.fillAmount = (buttonHoldTime - craftTimer) / buttonHoldTime;
            if (craftTimer <= 0)
            {
                DoCraft();
                craftTimer = 0;
                timerDial.gameObject.SetActive(false);
                timerDial.fillAmount = 0f;
                isCrafting = false;
            }
        }
    }

    public void SetCraftInfo(SatelliteRecipe recipe, string popTextMSG)
    {
        queuedRecipe = recipe;
        popTextMSG = $"{recipe.DisplayName} crafted";
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

    private void DoCraft()
    {
        CraftingModule.CraftSatellite(shipInventory, playerInventory, satInventory, queuedRecipe);
        var poptext = Instantiate(popText);
        poptext.popText.SetText($"{queuedRecipe.DisplayName} crafted");
        poptext.gameObject.transform.SetParent(CraftButton.transform);
        poptext.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        //show the player how many of the output item they already have
        var ownedSat = satInventory.GetSatellite();
        if (ownedSat != null && ownedSat == queuedRecipe.Output)
        {
            amountInInventory.SetText($"{queuedRecipe.DisplayName} in inventory: 1");
        }
        else
        {
            amountInInventory.SetText($"{queuedRecipe.DisplayName} in inventory: 0");
        }

        storageDials.UpdateDials();
        UpdateOwnedAmounts();

        queuedRecipe = null;
        popTextMSG = null;
    }

    //instantiate a new button and put it in the sidebar
    public Button AddNewButton(string buttonText, VerticalLayoutGroup contentGroup)
    {
        var newButton = Instantiate(RecipeButton);

        newButton.transform.SetParent(contentGroup.transform);

        newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

        return newButton;
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

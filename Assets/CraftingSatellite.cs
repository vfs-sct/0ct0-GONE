//Kristin Ruff-Frederickson | Copyright 2020©
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class CraftingSatellite : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] private SaveFile saveFile = null;
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

    //associate button with its recipe
    Dictionary<Button, SatelliteRecipe> ButtonToRecipe = new Dictionary<Button, SatelliteRecipe>();

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

    [Header("Sound Related")]
    public bool isSoundPlayed;

    private void OnEnable()
    {
        if(currentRecipe != null)
        {
            //show the player how many of the output item they already have
            if (satInventory.GetSatellite() != null)
            {
                amountInInventory.SetText($"Satellite Inventory: <b><color=#FF1B00>FULL</color></b>\nCurrently Equipped: {satInventory.GetSatellite().DisplayName}");
            }
            else
            {
                amountInInventory.SetText($"Satellite Inventory: <b><color=#06FF00>Empty</color></b>");
            }
        }
        UpdateOwnedAmounts();
        UpdateCraftableRecipes();
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

        if(currentRecipe == null)
        {
            mainCrafting.canSatCraft = false;
            return;
        }

        if (CraftingModule.CanCraftSatellite(shipInventory, playerInventory, satInventory, currentRecipe))
        {
            mainCrafting.canSatCraft = true;
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
            //EVAN - this is where you place a sound for clicking a specific recipe button
            AkSoundEngine.PostEvent("MainMenu_Button_Play", gameObject);

            var newButton = AddNewButton(recipe.DisplayName, contentGroups[contentGroup].GetComponent<VerticalLayoutGroup>());

            //save association between button and its recipe so we can sort recipes by which ones you have the ingredients for
            ButtonToRecipe.Add(newButton, recipe);
            if (!CraftingModule.CanCraftSatellite(shipInventory, playerInventory, satInventory, recipe))
            {
                newButton.gameObject.GetComponent<Image>().color = newButton.colors.disabledColor;
            }

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
                if (ownedSat != null)
                {
                    amountInInventory.SetText($"Satellite Inventory: <b><color=#FF1B00>FULL</color></b>\nCurrently Equipped: {ownedSat.DisplayName}");
                }
                else
                {
                    amountInInventory.SetText($"Satellite Inventory: <b><color=#06FF00>Empty</color></b>");
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

                    var tooltip = ingredient.GetComponent<GetTooltip>().GetTooltipScript();
                    tooltip.GetTitle().color = input.resource.ResourceColor;
                    tooltip.SetTitle(input.resource.DisplayName);
                    tooltip.SetDesc(input.resource.Desc);

                    //tooltips for resources dont have the extra panel for ingredients at the bottom
                    var sizeDelta = tooltip.GetBkImg().GetComponent<RectTransform>().sizeDelta;
                    sizeDelta.y -= 50;
                    tooltip.GetBkImg().GetComponent<RectTransform>().sizeDelta = sizeDelta;
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

                    var tooltip = ingredient.GetComponent<GetTooltip>().GetTooltipScript();
                    tooltip.SetTitle(input.item.Name);
                    tooltip.SetDesc(input.item.ItemDesc);

                    tooltip.AddSubIngredients(input.item.CraftingRecipe, shipInventory, playerInventory);
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
                    //EVAN - this is where clicking the "craft" button first happens - you already have a sound further down for
                    //holding the craft button down

                    if (!CraftingModule.CanCraftSatellite(shipInventory, playerInventory, satInventory, currentRecipe))
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
        //after instantiating all buttons, sort them all by which can be crafted
        UpdateCraftableRecipes();
    }

    //update recipe buttons to show which ones the player has the materials to craft
    public void UpdateCraftableRecipes()
    {
        foreach (var kvp in ButtonToRecipe)
        {
            if (kvp.Key.gameObject.activeSelf)
            {
                if (!CraftingModule.CanCraftSatellite(shipInventory, playerInventory, satInventory, kvp.Value))
                {
                    //set to uncraftable color
                    kvp.Key.gameObject.GetComponent<Image>().color = kvp.Key.colors.disabledColor;

                    //re-add to the content group so it goes to the bottom of the list (making craftable ones stay on top)
                    kvp.Key.transform.SetAsLastSibling();
                }
                else
                {
                    kvp.Key.gameObject.GetComponent<Image>().color = new Color(0.745283f, 0.5156953f, 0, 1);
                }
            }
        }
    }

    public void UpdateOwnedAmounts()
    {
        var activeResource = shipInventory.GetActiveResourceList();
        for (int i = 0; i < activeResource.Count; i++)
        {
            saveFile.hubResource[i] = shipInventory.GetResource(activeResource.ElementAt(i));
            saveFile.Save();
        }
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
        if (!isSoundPlayed)
        {
            AkSoundEngine.PostEvent("Octo_Repair_Start", gameObject);
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
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void DoCraft()
    {
        //EVAN - some sort of ding or "crafting complete!" sound
        AkSoundEngine.PostEvent("Crafting_Success", gameObject);
        CraftingModule.CraftSatellite(shipInventory, playerInventory, satInventory, queuedRecipe);
        var poptext = Instantiate(popText);
        poptext.popText.SetText($"{queuedRecipe.DisplayName} crafted");
        poptext.gameObject.transform.SetParent(CraftButton.transform);
        poptext.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        //show the player how many of the output item they already have
        var ownedSat = satInventory.GetSatellite();
        if (ownedSat != null)
        {
            amountInInventory.SetText($"Satellite Inventory: <b><color=#FF1B00>FULL</color></b>\nCurrently Equipped: {queuedRecipe.DisplayName}");
        }
        else
        {
            amountInInventory.SetText($"Satellite Inventory: <b><color=#06FF00>Empty</color></b>");
        }

        storageDials.UpdateDials();
        UpdateOwnedAmounts();
        UpdateCraftableRecipes();
        mainCrafting.UpdateCraftableRecipes();

        EventSystem.current.SetSelectedGameObject(null);

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

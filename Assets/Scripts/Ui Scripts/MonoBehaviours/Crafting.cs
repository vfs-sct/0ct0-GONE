//Kristin Ruff-Frederickson | Copyright 2020©
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Crafting : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] CraftingModule CraftingModule = null;
    [SerializeField] UIAwake UIRoot = null; 
    [SerializeField] GameObject HUDPrefab = null;
    [SerializeField] ResourceInventory shipInventory = null;

    [Header("Recipe Panels and Tabs")]
    //arrays for the tier tabs and their associated recipe button panels
    [SerializeField] Button[] tabButtons = null;
    [SerializeField] GameObject[] contentGroups = null;

    [Header("Crafting Panel")]
    [SerializeField] Button CraftButton = null;
    [SerializeField] TextMeshProUGUI TitleText = null;
    [SerializeField] HorizontalLayoutGroup ProductGroup = null;
    [SerializeField] GameObject RequiresText = null;
    [SerializeField] HorizontalLayoutGroup IngredientGroup = null;

    [Header("Recipe Categories")]
    [SerializeField] Recipe[] Components = null;
    [SerializeField] Recipe[] AdvancedComponents = null;
    [SerializeField] Recipe[] Upgrades= null;
    [SerializeField] Recipe[] Satellites = null;

    [Header("Code Generated Object Templates")]
    //default button used to make all the buttons in the recipe tabs
    [SerializeField] Button RecipeButton = null;
    [SerializeField] GameObject Product = null;
    [SerializeField] GameObject Ingredient = null;

    //associate tab buttons with their tab panel
    Dictionary<GameObject, Button> PanelToButton = new Dictionary<GameObject, Button>();

    private Recipe currentRecipe;

    private InventoryController playerInventory;
    void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();

        //correlate tab panels with tab buttons - note, buttons and content groups need to be in the correct order in editor
        for(int i = 0; i < tabButtons.Length; i++)
        {
            PanelToButton[contentGroups[i]] = tabButtons[i];
        }

        //fill in each of the panels
        PopulateRecipePanel(Components, 0);
        PopulateRecipePanel(AdvancedComponents, 1);
        PopulateRecipePanel(Upgrades, 2);
        PopulateRecipePanel(Satellites, 3);

        //set the default panel to active
        SwitchActiveTab(contentGroups[0]);
    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(currentRecipe != null && CraftingModule.CanCraft(shipInventory, playerInventory, playerInventory, currentRecipe))
       {
            CraftButton.interactable = true;
       }
       else
       {
            CraftButton.interactable = false;
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

    //this function is used in-editor on the tab buttons directly
    public void SwitchActiveTab(Object active_panel)
    {
        //turn on the panel we want and turn off its associated button
        //anything that isn't the panel we want gets turned off and turns its button on
        foreach (var kvp in PanelToButton)
        {
            if (kvp.Key == active_panel)
            {
                kvp.Key.SetActive(true);
                kvp.Value.GetComponentInChildren<Button>().interactable = false;
            }
            else
            {
                kvp.Key.SetActive(false);
                kvp.Value.GetComponentInChildren<Button>().interactable = true;
            }
        }
    }

    //use an array of recipe objects to generate crafting screen buttons & their associated functionality
    public void PopulateRecipePanel(Recipe[] recipeList, int contentGroup)
    {
        foreach (var recipe in recipeList)
        {
            var newButton = AddNewButton(recipe.DisplayName, contentGroups[contentGroup].GetComponent<VerticalLayoutGroup>());

            //set up what the recipe button does when you click it - used to fill in all the
            //recipe info on the crafting panel (# of ingredients, names, amount needed, etc)
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                TitleText.SetText(recipe.DisplayName);
                RequiresText.SetActive(true);

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
                //there's two text portions on the UI element, the name and the amount
                var outputText = product.GetComponentsInChildren<TextMeshProUGUI>();
                outputText[0].SetText(recipe.DisplayName);
                outputText[1].SetText("x" + recipe.Output.amount.ToString());

                foreach (var input in recipe.ResourceInput)
                {
                    //create ingredient box
                    var ingredient = Instantiate(Ingredient);
                    ingredient.transform.SetParent(IngredientGroup.transform);

                    //set the icon on the box to the resource icon
                    var resourceIcon = input.resource.resourceIcon;
                    if (resourceIcon != null)
                    {
                        ingredient.GetComponentInChildren<Image>().sprite = resourceIcon;
                    }

                    var inputText = ingredient.GetComponentsInChildren<TextMeshProUGUI>();

                    inputText[0].SetText(input.resource.DisplayName);
                    inputText[1].SetText("x" + input.amount.ToString());
                }

                CraftButton.gameObject.SetActive(true);
                currentRecipe = recipe;
                //change what the craft button does
                CraftButton.onClick.RemoveAllListeners();
                CraftButton.onClick.AddListener(() =>
                {
                   CraftingModule.CraftItem(shipInventory, playerInventory, playerInventory, recipe);
                });

            });
        }
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

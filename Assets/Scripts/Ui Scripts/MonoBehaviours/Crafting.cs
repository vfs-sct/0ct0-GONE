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

    [SerializeField] Button[] tabButtons = null;
    [SerializeField] GameObject[] contentGroups = null;

    [SerializeField] Button CraftButton = null;
    [SerializeField] TextMeshProUGUI TitleText = null;
    [SerializeField] HorizontalLayoutGroup ProductGroup = null;
    [SerializeField] GameObject RequiresText = null;
    [SerializeField] HorizontalLayoutGroup IngredientGroup = null;

    [Header("Recipe Tiers")]
    [SerializeField] CraftingRecipe[] T0Recipes = null;
    [SerializeField] CraftingRecipe[] T1Recipes = null;
    [SerializeField] CraftingRecipe[] T2Recipes = null;
    [SerializeField] CraftingRecipe[] T3Recipes = null;

    [Header("Code Generated Object Templates")]
    //default button used to make all the buttons in the recipe tabs
    [SerializeField] Button RecipeButton = null;
    [SerializeField] GameObject Product = null;
    [SerializeField] GameObject Ingredient = null;

    //associate tab buttons with their tab panel
    Dictionary<GameObject, Button> PanelToButton = new Dictionary<GameObject, Button>();

    private CraftingRecipe currentRecipe;

    private ResourceInventory playerInventory;
    void Awake()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();

        //correlate tab panels with tab buttons - note, buttons and content groups need to be in the correct order in editor
        for(int i = 0; i < tabButtons.Length; i++)
        {
            PanelToButton[contentGroups[i]] = tabButtons[i];
        }

        //fill in each of the panels
        PopulateRecipePanel(T0Recipes);
        //PopulateRecipePanel(T1Recipes);
        //PopulateRecipePanel(T2Recipes);
        //PopulateRecipePanel(T3Recipes);

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
        if(currentRecipe != null && CraftingModule.CanCraft(playerInventory, playerInventory, currentRecipe))
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

    public void SwitchActiveTab(GameObject active_panel)
    {
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
    public void PopulateRecipePanel(CraftingRecipe[] recipeList)
    {
        foreach (var recipe in recipeList)
        {
            var newButton = AddNewButton(recipe.DisplayName, contentGroups[0].GetComponent<VerticalLayoutGroup>());

            //set up what the recipe button does when you click it - used to fill in all the
            //recipe info on the crafting panel (# of ingredients, names, amount needed, etc)
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                TitleText.SetText(recipe.DisplayName);
                RequiresText.SetActive(true);

                int childCount = ProductGroup.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Destroy(ProductGroup.transform.GetChild(0).gameObject);
                }

                childCount = IngredientGroup.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Destroy(IngredientGroup.transform.GetChild(0).gameObject);
                }

                //add new inputs and outputs
                var product = Instantiate(Product);
                product.transform.SetParent(ProductGroup.transform);
                var outputText = product.GetComponentsInChildren<TextMeshProUGUI>();
                outputText[0].SetText(recipe.DisplayName);
                outputText[1].SetText("x" + recipe.Output.amount.ToString());

                foreach (var input in recipe.Input)
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
                    CraftingModule.CraftItem(playerInventory, playerInventory, recipe);
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

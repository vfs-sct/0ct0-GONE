﻿using System.Collections.Generic;
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
    [SerializeField] CraftingRecipe[] T0Recipes;
    [SerializeField] CraftingRecipe[] T1Recipes;
    [SerializeField] CraftingRecipe[] T2Recipes;
    [SerializeField] CraftingRecipe[] T3Recipes;

    [Header("Code Generated Object Templates")]
    //default button used to make all the buttons in the recipe tabs
    [SerializeField] Button RecipeButton = null;
    [SerializeField] GameObject Product = null;
    [SerializeField] GameObject Ingredient = null;

    //associate tab buttons with their tab panel
    Dictionary<GameObject, Button> PanelToButton = new Dictionary<GameObject, Button>();

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

    //update numbers related to player inventory here
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public System.Action closeCallback;

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

    public void PopulateRecipePanel(CraftingRecipe[] recipeList)
    {
        foreach (var recipe in recipeList)
        {
            var newButton = AddNewButton(recipe.DisplayName, contentGroups[0].GetComponent<VerticalLayoutGroup>());

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

                foreach (var input in recipe.Input)
                {
                    var ingredient = Instantiate(Ingredient);
                    ingredient.transform.SetParent(IngredientGroup.transform);
                }

                CraftButton.gameObject.SetActive(true);
                //change what the craft button does
                CraftButton.onClick.RemoveAllListeners();
                CraftButton.onClick.AddListener(() =>
                {
                    CraftingModule.CraftItem(playerInventory, playerInventory, recipe);
                });

            });
        }
    }

    public Button AddNewButton(string buttonText, VerticalLayoutGroup contentGroup)
    {
        //Here we create an instance of the template button we serialized at the top and save it into a variable
        var newButton = Instantiate(RecipeButton);

        //Now using that variable we put the button into the navigation bar, which has settings in the editor
        //to automatically layout and space any buttons it contains
        newButton.transform.SetParent(contentGroup.transform);

        //Finally we get the text on the button from its children and set the text to the name of the codex entry
        newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

        return newButton;
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

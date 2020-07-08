using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/New Consumable Recipe")]
public class ConsumableRecipe : ScriptableObject
{
    public string DisplayName = "";

    public bool isUpgrade = false;

    public List<CraftingModule.ItemRecipeData> ItemInput;

    public List<CraftingModule.RecipeResourceData> ResourceInput;

    [Header("Creates")]
    public Resource Output;
    public float OutputAmount;

    [Header("Do not touch")]
    public float RequiredItemInputSpace= 0;
    public float ItemOuputSpace = 0;

    public ConsumableRecipe(List<CraftingModule.ItemRecipeData> NewIIn,List<CraftingModule.RecipeResourceData> NewRIn, Resource O)
    {
        ItemInput = NewIIn;
        ResourceInput  = NewRIn;
        Output = O;
    }

    //public ConsumableRecipe(List<CraftingModule.ItemRecipeData> NewIIn, List<CraftingModule.RecipeResourceData> NewRIn, Resource O)
    //{
    //    ItemInput = NewIIn;
    //    ResourceInput  = NewRIn;
    //    Output = O;
    //}

    //private void OnValidate()
   // {
    //    Awake();
    //}
    private void Awake()
    {
    }
}

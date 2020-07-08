using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/New Consumable Recipe")]
public class ConsumableRecipe : ScriptableObject
{
    public string DisplayName = "";
    public string ItemDesc = "";

    public bool isUpgrade = false;

    public List<CraftingModule.ItemRecipeData> ItemInput;

    public List<CraftingModule.RecipeResourceData> ResourceInput;

    [Header("Creates")]
    public Resource Output;
    public float OutputAmount;

    public ConsumableRecipe(List<CraftingModule.ItemRecipeData> NewIIn,List<CraftingModule.RecipeResourceData> NewRIn, Resource O)
    {
        ItemInput = NewIIn;
        ResourceInput  = NewRIn;
        Output = O;
    }
}

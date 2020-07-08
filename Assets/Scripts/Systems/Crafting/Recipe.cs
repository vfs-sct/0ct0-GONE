using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/New Item Recipe")]
public class Recipe : ScriptableObject
{
    public string DisplayName = "";
    public string ItemDesc = "";

    public List<CraftingModule.ItemRecipeData> ItemInput;

    public List<CraftingModule.RecipeResourceData> ResourceInput;

    public  CraftingModule.ItemRecipeData Output;

    public bool CreatesByProducts;

    public List<CraftingModule.ItemRecipeData> ItemByProducts;

    public List<CraftingModule.RecipeResourceData> ResourceByProducts;

    [Header("Do not touch")]
    public float RequiredItemInputSpace= 0;
    public float ItemOuputSpace = 0;

    public Recipe(List<CraftingModule.ItemRecipeData> NewIIn,List<CraftingModule.RecipeResourceData> NewRIn, CraftingModule.ItemRecipeData O, bool CB,List<CraftingModule.ItemRecipeData> IByOut,List<CraftingModule.RecipeResourceData> RByOut)
    {
        ItemInput = NewIIn;
        ResourceInput  = NewRIn;
        Output = O;
        CreatesByProducts = CB;
        ItemByProducts = IByOut;
        ResourceByProducts = RByOut;
    }

    public Recipe(List<CraftingModule.ItemRecipeData> NewIIn, List<CraftingModule.RecipeResourceData> NewRIn,CraftingModule.ItemRecipeData O)
    {
        ItemInput = NewIIn;
        ResourceInput  = NewRIn;
        Output = O;
        CreatesByProducts = false;
        ItemByProducts = null;
        ResourceByProducts = null;
        
    }

    //private void OnValidate()
   // {
    //    Awake();
    //}
    private void Awake()
    {
        foreach (var input in ItemInput) RequiredItemInputSpace += (input.amount * input.item.Size);
        foreach (var output in ItemByProducts) ItemOuputSpace += (output.amount * output.item.Size);
        ItemOuputSpace += (Output.amount * Output.item.Size);
    }
}

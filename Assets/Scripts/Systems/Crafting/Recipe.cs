using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/New Recipe")]
public class Recipe : ScriptableObject
{
    public string DisplayName = "";
    

    public List<CraftingModule.RecipeData> ItemInput;

    public List<CraftingModule.RecipeResourceData> ResourceInput;

    public  CraftingModule.RecipeData Output;

    public bool CreatesByProducts;

    public List<CraftingModule.RecipeData> ItemByProducts;

    public List<CraftingModule.RecipeResourceData> ResourceByProducts;

    [Header("Do not touch")]
    public float RequiredItemInputSpace= 0;
    public float ItemOuputSpace = 0;

    public Recipe(List<CraftingModule.RecipeData> NewIIn,List<CraftingModule.RecipeResourceData> NewRIn, CraftingModule.RecipeData O, bool CB,List<CraftingModule.RecipeData> IByOut,List<CraftingModule.RecipeResourceData> RByOut)
    {
        ItemInput = NewIIn;
        ResourceInput  = NewRIn;
        Output = O;
        CreatesByProducts = CB;
        ItemByProducts = IByOut;
        ResourceByProducts = RByOut;
    }

    public Recipe(List<CraftingModule.RecipeData> NewIIn, List<CraftingModule.RecipeResourceData> NewRIn,CraftingModule.RecipeData O)
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

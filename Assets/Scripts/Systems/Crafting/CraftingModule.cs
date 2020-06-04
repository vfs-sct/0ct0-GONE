using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/CraftingModule")]
public class CraftingModule : Module
{
    [System.Serializable]
    public struct CraftingData
    {
       public Resource resource;
       public float amount;
        public CraftingData(Resource r, float a)
        {
            resource = r;
            amount = a;
        }
    }

    [SerializeField] private List<CraftingRecipe> ActiveRecipes = new List<CraftingRecipe>();
    [SerializeField] private ResourceModule ResourceManager;

    public List<CraftingRecipe> GetRecipes(Resource ResourceToCheck)
    {
        List<CraftingRecipe> foundRecipes = new List<CraftingRecipe>();
        foreach (var Recipe in ActiveRecipes)
        {
            if (Recipe.Output.resource == ResourceToCheck)
            {
                foundRecipes.Add(Recipe);
            }
        }
        return foundRecipes;
    }

    public bool CanCraft(ResourceInventory Source, ResourceInventory Target, CraftingRecipe Recipe)
    {
        foreach (var ResourceData in Recipe.Input)
        {
            if (!Source.HasResource(ResourceData.resource)) return false;
            if (Source.GetResource(ResourceData.resource) < ResourceData.amount) return false;
            //TODO add functionality to prevent crafting a resource when storage is full
        }
        return true;
    }

    public void CraftItem(ResourceInventory Source, ResourceInventory Target, CraftingRecipe Recipe)
    {
        if (!CanCraft(Source, Target, Recipe))
        {
            Debug.Log("Can't craft");
            return; //exit out if can't craft item
        }

        foreach (var RecipeData in Recipe.Input)
        {
            Debug.Log("Resource removed");
            Source.RemoveResource(RecipeData.resource,RecipeData.amount);
        }
        if (!Target.HasResource(Recipe.Output.resource)) //create a resource instance if one is not present already
        {
            Debug.Log("Resource instance created");
            ResourceManager.CreateResourceInstance(Recipe.Output.resource,Target);
        }
        Debug.Log("Resource crafted");
        Target.AddResource(Recipe.Output.resource,Recipe.Output.amount);
    }

    public override void Initialize()
    {
    }
    public override void Reset()
    {
        
    }
}

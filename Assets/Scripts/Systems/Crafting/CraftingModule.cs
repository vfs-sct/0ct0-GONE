using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/CraftingModule")]
public class CraftingModule : Module
{
    //used for upgrades
    [SerializeField] protected UIModule UIRootModule = null;
    [System.Serializable]
    public struct ItemRecipeData
    {
        public Item item;
        public int amount;

        public ItemRecipeData(Item I, int A)
        {
            item = I;
            amount = A;
        }
    }

    [System.Serializable]
    public struct RecipeResourceData
    {
        public Resource resource;
        public float amount;

        public RecipeResourceData(Resource R, float A)
        {
            resource = R;
            amount = A;
        }
    }
    [SerializeField] private List<Recipe> Recipes = new List<Recipe>();
    public bool CanCraftConsumable(ResourceInventory ResourceInv, InventoryController SourceInv, ResourceInventory TargetInv, ConsumableRecipe CraftRecipe)
    {
        if (SourceInv == null || TargetInv == null || ResourceInv == null || TargetInv == null)
        {
            Debug.LogError(SourceInv);
            Debug.LogError(TargetInv);
            Debug.LogError(ResourceInv);
            return false;
        }

        foreach (var itemIn in CraftRecipe.ItemInput)
        {
            if (SourceInv.GetItemAmount(itemIn.item) < itemIn.amount) return false;
        }

        foreach (var resIn in CraftRecipe.ResourceInput)
        {
            if (ResourceInv.GetResource(resIn.resource) < resIn.amount) return false;
        }

        if (CraftRecipe.isUpgrade)
        {
            return true;
        }

        var resource = CraftRecipe.Output;
        if (TargetInv.GetResource(resource) == resource.GetMaximum())
        {
            Debug.Log($"{resource} is full");
        }
        return true;
    }

    public bool CanCraftSatellite(ResourceInventory ResourceInv,InventoryController SourceInv, SatelliteInventory TargetInv, SatelliteRecipe CraftRecipe)
    {
        if (SourceInv == null || TargetInv == null || ResourceInv == null || TargetInv == null) 
        {
            Debug.LogError(SourceInv);
            Debug.LogError(TargetInv);
            Debug.LogError(ResourceInv);
            return false;
        }
         foreach (var itemIn in CraftRecipe.ItemInput)
        {
            if (SourceInv.GetItemAmount(itemIn.item) <  itemIn.amount) return false;
        }

        foreach (var resIn in CraftRecipe.ResourceInput)
        {
            if (ResourceInv.GetResource(resIn.resource) <  resIn.amount) return false;
        }

        if (CraftRecipe.CreatesByProducts)
        {
            foreach (var resOut in CraftRecipe.ResourceByProducts)
            {
                if (ResourceInv.GetResource(resOut.resource)+  resOut.amount > resOut.resource.GetMaximum()) return false; //holy crap this is a kludge. TODO: Fix this
            }
        }

        if (TargetInv.StoredSatellites.Count != 0 && TargetInv.StoredSatellites[0] != null)
        {
            return false;
        }
        return true;
    }

    public bool CanCraft(ResourceInventory ResourceInv,InventoryController SourceInv, InventoryController TargetInv, Recipe CraftRecipe)
    {
        if (SourceInv == null || TargetInv == null || ResourceInv == null) 
        {
            Debug.LogError(SourceInv);
            Debug.LogError(TargetInv);
            Debug.LogError(ResourceInv);
            return false;
        }
        foreach (var itemIn in CraftRecipe.ItemInput)
        {
            if (SourceInv.GetItemAmount(itemIn.item) <  itemIn.amount) return false;
        }

        foreach (var resIn in CraftRecipe.ResourceInput)
        {
            if (ResourceInv.GetResource(resIn.resource) <  resIn.amount) return false;
        }

        if (CraftRecipe.CreatesByProducts)
        {
            foreach (var resOut in CraftRecipe.ResourceByProducts)
            {
                if (ResourceInv.GetResource(resOut.resource)+  resOut.amount > resOut.resource.GetMaximum()) return false; //holy crap this is a kludge. TODO: Fix this
            }
        }
        if ( TargetInv.GetItemBucketFillAmount() + CraftRecipe.ItemOuputSpace > TargetInv.GetItemBucketCap()) return false;

        return true;
    }

    public bool CraftItem(ResourceInventory ResourceInv,InventoryController SourceInv, InventoryController TargetInv, Recipe CraftRecipe,bool ByPassCraftCheck = false)
    {
        if (!ByPassCraftCheck) //optimization to skip checking if we can craft this recipe
        {
            if (!CanCraft(ResourceInv,SourceInv,TargetInv,CraftRecipe)) return false;
        }
        foreach (var itemIn in CraftRecipe.ItemInput) //TODO Optimize data structure to allow use of a single foreach for inputs
        {
            SourceInv.RemoveFromItemBucket(itemIn.item,itemIn.amount);
        }
        foreach (var resourceIn in CraftRecipe.ResourceInput)
        {
            ResourceInv.RemoveResource(resourceIn.resource,resourceIn.amount);
        }
        
        TargetInv.AddToItemBucket(CraftRecipe.Output.item,CraftRecipe.Output.amount);

        if (!CraftRecipe.CreatesByProducts) return true;

        foreach (var itemOut in CraftRecipe.ItemByProducts)//TODO Optimize data structure to allow use of a single foreach for outputs
        {
            TargetInv.AddToItemBucket(itemOut.item,itemOut.amount);
        }
        foreach (var resOut in CraftRecipe.ResourceByProducts)
        {
            ResourceInv.AddResource(resOut.resource,resOut.amount);
        }
        return true;
    }

     public bool CraftSatellite(ResourceInventory ResourceInv,InventoryController SourceInv, SatelliteInventory TargetInv, SatelliteRecipe CraftRecipe,bool ByPassCraftCheck = false)
    {
        if (!ByPassCraftCheck) //optimization to skip checking if we can craft this recipe
        {
            if (!CanCraftSatellite(ResourceInv,SourceInv,TargetInv,CraftRecipe)) return false;
        }
        foreach (var itemIn in CraftRecipe.ItemInput) //TODO Optimize data structure to allow use of a single foreach for inputs
        {
            SourceInv.RemoveFromItemBucket(itemIn.item,itemIn.amount);
        }
        foreach (var resourceIn in CraftRecipe.ResourceInput)
        {
            ResourceInv.RemoveResource(resourceIn.resource,resourceIn.amount);
        }

        TargetInv.SetSatellite(CraftRecipe.Output);
        //TODO (If required) Implement resource and item byproducts
        return true;
    }

    public bool CraftConsumable(ResourceInventory ResourceInv, InventoryController SourceInv, ResourceInventory TargetInv, ConsumableRecipe CraftRecipe, bool ByPassCraftCheck = false)
    {
        if (!ByPassCraftCheck) //optimization to skip checking if we can craft this recipe
        {
            if (!CanCraftConsumable(ResourceInv, SourceInv, TargetInv, CraftRecipe)) return false;
        }

        foreach (var itemIn in CraftRecipe.ItemInput) //TODO Optimize data structure to allow use of a single foreach for inputs
        {
            SourceInv.RemoveFromItemBucket(itemIn.item, itemIn.amount);
        }
        foreach (var resourceIn in CraftRecipe.ResourceInput)
        {
            ResourceInv.RemoveResource(resourceIn.resource, resourceIn.amount);
        }

        if(CraftRecipe.isUpgrade)
        {
            Debug.Log("Before Amount: " + TargetInv.GetResource(CraftRecipe.Output));
            //if(CraftRecipe.Output.name == "Health")
            //{
            //    UIRootModule.UIRoot.GetScreen<GameHUD>().healthUpgrade.Upgrade(CraftRecipe.OutputAmount);
            //}
            if (CraftRecipe.Output.name == "Fuel")
            {
                UIRootModule.UIRoot.GetScreen<GameHUD>().fuelUpgrade.Upgrade(CraftRecipe.OutputAmount);
            }
            //Add Resource already contains a clamp so resource cannot go over max
            TargetInv.AddResource(CraftRecipe.Output, CraftRecipe.Output.GetMaximum());
            Debug.Log("After Amount: " + TargetInv.GetResource(CraftRecipe.Output));
        }
        else
        {
            //Add Resource already contains a clamp so resource cannot go over max
            TargetInv.AddResource(CraftRecipe.Output, CraftRecipe.OutputAmount);
        }
        return true;
    }


    public override void Initialize()
    {

    }
    public override void Reset()
    {
        
    }
}

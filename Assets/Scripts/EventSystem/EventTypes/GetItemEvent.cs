using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Item Collection Event")]

public class GetItemEvent : Event
{

    [SerializeField] private List<CraftingModule.ItemRecipeData> Items = new List<CraftingModule.ItemRecipeData>();

    [SerializeField] public string actionVerb = "Craft";


    [Header("References")]
    [SerializeField] private CraftingModule _CraftingModule;
    [SerializeField] private Playing Playstate;
    private InventoryController Inventory;


    public override bool Condition(GameObject target)
    {
        foreach (var itemData in Items)
        {
            if (!Inventory.CheckIfItemBucket()) return false;
            if (!Inventory.GetItemBucket()[0].Bucket.ContainsKey(itemData.item)) return false;
        }

        foreach (var itemData in Items)
        {
            if (Inventory.GetItemBucket()[0].Bucket[itemData.item] < itemData.amount) return false;
        }
        //todo update widget
        return true;
    }

    protected override void Effect(GameObject target)
    {
        
    }
    override public void InitializeEvent()
    {
        Inventory = Playstate.ActivePlayer.GetComponent<InventoryController>(); //TODO If we add respawning this will break!
    }
}

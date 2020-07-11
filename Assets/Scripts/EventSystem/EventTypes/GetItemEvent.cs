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
    [SerializeField] protected UIModule UIRootModule = null;
    private InventoryController Inventory;


    public override bool Condition(GameObject target)
    {
        foreach (var itemData in Items)
        {
            if (!Inventory.CheckIfItemBucket()) return false;
            if (!Inventory.GetItemBucket()[0].Bucket.ContainsKey(itemData.item)) return false;
        }

        //keep track of how many of the objectives arent met
        int notMet = 0;
        //index is used to update objective text
        int index = 0;
        foreach (var itemData in Items)
        {
            var currentAmount = Inventory.GetItemBucket()[0].Bucket[itemData.item];
            if (currentAmount < itemData.amount)
            {
                notMet++;
            }
            string objectiveUpdate = $"{currentAmount}/{itemData.amount} - {actionVerb} {Items[index].item.Name}";
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(index, objectiveUpdate);
            index++;
        }
        if(notMet > 0)
        {
            return false;
        }

        //todo update widget
        ObjectivePopup(isFirstEvent);
        Debug.Log("EVENT CONDITION MET");
        CodexProgression();
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
        Inventory = null;
        return true;
    }

    private void ObjectivePopup(bool isFirst)
    {
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePopUp.SetObjectiveText(isFirst);
    }

    private void CodexProgression()
    {
        UIRootModule.UIRoot.GetScreen<Codex>().UnlockNextEntry();
    }

    protected override void Effect(GameObject target)
    {
        
    }
    override public void InitializeEvent()
    {
        Debug.Log("EVENT" + this.name);

        Inventory = Playstate.ActivePlayer.GetComponent<InventoryController>(); //TODO If we add respawning this will break!

        //objective text
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();

        int index = 0;
        foreach (var itemData in Items)
        {
            string objectiveUpdate;
            if (Inventory.GetItemBucket()[0].Bucket != null)
            {
                var currentAmount = Inventory.GetItemBucket()[0].Bucket[itemData.item];
                objectiveUpdate = $"{currentAmount}/{itemData.amount} - {actionVerb} {Items[index].item.Name}";
            }
            else
            {
                objectiveUpdate = $"0/{itemData.amount} - {actionVerb} {Items[index].item.Name}";
            }
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
            index++;
        }
    }
}

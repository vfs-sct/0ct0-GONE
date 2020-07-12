using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Craft&Place Satellite Event")]

public class CraftSatelliteEvent : Event
{

    [SerializeField] private List<CraftingModule.ItemRecipeData> Items = new List<CraftingModule.ItemRecipeData>();

    [SerializeField] public string[] actionVerb = new string[]
    { 
        "Craft",
        "Place"
    };

    [Header("References")]
    [SerializeField] private CraftingModule _CraftingModule;
    [SerializeField] private Playing Playstate;
    [SerializeField] protected UIModule UIRootModule = null;
    [SerializeField] private GameObject satelliteType = null;

    private SatelliteInventory SatInv;
    bool craftComplete = false;
    bool placeComplete = true;
    public bool isActive = false;


    public override bool Condition(GameObject target)
    {
        //this objective will be auto-completed if the player already has the correct sat in their inventory on objective start
        if(craftComplete == false)
        {
            if (!SatInv.CheckIfSat()) return false;

            if (SatInv.GetSatellite() == satelliteType)
            {
                craftComplete = true;
                string objectiveUpdate = $"1/1 {actionVerb[0]} {satelliteType.name}";
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(0, objectiveUpdate);
            }
        }

        if (placeComplete && craftComplete)
        {
            //todo update widget
            ObjectivePopup(isFirstEvent);
            Debug.Log("EVENT CONDITION MET");
            CodexProgression();
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            SatInv = null;
            return true;
        }

        return false;
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

    public void SatPlaced(GameObject satPlaced)
    {
        if(satPlaced == satelliteType)
        {
            placeComplete = true;
            string objectiveUpdate = $"1/1 {actionVerb[1]} {satelliteType.name}";
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(1, objectiveUpdate);
        }
    }

    override public void InitializeEvent()
    {
        isActive = true;
        Debug.Log("EVENT" + this.name);

        SatInv = Playstate.ActivePlayer.GetComponent<SatelliteInventory>(); //TODO If we add respawning this will break!

        //objective text
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();

        string objectiveUpdate = $"0/1 {actionVerb[0]} {satelliteType.name}";
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);

        objectiveUpdate = $"0/1 {actionVerb[1]} {satelliteType.name}";
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
    }
}

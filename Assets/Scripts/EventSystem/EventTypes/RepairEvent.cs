using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Repair Station Event")]

public class RepairEvent : Event
{
    //serialized number grabs the satellite at that index from the RepairableStation prefab root
    //make sure the entered number correlates with the correct satellite index in the RepairableStation array
    [SerializeField] private int repairStation;
    [SerializeField] public string actionVerb = "Repair";

    [Header("Upgrades")]
    [SerializeField] private float commRangeIncrease = 0f;
    [SerializeField] private float fuelBarIncrease = 0f;
    [SerializeField] private float thrusterIncrease = 0f;

    [Header("References")]
    [SerializeField] private EventModule EventModule;
    [SerializeField] private Playing Playstate;
    [SerializeField] protected UIModule UIRootModule = null;

    private RepairableInfo targetSat = null;


    public override bool Condition(GameObject target)
    {
        //Debug.LogError(targetSat.IsRepaired());

        if(targetSat.IsRepaired())
        {
            EventModule.CommZone.AddRange(commRangeIncrease);
            //todo update widget
            ObjectivePopup(isFirstEvent);
            Debug.Log("EVENT CONDITION MET");
            CodexProgression();
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            //reset scriptable object values
            targetSat = null;
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
    override public void InitializeEvent()
    {
        Debug.Log("EVENT" + this.name);

        //get our specific satellite's repair info off the repairableroot
        targetSat = EventModule.RepairableRoot.GetRepairable(repairStation).GetComponent<RepairableInfo>();

        //set objective text
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
        string objectiveUpdate = $"0/1 - {actionVerb} the {targetSat.DisplayName}";
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
    }
}

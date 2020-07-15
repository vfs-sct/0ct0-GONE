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
    private GameHUD gameHUD = null;

    public override bool Condition(GameObject target)
    {
        //Debug.LogError(targetSat.IsRepaired());

        if(targetSat.IsRepaired())
        {
            if (commRangeIncrease != 0)
            {
                EventModule.CommZone.AddRange(commRangeIncrease);
            }
            if (fuelBarIncrease != 0)
            {
                gameHUD.fuelUpgrade.Upgrade(fuelBarIncrease);
            }
            if(thrusterIncrease != 0)
            {
                UIRootModule.UIRoot.player.gameObject.GetComponent<MovementController>().AddThrusterImpulse(thrusterIncrease);
            }
            //todo update widget
            ObjectivePopup(isFirstEvent);
            Debug.Log("EVENT CONDITION MET");
            CodexProgression();
            gameHUD.objectivePanel.ClearObjectives();
            //reset scriptable object values
            targetSat = null;
            gameHUD = null;
            return true;
        }
        return false;
    }

    private void ObjectivePopup(bool isFirst)
    {
        gameHUD.objectivePopUp.SetObjectiveText(isFirst);
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

        gameHUD = UIRootModule.UIRoot.GetScreen<GameHUD>();

        //set objective text
        gameHUD.objectivePanel.ClearObjectives();
        string objectiveUpdate = $"0/1 - {actionVerb} the {targetSat.DisplayName}";
        gameHUD.objectivePanel.AddObjective(objectiveUpdate);
    }
}

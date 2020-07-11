using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Generic Event")]
public class GenericEvent : Event
{
    public bool EventTrigger = false;
    [SerializeField] public string actionVerb = "Repair";
    [SerializeField] protected UIModule UIRootModule = null;
    private bool isInitialized = false;
    private bool isUpdating = false;

    public override bool Condition(GameObject target)
    {
        if(EventTrigger == true)
        {
            ObjectivePopup(isFirstEvent);
            //NextTutorialPrompt();
            Debug.Log("EVENT CONDITION MET");
            EventTrigger = true;
            CodexProgression();
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            //reset the scriptableobject values
            isInitialized = false;
            isUpdating = false;
        }
        return EventTrigger;
    }

    public override void InitializeEvent()
    {
        //objective text
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
        string objectiveUpdate = $"0/1 - {actionVerb} Solar Panels";
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
        
        isInitialized = true;
    }

    private void ObjectivePopup(bool isFirst)
    {
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePopUp.SetObjectiveText(isFirst);
    }

    private void NextTutorialPrompt()
    {
        UIRootModule.UIRoot.GetScreen<Tutorial>().NextPrompt();
    }

    private void CodexProgression()
    {
        UIRootModule.UIRoot.GetScreen<Codex>().UnlockNextEntry();
    }

    protected override void Effect(GameObject target)
    {
        //no effect, this uses unity events for any effects
    }
}

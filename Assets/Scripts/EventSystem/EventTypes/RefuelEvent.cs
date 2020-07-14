using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Refuel Event")]
public class RefuelEvent : Event
{
    private bool EventTrigger = false;
    [SerializeField] Resource CollectResource;
    [SerializeField] public string actionVerb = "Refuel";
    [SerializeField] protected UIModule UIRootModule = null;
    private ResourceInventory playerInventory;
    private bool isInitialized = false;
    private bool isUpdating = false;

    public override bool Condition(GameObject target)
    {
        if(isInitialized == true)
        {
            Debug.Log("EVENT" + this.name);
            //playerinventory
            playerInventory = UIRootModule.UIRoot.player.GetComponent<ResourceInventory>();
            
            //tutorial
            //UIRootModule.UIRoot.GetScreen<Tutorial>().FirstPrompt();

            //objective text
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            var shortenCurrentAmount = (float)Math.Floor(CollectResource.GetInstanceValue(playerInventory));
            string objectiveUpdate = $"{shortenCurrentAmount}/{CollectResource.GetMaximum()} - {actionVerb} {CollectResource.DisplayName}";
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
            
            //state of event
            isUpdating = true;
            isInitialized = false;
        }
        if (isUpdating == true)
        {
            var currentAmount = CollectResource.GetInstanceValue(playerInventory);

            if (UIRootModule.UIRoot != null)
            {
                var shortenCurrentAmount = (float)Math.Floor(currentAmount);
                string objectiveUpdate = $"{shortenCurrentAmount}/{CollectResource.GetMaximum()} - {actionVerb} {CollectResource.DisplayName}";
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(0, objectiveUpdate);
            }

            //quest complete?
            if (currentAmount >= CollectResource.GetMaximum())
            {
                ObjectivePopup(isFirstEvent);
                NextTutorialPrompt();
                Debug.Log("EVENT CONDITION MET");
                EventTrigger = true;
                CodexProgression();
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
                //reset the scriptableobject values
                playerInventory = null;
                isInitialized = false;
                isUpdating = false;
            }
        }

        return EventTrigger;
    }

    public override void InitializeEvent()
    {
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
    }
}

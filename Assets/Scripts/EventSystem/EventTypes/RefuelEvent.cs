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

    private float previousAmount;

    public override bool Condition(GameObject target)
    {
        if(isInitialized == true)
        {
            Debug.Log("EVENT" + this.name);
            //playerinventory
            playerInventory = UIRootModule.UIRoot.player.GetComponent<ResourceInventory>();

            previousAmount = CollectResource.GetInstanceValue(playerInventory);

            //tutorial
            //UIRootModule.UIRoot.GetScreen<Tutorial>().FirstPrompt();

            //objective text
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            //var shortenCurrentAmount = (float)Math.Floor(CollectResource.GetInstanceValue(playerInventory));
            //string objectiveUpdate = $"{shortenCurrentAmount}/{CollectResource.GetMaximum()} - {actionVerb} {CollectResource.DisplayName}";
            string objectiveUpdate = $"{actionVerb} at the station";
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
            
            //state of event
            isUpdating = true;
            isInitialized = false;
        }
        if (isUpdating == true)
        {
            var currentAmount = CollectResource.GetInstanceValue(playerInventory);

            //if (UIRootModule.UIRoot != null)
            //{
            //    var shortenCurrentAmount = (float)Math.Floor(currentAmount);
            //    string objectiveUpdate = $"{shortenCurrentAmount}/{CollectResource.GetMaximum()} - {actionVerb} {CollectResource.DisplayName}";
            //    UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(0, objectiveUpdate);
            //}

            //quest complete?
            if (currentAmount > previousAmount)
            {
                ObjectivePopup(isFirstEvent);
                NextTutorialPrompt();
                Debug.Log("EVENT CONDITION MET");
                EventTrigger = true;
                CodexProgression();
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
                //reset the scriptableobject values
                playerInventory = null;
                previousAmount = 9999;
                isInitialized = false;
                isUpdating = false;
            }
            previousAmount = currentAmount;
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
        UIRootModule.UIRoot.GetScreen<Tutorial>().NextPrompt(5f);
    }

    private void CodexProgression()
    {
        UIRootModule.UIRoot.GetScreen<Codex>().UnlockNextEntry();
    }

    protected override void Effect(GameObject target)
    {
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Resource Collection Event")]
public class GetResourceEvent : Event
{
    private bool EventTrigger = false;
    [SerializeField] public Resource[] CollectResource;
    [SerializeField] public float[] CollectAmount;
    [SerializeField] public string actionVerb = "Salvage";
    [SerializeField] protected UIModule UIRootModule = null;

    private List<float> previousAmount = new List<float>();
    private List<float> totalAdded = new List<float>();
    private bool isInitialized = false;
    private bool isUpdating = false;

    public override bool Condition(GameObject target)
    {
        if(isInitialized == true)
        {
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            Debug.Log("EVENT" + this.name);

            var playerInventory = target.GetComponent<InventoryController>();

            for (int i = 0; i < CollectResource.Length; i++)
            {
                if (!playerInventory.CheckIfItemBucket())
                {
                    previousAmount.Add(0f);
                }
                else
                {
                    //start amounts
                    previousAmount.Add(playerInventory.GetResourceAmount(CollectResource[i]));
                }
                totalAdded.Add(0f);

                //objective text
                string objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
            }

            //update state of event
            isUpdating = true;
            isInitialized = false;
        }
        if (isUpdating == true)
        {
            int incompletedTally = CollectResource.Length;
            for(int i = 0; i < CollectResource.Length; i++)
            {
                float currentAmount;
                if (target.GetComponent<InventoryController>() != null)
                {
                    currentAmount = target.GetComponent<InventoryController>().GetResourceAmount(CollectResource[i]);
                }
                else
                {
                    return false;
                }
                //TODO: this should really only be done when theres a change to the text
                if (UIRootModule.UIRoot != null)
                {
                    string objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
                    UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(i, objectiveUpdate);
                }

                //exit early if no change
                if (currentAmount == previousAmount[i])
                {
                    continue;
                }
                else
                {
                    //find out if the amount we have now is higher than the previous amount to see if any resource was added
                    var delta = (float)Math.Floor(currentAmount - previousAmount[i]);
                    if (delta > 0)
                    {
                        //if resource was added, add to the total added amount
                        totalAdded[i] = totalAdded[i] + delta;
                    }

                    //update previous amount for next tick
                    previousAmount[i] = currentAmount;
                }
            }

            //quest complete?
            if (incompletedTally == 0)
            {
                ObjectivePopup(isFirstEvent);
                Debug.Log("EVENT CONDITION MET");
                EventTrigger = true;
                CodexProgression();
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
                //reset the scriptableobject values
                totalAdded.Clear();
                previousAmount.Clear();
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

    private void CodexProgression()
    {
        UIRootModule.UIRoot.GetScreen<Codex>().UnlockNextEntry();
    }

    protected override void Effect(GameObject target)
    {
    }
}

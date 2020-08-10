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
        var playerInventory = target.GetComponentInChildren<InventoryController>();

        if (isInitialized == true)
        {
            previousAmount.Clear();
            totalAdded.Clear();
            UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
            //Debug.Log("EVENT" + this.name);

            for (int i = 0; i < CollectResource.Length; i++)
            {
                //start amounts
                if (!playerInventory.CheckIfItemBucket())
                {
                    previousAmount.Add(0f);
                }
                else
                {
                    previousAmount.Add(playerInventory.GetResourceAmount(CollectResource[i]));
                }
                totalAdded.Add(0f);
                var resColor = ColorUtility.ToHtmlStringRGBA(CollectResource[i].ResourceColor);
                string objectiveUpdate = string.Format("{0}/{1} - {2} <color=#" + resColor + ">{3}</color>", totalAdded[i], CollectAmount[i], actionVerb, CollectResource[i].DisplayName);
                //objective text
                //string objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.AddObjective(objectiveUpdate);
            }

            //update state of event
            isUpdating = true;
            isInitialized = false;
        }
        if (isUpdating == true)
        {
            if(!playerInventory.CheckIfResourceBucket())
            {
                return EventTrigger;
            }
            //if they player hasnt collected enough of the resource, subtract from tally
            int incompletedTally = 0;
            for(int i = 0; i < CollectResource.Length; i++)
            {
                //go to next loop early if resource collection is already complete
                if (totalAdded[i] >= CollectAmount[i])
                {
                    continue;
                }

                float currentAmount;
                currentAmount = playerInventory.GetResourceAmount(CollectResource[i]);

                //exit early if no change
                //we already exited if the collection was complete so we know if there's no change here that they havent collected enough
                if (currentAmount == previousAmount[i])
                {
                    incompletedTally++;
                    if (UIRootModule.UIRoot != null)
                    {
                        var resColor = ColorUtility.ToHtmlStringRGBA(CollectResource[i].ResourceColor);
                        string objectiveUpdate = string.Format("{0}/{1} - {2} <color=#" + resColor + ">{3}</color>", totalAdded[i], CollectAmount[i], actionVerb, CollectResource[i].DisplayName);
                        //string objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
                        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(i, objectiveUpdate);
                    }
                    previousAmount[i] = currentAmount;
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
                        if(totalAdded[i] <= CollectAmount[i])
                        {
                            incompletedTally++;
                            if (UIRootModule.UIRoot != null)
                            {
                                var resColor = ColorUtility.ToHtmlStringRGBA(CollectResource[i].ResourceColor);
                                string objectiveUpdate = string.Format("{0}/{1} - {2} <color=#" + resColor + ">{3}</color>", totalAdded[i], CollectAmount[i], actionVerb, CollectResource[i].DisplayName);
                                //string objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
                                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(i, objectiveUpdate);
                            }
                            previousAmount[i] = currentAmount;
                            continue;
                        }
                    }

                    //update previous amount for next tick
                    previousAmount[i] = currentAmount;
                }

                //TODO: this should really only be done when theres a change to the text
                if (UIRootModule.UIRoot != null)
                {
                    var resColor = ColorUtility.ToHtmlStringRGBA(CollectResource[i].ResourceColor);
                    string objectiveUpdate = string.Format("{0}/{1} - {2} <color=#" + resColor + ">{3}</color>", totalAdded[i], CollectAmount[i], actionVerb, CollectResource[i].DisplayName);
                    //string objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
                    UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(i, objectiveUpdate);
                }
            }

            //quest complete?
            if (incompletedTally == 0)
            {
                ObjectivePopup(isFirstEvent);
                //Debug.Log("EVENT CONDITION MET");
                EventTrigger = true;
                CodexProgression();
                UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.ClearObjectives();
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

    private void UpdateObjectivePanel(int i)
    {   
        string objectiveUpdate;
        var resColor = ColorUtility.ToHtmlStringRGBA(CollectResource[i].ResourceColor);
        if (UIRootModule.UIRoot != null)
        {
            objectiveUpdate = string.Format("{0}/{1} - {2} <color=#" + resColor + ">{3}</color>", totalAdded[i], CollectAmount[i], actionVerb, CollectResource[i].DisplayName);
            //objectiveUpdate = $"{totalAdded[i]}/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
        }
        else
        {
            objectiveUpdate = string.Format("0/{0} - {1} <color=#" + resColor + ">{2}</color>", CollectAmount[i], actionVerb, CollectResource[i].DisplayName);
            //objectiveUpdate = $"0/{CollectAmount[i]} - {actionVerb} {CollectResource[i].DisplayName}";
        }
        UIRootModule.UIRoot.GetScreen<GameHUD>().objectivePanel.UpdateObjective(i, objectiveUpdate);
    }

    protected override void Effect(GameObject target)
    {
    }
}

using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Resource Collection Event")]
public class GetResourceEvent : Event
{
    private bool EventTrigger = false;
    [SerializeField] Resource CollectResource;
    [SerializeField] float ResourceAmount;
    [SerializeField] public string actionVerb = "Salvage";
    [SerializeField] protected UIModule UIRootModule = null;

    private float previousAmount = -1000f;
    private float totalAdded;
    private bool isInitialized = false;
    private bool isUpdating = false;

    public override bool Condition(GameObject target)
    {
        if(isInitialized == true)
        {
            previousAmount = target.GetComponent<InventoryController>().GetResourceAmount(CollectResource);
            isUpdating = true;
            isInitialized = false;
        }
        if (isUpdating == true)
        {
            float currentAmount;
            if (target.GetComponent<InventoryController>() != null)
            {
                currentAmount = target.GetComponent<InventoryController>().GetResourceAmount(CollectResource);
            }
            else
            {
                return false;
            }
            //TODO: this should really only be done when theres a change to the text
            if (UIRootModule.UIRoot != null)
            {
                string objectiveUpdate = $"{totalAdded}/{ResourceAmount} - {actionVerb} {CollectResource.DisplayName}";
                UIRootModule.UIRoot.GetScreen<GameHUD>().SetObjectiveText(objectiveUpdate);
            }

            //exit early if no change
            if (currentAmount == previousAmount)
            {
                return EventTrigger;
            }
            else
            {
                //find out if the amount we have now is higher than the previous amount to see if any resource was added
                var delta = (float)Math.Floor(currentAmount - previousAmount);
                if (delta > 0)
                {
                    //if resource was added, add to the total added amount
                    totalAdded = totalAdded + delta;
                }

                //update previous amount for next tick
                previousAmount = currentAmount;
            }

            //quest complete?
            if (totalAdded >= ResourceAmount)
            {
                Debug.Log("EVENT CONDITION MET");
                EventTrigger = true;
                CodexProgression();
                //reset the scriptableobject values
                totalAdded = 0;
                previousAmount = -1000f;
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

    private void CodexProgression()
    {
        UIRootModule.UIRoot.GetScreen<Codex>().UnlockNextEntry();
    }

    protected override void Effect(GameObject target)
    {
    }
}

using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Resource Crafting Event")]
public class ResourceCraftingEvent : Event
{
    public bool EventTrigger = false;
    [SerializeField] Resource[] CollectResource;
    [SerializeField] int[] ResourceAmount;

    public override bool Condition(GameObject target)
    {
        return EventTrigger;
    }

    protected override void Effect(GameObject target)
    {
        //no effect, this uses unity events for any effects
    }
}

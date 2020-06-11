using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Resource Collection Event")]
public class ResourceCollectionEvent : Event
{
    public bool EventTrigger = false;
    [SerializeField] Resource[] CollectResource;
    [SerializeField] int[] ResourceAmount;
    [SerializeField] private ResourceModule ResourceController = null;

    public override bool Condition(GameObject target)
    {
        return EventTrigger;
    }

    protected override void Effect(GameObject target)
    {
        //no effect, this uses unity events for any effects
    }
}

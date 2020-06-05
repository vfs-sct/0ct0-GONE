using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Generic Event")]
public class GenericEvent : Event
{
    public bool EventTrigger = false;

    public override bool Condition(GameObject target)
    {
        return EventTrigger;
    }

    protected override void Effect(GameObject target)
    {
        //no effect, this uses unity events for any effects
    }
}

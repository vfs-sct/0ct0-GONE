using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Systems/Events/Effects/New Generic Effect")]
public class GenericEventEffect : EventEffect
{
    public UnityEvent TriggerEvent;
    public override void Trigger(Event parent,GameObject triggerObject)
    {
        TriggerEvent.Invoke();
    }
}

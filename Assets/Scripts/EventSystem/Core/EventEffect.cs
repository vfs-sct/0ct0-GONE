using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Systems/Events/Effects/New Effect")]
public abstract class EventEffect : ScriptableObject
{
    public abstract void Trigger(Event parent,GameObject triggerObject);
}

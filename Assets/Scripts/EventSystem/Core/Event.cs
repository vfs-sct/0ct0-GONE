using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using ScriptableGameFramework;

//[CreateAssetMenu(menuName = "Systems/Events/New Event")]
public abstract class Event : ScriptableObject
{
    public string EventName;
    public bool isFirstEvent = false;
    public bool progressesStory = true;

    public abstract bool Condition(GameObject target);

    public List<EventEffect> EventEffects = new List<EventEffect>();

    public void TriggerEffect(GameObject target)
    {
        //CodexProgression();
        TriggerEventEffects(target);
        Effect(target);
    }

    private void TriggerEventEffects(GameObject target)
    {
        foreach (var effect in EventEffects)
        {
            effect.Trigger(this,target);
        }
    }

    virtual public void InitializeEvent(){}

    protected abstract void Effect(GameObject target);
}

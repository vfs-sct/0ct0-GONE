using UnityEngine;
using UnityEngine.Events;


//[CreateAssetMenu(menuName = "Systems/Events/New Event")]
public abstract class Event : ScriptableObject
{
    public string EventName;
    public string EventText;
    public string CompletionText;
    protected GameFrameworkManager GameManager = null;

    public abstract bool Condition(GameObject target);

    public UnityEvent EventEffectDelegates;

    public void TriggerEffect(GameObject target)
    {
        //CodexProgression();
        EventEffectDelegates.Invoke();
        Effect(target);
    }

    protected abstract void Effect(GameObject target);
}

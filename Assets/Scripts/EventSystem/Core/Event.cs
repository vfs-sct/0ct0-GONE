using UnityEngine;
using UnityEngine.Events;


//[CreateAssetMenu(menuName = "Systems/Events/New Event")]
public abstract class Event : ScriptableObject
{
    public string EventName;
    public bool isFirstEvent = false;
    public bool progressesStory = true;
    protected GameFrameworkManager GameManager = null;

    public abstract bool Condition(GameObject target);

    public UnityEvent EventEffectDelegates;

    public void TriggerEffect(GameObject target)
    {
        //CodexProgression();
        EventEffectDelegates.Invoke();
        Effect(target);
    }

    virtual public void InitializeEvent(){}

    protected abstract void Effect(GameObject target);
}

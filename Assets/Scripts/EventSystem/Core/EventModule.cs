using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Event Module")]
public class EventModule : Module
{

    //List of events that are called in sequence
    [SerializeField] private List<Event> EventSequence = new List<Event>();
    
    private Queue<Event> EventQueue = new Queue<Event>();
    private Event _CurrentEvent;
    public Event CurrentEvent{get=>_CurrentEvent;}


    public override void Start()
    {
        _CurrentEvent = EventSequence[0];
        foreach (var item in EventSequence)
        {
            EventQueue.Enqueue(item);
        }
    }

    //gets the next event in the stack
    private void NextEvent(GameObject target)
    {
        _CurrentEvent.TriggerEffect(target);
        if (EventQueue.Count != 0)
        {
           _CurrentEvent = EventQueue.Dequeue(); 
        } 
        else 
        {
            _CurrentEvent = null;
        }
    }

    //check the conditions of the current event
    public bool CheckSequenceConditions(GameObject target)
    {
        return _CurrentEvent.Condition(target);
    }    


    //call this to update the events
    public void UpdateEvents(GameObject target)
    {
        if (CheckSequenceConditions(target))
        {
            NextEvent(target);
        }
    }


    public override void Reset()
    {
        EventQueue.Clear();
    }
}

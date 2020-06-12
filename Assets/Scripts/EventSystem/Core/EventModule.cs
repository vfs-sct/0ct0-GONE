using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Event Module")]
public class EventModule : Module
{

    //NEVER ALTER OR TOUCH THINGS IN THESE EVENTS:
    [SerializeField] private List<Event> EventSequence = new List<Event>();
    
    //This is a copy of the above events. Events that are used/altered in game are a copy of the above events, meaning
    //that values will automatically reset when you finish/exit/crash the game since we're never altering
    //root events directly
    private Queue<Event> EventQueue = new Queue<Event>();
    private Event _CurrentEvent;
    public Event CurrentEvent{get=>_CurrentEvent;}


    public override void Start()
    {
        bool is_first = true;
        foreach (var item in EventSequence)
        {
            var item_copy = GameObject.Instantiate(item);
            if (is_first)
            {
                is_first = false;
                _CurrentEvent = item_copy;
            }
            else
            {
                EventQueue.Enqueue(item_copy);
            }
        }
    }

    //gets the next event in the stack
    private void NextEvent(GameObject target)
    {
        Debug.Log("EVENT COMPLETE");
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
        if (CheckSequenceConditions(target) == true)
        {  
            NextEvent(target);
        }
    }


    public override void Reset()
    {
        EventQueue.Clear();
    }
}

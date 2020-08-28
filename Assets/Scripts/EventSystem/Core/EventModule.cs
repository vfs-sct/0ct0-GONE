using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Event Module")]
public class EventModule : Module
{
    [SerializeField] GameFrameworkManager gameManager = null;

    [SerializeField] private SaveFile saveFile = null;
    
    [Header("DO NOT TOUCH")]
    //NEVER ALTER OR TOUCH THINGS IN THESE EVENTS:
    [SerializeField] private List<Event> EventSequence = new List<Event>();


    private CommunicationZone _CommZone = null;
    public CommunicationZone CommZone { get => _CommZone; }
    //used to access the ship pieces you repair
    private RepairablesRoot _RepairableRoot = null;
    public RepairablesRoot RepairableRoot { get => _RepairableRoot; }

    //This is a copy of the above events. Events that are used/altered in game are a copy of the above events, meaning
    //that values will automatically reset when you finish/exit/crash the game since we're never altering
    //root events directly
    private Queue<Event> EventQueue = new Queue<Event>();
    private Event _CurrentEvent;
    private bool _EventListComplete = false;
    public bool EventListComplete{get =>_EventListComplete;}
    public Event CurrentEvent{get=>_CurrentEvent;}


    public void SetCommZone(CommunicationZone commZone)
    {
        _CommZone = commZone;
    }

    public void SetRepairableRoot(RepairablesRoot repairRoot)
    {
        _RepairableRoot = repairRoot;
    }

    public override void Start()
    {
        Reset();

        //if no event progression is saved, proceed as if starting a new game
        if (saveFile.objective == 0)
        {
            //making copy of event list so we dont alter the original
            bool is_first = true;
            foreach (var item in EventSequence)
            {
                var item_copy = GameObject.Instantiate(item);
                if (is_first)
                {
                    is_first = false;
                    _CurrentEvent = item_copy;
                    _CurrentEvent.InitializeEvent();
                }
                else
                {
                    EventQueue.Enqueue(item_copy);
                }
            }
        }
        else //if event progression is saved, initiate the event the player was on when they last stopped playing
        {
            int isStartPoint = 0;
            foreach (var item in EventSequence)
            {
                var item_copy = GameObject.Instantiate(item);
                if (isStartPoint == saveFile.objective)
                {
                    _CurrentEvent = item_copy;
                    _CurrentEvent.InitializeEvent();
                }
                else if(isStartPoint > saveFile.objective)
                {
                    EventQueue.Enqueue(item_copy);
                }

                isStartPoint++;
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
            saveFile.objective++;
           _CurrentEvent = EventQueue.Dequeue();
            //initialize new event
           _CurrentEvent.InitializeEvent();
        } 
        else 
        {
            _CurrentEvent = null;
            _EventListComplete = true;
        }
    }

    //check the conditions of the current event
    public bool CheckSequenceConditions(GameObject target)
    {
        if(gameManager.ActiveGameState.GetType() == typeof(Playing))
        {
            return _CurrentEvent.Condition(target);
        }
        else
        {
            return false;
        }
    }    


    //call this to update the events
    public void UpdateEvents(GameObject target)
    {
        if (!_EventListComplete && CheckSequenceConditions(target) == true)
        {  
            NextEvent(target);
        }
    }


    public override void Reset()
    {
        EventQueue.Clear();
        _EventListComplete = false;
    }
}

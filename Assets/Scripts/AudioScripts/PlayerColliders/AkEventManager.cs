using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkEventManager : AkManager
{
    [SerializeField] private AK.Wwise.Event[] akEventsEnter;
    [SerializeField] private AK.Wwise.Event[] akEventsExit;

    private void OnTriggerEnter(Collider other)
    {
        if(willTriggerEnter)
        {
            if(isValidTag(other.tag))
            {
                foreach (var akEvent in akEventsEnter)
                {
                    akEvent.Post(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(willTriggerExit)
        {
            if(isValidTag(other.tag))
            {
                foreach (var akEvent in akEventsExit)
                {
                    akEvent.Post(gameObject);
                }
            }
        }
    }
}

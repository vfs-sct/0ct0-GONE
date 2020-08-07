using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkEventManager : AkManager
{
    [Header("Wwise Events")]
    [SerializeField] private AK.Wwise.Event[] akEventsPlay;
    [SerializeField] private AK.Wwise.Event[] akEventsStop;

    [Header("Trigger Behaviours")]
    [SerializeField] private bool stopOnDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if(willTriggerEnter)
        {
            if(isValidTag(other.tag))
            {
                foreach (var akEvent in akEventsPlay)
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
                foreach (var akEvent in akEventsStop)
                {
                    akEvent.Post(gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (stopOnDestroy)
        {
            foreach (var akEvent in akEventsStop)
            {
                akEvent.Post(gameObject);
            }
        }
    }
}

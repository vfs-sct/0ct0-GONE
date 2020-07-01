using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkStateManager : AkManager
{
    [SerializeField] private AK.Wwise.State[] akStatesEnter;
    [SerializeField] private AK.Wwise.State[] akStatesExit;

    private void OnTriggerEnter(Collider other)
    {
        if(willTriggerEnter)
        {
            if(isValidTag(other.tag))
            {
                foreach (var akState in akStatesEnter)
                {
                    akState.SetValue();
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
                foreach (var akState in akStatesExit)
                {
                    akState.SetValue();
                }
            }
        }
    }

    private bool isValidTag(string tag)
    {  
        foreach (var validTag in validTags)
        {
            // Check if there are any valid tags inside the array.
            if(validTag == tag)
            {
                return true;
            }
        }
        return false;
    }
}

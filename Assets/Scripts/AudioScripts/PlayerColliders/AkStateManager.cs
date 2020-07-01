using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkStateManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.State[] akStates;
    [SerializeField] private string[] validTags;

    private void OnTriggerEnter(Collider other)
    {
        if(isValidTag(other.tag))
        {
            foreach (var akState in akStates)
            {
                akState.SetValue();
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

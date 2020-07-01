using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AkManager : MonoBehaviour
{
    [SerializeField] protected string[] validTags;

    [SerializeField] protected bool willTriggerEnter = true;
    [SerializeField] protected bool willTriggerExit = false;

    protected bool isValidTag(string tag)
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

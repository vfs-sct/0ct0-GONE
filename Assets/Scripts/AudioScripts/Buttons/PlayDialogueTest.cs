using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialogueTest : MonoBehaviour
{
    [SerializeField] private AudioSource OAKLEY1;
    [SerializeField] private AudioSource OAKLEY2;
    [SerializeField] private AudioSource OAKLEY3;
    [SerializeField] private AudioSource OAKLEY4;

    public void PlayTestSoundOne()
    {
        OAKLEY1.Play();
        Debug.Log("Playing Sound 1");
    }
    
    public void PlayTestSoundTwo()
    {
        OAKLEY2.Play();
        Debug.Log("Playing Sound 2");
    }
    
    public void PlayTestSoundThree()
    {
        OAKLEY3.Play();
        Debug.Log("Playing Sound 3");
    }
    
    public void PlayTestSoundFour()
    {
        OAKLEY4.Play();
        Debug.Log("Playing Sound 4");
    }
}

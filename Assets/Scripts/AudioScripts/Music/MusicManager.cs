using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event Play;
    [SerializeField] private AK.Wwise.Event Stop;
    [SerializeField] private AK.Wwise.Event Resume;
    [SerializeField] private AK.Wwise.Event Pause;

    [SerializeField]
    private AK.Wwise.State[] GeneralState =
    {
        // 0 = BackEnd
        // 1 = FrontEnd
    };
    
    public AK.Wwise.State GetGeneralState(int i) 
    { 
        return GeneralState[i]; 
    } 

    [SerializeField]
    private AK.Wwise.State[] BackEndState =
    {
        // 0 = GameOver
        // 1 = GamePlay
        // 2 = Home
        // 3 = Threat
    };
   
    public AK.Wwise.State GetBackEndState(int i) 
    { 
        return BackEndState[i]; 
    }

    [SerializeField]
    private AK.Wwise.State[] FrontEndState =
    {
        // 0 = Cinematic
        // 1 = Credits
        // 2 = MainMenu
    };
   
    public AK.Wwise.State GetFrontEndState(int i) 
    { 
        return FrontEndState[i]; 
    }
    
    private void Start()
    {
        Play.Post(gameObject);

        // ! Default State
        GeneralState[1].SetValue(); // General = FrontEnd 
        FrontEndState[2].SetValue(); // FrontEnd = MainMenu
    }
}

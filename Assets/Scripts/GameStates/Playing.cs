using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/Playing")]
public class Playing : GameState
{
    [System.Serializable]
    public struct WeatherCondData
    {   
        public float Time;

        public float Delta;
        public string WeatherCond;

        public WeatherCondData(float T,float D,string W)
        {
            Time = T;
            WeatherCond = W;
            Delta = D;
        }
    }


    private Player _ActivePlayer;
    [SerializeField] private CommunicationModule RelayController;
    
    
    [SerializeField] private List<WeatherCondData> WeatherConditions = new List<WeatherCondData>(); // the times MUST be in ascending order other shit breaks
    
    public Player ActivePlayer{get=>_ActivePlayer;}


    private Dictionary<float,WeatherCondData> _WeatherData = new Dictionary<float, WeatherCondData>();
    public Dictionary<float,WeatherCondData> WeatherData{get => _WeatherData;}   

    private Queue<float> WeatherTriggerQueue = new Queue<float>();

    private SalvageStorm _WeatherController;
    public SalvageStorm WeatherController{get=>_WeatherController;}


    private float PlayStartTime;

    public override void OnInitialize()
    {
        foreach (var WeatherDat in WeatherConditions)
        {
            _WeatherData.Add(WeatherDat.Time,new WeatherCondData(-1,WeatherDat.Delta,WeatherDat.WeatherCond));
            WeatherTriggerQueue.Enqueue(WeatherDat.Time);
        }
    }


    public void RegisterWeatherController(SalvageStorm NewController)
    {
        _WeatherController = NewController;
    }

    public void RegisterPlayer(Player newPlayer)
    {
        _ActivePlayer = newPlayer;
    }

    public override void OnActivate(GameState LastState)
    {
        Debug.Log("Starting Gameplay");
        RelayController.SetPlayer(_ActivePlayer.gameObject);
        RelayController.Start();
        PlayStartTime = Time.time;
       
    }








    float TimeBetweenStorms = 90; //time in seconds
    float TimeDecreasePerCycle = 10; //the decrease in time between storms each storm cycle

    bool IsTutorial = true;

    public override void OnUpdate()
    {
        if (!IsTutorial) // dont run incremental storms until the player has finished the tutorial, that would be too evil
        {







        }
    }



    public override void OnDeactivate(GameState NewState)
    {
        RelayController.Reset();
    }

    public override void Reset()
    {
        _ActivePlayer = null;
        IsTutorial = true;
    }
}

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
    
    
    /*[SerializeField]*/ private List<WeatherCondData> WeatherConditions = new List<WeatherCondData>(); // the times MUST be in ascending order other shit breaks
    
    [SerializeField] private string StormWeatherCondition = "Storm";    


    [SerializeField] private float StartingTimeBetweenStorms = 90;

    [SerializeField] private float StartingStormDuration = 5;

    [SerializeField] private float TimeDecreasePerCycle = 10; //the decrease in time between storms each storm cycle

    [SerializeField] private float StormDurationIncreasePerCycle = 1;

    [SerializeField] private float StormlerpTime = 5;
    float TimeBetweenStorms; //time in seconds
    float StormDuration;
    float NextStormTime;

    float NextStormFinishTime;

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
        TimeBetweenStorms = StartingTimeBetweenStorms;
        EndTutorial();//FOR TESTING
    }



    public void EndTutorial()
    {
        NextStormTime = Time.time + StartingTimeBetweenStorms;
        IsTutorial = false;
    }






    bool IsTutorial = true; //false for testing

    public override void OnUpdate()
    {
        if (!IsTutorial) // dont run incremental storms until the player has finished the tutorial, that would be too evil
        {
            if (Time.time >= NextStormTime)
            {
                Debug.Log("STORM");
                WeatherController.SetNewWeatherCondition(StormWeatherCondition,StormlerpTime);
                NextStormFinishTime = Time.time+StormlerpTime+ StormDuration;
                NextStormTime += 9999999;
            }
            if (Time.time >= NextStormFinishTime)
            {
                Debug.Log("STORM Done");
                WeatherController.SetNewWeatherCondition("base",StormlerpTime);
                TimeBetweenStorms -= TimeDecreasePerCycle;
                StormDuration += StormDurationIncreasePerCycle;
                NextStormTime = Time.time + TimeBetweenStorms+StormlerpTime;
                NextStormFinishTime = NextStormTime+999;
            }
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

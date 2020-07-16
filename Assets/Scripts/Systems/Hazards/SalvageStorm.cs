using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SalvageStorm : MonoBehaviour
{

    [System.Serializable]
    public struct WeatherConditionData
    {
        public string Name;
        public WeatherData Weather;

        public WeatherConditionData(string N, WeatherData W)
        {
            Name = N;
            Weather = W;
        }
    }





    [SerializeField] private List<GameObject> SalvagePrefabs;

    [SerializeField] private WeatherData BaseCondition;
    private WeatherData _BaseCondition;


    [SerializeField] private List<WeatherConditionData> WeatherConditions = new List<WeatherConditionData>();

    private List<WeatherData> _Conditions = new List<WeatherData>();

    [SerializeField] private BoxCollider StormBounds;
    [SerializeField] private float Seed;
    [SerializeField] private float Scale = 1;

    [SerializeField] private GameObject LinkedGameObject;

    //[SerializeField] private bool UpdatePosition;

    [SerializeField] private Playing PlayingGameState;


    //NOTE: base is a reserved keyword for base weather condition and should not be used in a custom condition!
    private Dictionary<string,WeatherData> Conditions = new Dictionary<string,WeatherData>();
    

    [System.Serializable] 
    public struct WeatherData
    {
        public float MinSpeed;

        public float MaxSpeed;

        public float MinSeparation;

        public float WaveInterval;

        public float LifeTime;
        
        public float Density;

        private float _WeightSum;
        public float WeightSum{get=>_WeightSum;}

        public List<float> SalvageWeights;
        public WeatherData(float mns,float mxs,float ms,float wi,float l,float d,List<float> sw, float weightSum = 100)
        {
            MinSpeed = mns;
            MaxSpeed = mxs;
            MinSeparation = ms;
            WaveInterval = wi;
            LifeTime = l;
            Density = d;
            SalvageWeights = sw;
            _WeightSum = weightSum;
        }

        public WeatherData(WeatherData Old)
        {
            MinSpeed = Old.MinSpeed;
            MaxSpeed = Old.MaxSpeed;
            MinSeparation = Old.MinSeparation;
            WaveInterval = Old.WaveInterval;
            LifeTime = Old.LifeTime;
            Density = Old.Density;
            SalvageWeights = Old.SalvageWeights;
            _WeightSum = Old.WeightSum;
        }


    }


    private Queue<List<GameObject>> Waves = new Queue<List<GameObject>>();

    private float NextWaveTime = 0;


    private float LerpStartTime = 0f;


    private float WeatherTimeChange = 1;

    WeatherData CurrentCondition;

    WeatherData OldCondition;
    bool ChangingWeather = false;

    WeatherData NewCondition;

    private void SetConditionData(WeatherData Condition)
    {
        CurrentCondition = Condition;
        
    }

    private void LerpWeather(WeatherData OldCondition,WeatherData newCondition,float delta)
    {
        List<float> temp = new List<float>();
        for (int i = 0; i < OldCondition.SalvageWeights.Count; i++)
        {
            temp.Add(Mathf.Lerp(OldCondition.SalvageWeights[i],NewCondition.SalvageWeights[i],delta));
        }


        CurrentCondition = new WeatherData(
        Mathf.Lerp(OldCondition.MinSpeed,newCondition.MinSpeed,delta),
        Mathf.Lerp(OldCondition.MaxSpeed,newCondition.MaxSpeed,delta),
        Mathf.Lerp(OldCondition.MinSeparation,newCondition.MinSeparation,delta),
        Mathf.Lerp(OldCondition.WaveInterval,newCondition.WaveInterval,delta),
        Mathf.Lerp(OldCondition.LifeTime,newCondition.LifeTime,delta),
        Mathf.Lerp(OldCondition.Density,newCondition.Density,delta),
        temp
        );

        Debug.Log(newCondition.Density);
    
    }

    private void SetNewWeatherCondition(string Condition, float time = 0)
    {
        OldCondition = CurrentCondition;
        if (Condition == "base")
        {
            NewCondition = new WeatherData(BaseCondition);
        } 
        else
        {
            NewCondition=  new WeatherData(Conditions[Condition]);
        }
        LerpStartTime =Time.time;
        WeatherTimeChange = time;
        ChangingWeather = true;
    }



    public void Awake()
    {
        Dictionary<string,WeatherData> temp;
        WeatherData tempData;
        float WeightSum = 0;
        foreach (var Weather in WeatherConditions)
        {
            WeightSum = 0;
            foreach (var weight in Weather.Weather.SalvageWeights)
            {
                WeightSum += weight;
            }
            tempData = new WeatherData
            (
                Weather.Weather.MinSpeed,
                Weather.Weather.MaxSpeed,
                Weather.Weather.MinSeparation,
                Weather.Weather.WaveInterval,
                Weather.Weather.LifeTime,
                Weather.Weather.Density,
                Weather.Weather.SalvageWeights,
                WeightSum
            );
            temp = new Dictionary<string, WeatherData>();
            temp.Add(Weather.Name,tempData);
        }

        WeightSum = 0;
        foreach (var weight in BaseCondition.SalvageWeights)
        {
            WeightSum += weight;
        }
        tempData = new WeatherData
        (
            BaseCondition.MinSpeed,
            BaseCondition.MaxSpeed,
            BaseCondition.MinSeparation,
            BaseCondition.WaveInterval,
            BaseCondition.LifeTime,
            BaseCondition.Density,
            BaseCondition.SalvageWeights,
            WeightSum
        );
        temp = new Dictionary<string, WeatherData>();
        temp.Add("base",tempData);
        _BaseCondition = tempData;

        SetConditionData(_BaseCondition);
        PlayingGameState.RegisterWeatherController(this);        
    }

    private static float SamplePerlinNoise(float CoordsX,float CoordsY,float seed = 0,float scale = 1.0f)
    {
        return Mathf.PerlinNoise(scale*(seed+CoordsX),scale*(seed+ CoordsY));
    }

    public static T SelectRandom<T>(List<T> List)
    {
        return List[Random.Range(0,List.Count)];
    }

    public static T SelectRandomWeighted<T>(Dictionary<T,float> WeightedDict,float SumOfWeights = 100)
    {
        float RandomSum = Random.Range(0,SumOfWeights);
        foreach (var Dict in WeightedDict)
        {
            if (RandomSum < Dict.Value)
            {
                return Dict.Key;
            }
            RandomSum-=Dict.Value;
        }
        return default(T);
    }

    public static T SelectRandomWeighted<T>(List<T> Data, List<float> Weights,float SumOfWeights = 100)
    {
        float RandomSum = Random.Range(0,SumOfWeights);
        for (int i = 0; i < Data.Count; i++)
        {
            if (RandomSum < Weights[i])
            {
                return Data[i];
            }
            RandomSum-= Weights[i];
        }
        return default(T);
    }



    private void SpawnWave()
    {
        GameObject Temp;
        Rigidbody TempRB;
        float WaveSeed = Seed+Time.time;
        List<GameObject> NewWave = new List<GameObject>();
        float zBounds = StormBounds.size.z/2;
        float yBounds = StormBounds.size.y/2;
        float xBounds = StormBounds.size.x/2;
        for (float z= -zBounds; z < zBounds; z+= CurrentCondition.MinSeparation)
        {
            for (float y = -yBounds; y < yBounds; y+= CurrentCondition.MinSeparation)
            {
                if(SamplePerlinNoise(y,z,WaveSeed) >= (1-CurrentCondition.Density)) //subtract from 1 to check blackvalue
                {
                    Temp = Instantiate(SelectRandomWeighted<GameObject>(SalvagePrefabs,CurrentCondition.SalvageWeights,CurrentCondition.WeightSum),new Vector3(gameObject.transform.position.x + Random.Range(-xBounds,xBounds),y+gameObject.transform.position.y,z+gameObject.transform.position.z),Random.rotation);
                    NewWave.Add(Temp);
                    Debug.Log(Temp);
                    TempRB =  Temp.GetComponent<Rigidbody>();
                    TempRB.velocity = new Vector3(-Random.Range(CurrentCondition.MinSpeed,CurrentCondition.MaxSpeed),0,0);
                    TempRB.angularVelocity = new Vector3(Random.Range(-1.2f,1.2f),Random.Range(-1.2f,1.2f),Random.Range(-1.2f,1.2f));

                }
            }
        }
        Waves.Enqueue(NewWave);
        StartCoroutine("CleanupWave");   
    }
    private void Start()
    {
        NextWaveTime = Time.time+CurrentCondition.WaveInterval;
    }



    private IEnumerator CleanupWave()
    {
        yield return new WaitForSeconds(CurrentCondition.LifeTime);
        List<GameObject> Wave = Waves.Dequeue();
        foreach (var salvage in Wave)
        {
            if (salvage!= null)
            {
                Destroy(salvage);
            }
        }
    }

    bool NotTriggered = true;
    private void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,LinkedGameObject.transform.position.y,LinkedGameObject.transform.position.z);
        if (ChangingWeather && Time.time-LerpStartTime <= WeatherTimeChange)
        {
            LerpWeather(OldCondition,NewCondition,Time.time-LerpStartTime/WeatherTimeChange);
            if (WeatherTimeChange == Time.time-LerpStartTime)
            {
                ChangingWeather = false;
            }
        }

        if (NextWaveTime <= Time.time)
            {
                SpawnWave();
                NextWaveTime = Time.time + CurrentCondition.WaveInterval;
            }
    }
}
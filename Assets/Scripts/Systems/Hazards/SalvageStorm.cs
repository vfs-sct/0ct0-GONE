using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvageStorm : MonoBehaviour
{
    [SerializeField] private List<GameObject> SalvagePrefabs;
    [SerializeField] private BoxCollider StormBounds;

    [SerializeField] private float MinSpeed = 15;

    [SerializeField] private float MaxSpeed = 25;

    [SerializeField] private float MinSeparation = 1f;
    [SerializeField] private float WaveInterval = 0.5f;

    [SerializeField] private float TimeSpentActive = 10;
    [SerializeField] private float LifeTime = 5;
    [SerializeField] private float _Density = 0.5f;
    [SerializeField] private float Seed;
    [SerializeField] private float Scale = 1;


    private Queue<List<GameObject>> Waves = new Queue<List<GameObject>>();
    private float Density;

    private float FinishTime;
    private float NextWaveTime = 0;

    private float NextCleanupTime = 0;

    private bool StopSpawning = false;

    public void Awake()
    {
        Density = 1- _Density; //invert density so that white = none
    }

    private static float SamplePerlinNoise(float CoordsX,float CoordsY,float seed = 0,float scale = 1.0f)
    {
        return Mathf.PerlinNoise(scale*(seed+CoordsX),scale*(seed+ CoordsY));
    }

    public static T SelectRandom<T>(List<T> List)
    {
        return List[Random.Range(0,List.Count)];
    }


    private void SpawnWave()
    {
        GameObject Temp;
        Rigidbody TempRB;
        float WaveSeed = Seed+Time.time;
        List<GameObject> NewWave = new List<GameObject>();
        Debug.Log(StormBounds.size);
        for (float z= 0; z < StormBounds.size.z; z+= MinSeparation)
        {
            for (float y = 0; y < StormBounds.size.y; y+= MinSeparation)
            {
                if(SamplePerlinNoise(y,z,WaveSeed) >= Density)
                {
                    Temp = Instantiate(SelectRandom<GameObject>(SalvagePrefabs),new Vector3(gameObject.transform.position.x + Random.Range(0,StormBounds.size.x),y+gameObject.transform.position.y,z+gameObject.transform.position.z),Random.rotation);
                    NewWave.Add(Temp);
                    Debug.Log(Temp);
                    TempRB =  Temp.GetComponent<Rigidbody>();
                    TempRB.velocity = new Vector3(-Random.Range(MinSpeed,MaxSpeed),0,0);
                    TempRB.angularVelocity = new Vector3(Random.Range(-1.2f,1.2f),Random.Range(-1.2f,1.2f),Random.Range(-1.2f,1.2f));

                }
            }
        }
        Waves.Enqueue(NewWave);
        StartCoroutine("CleanupWave");   
    }
    private void Start()
    {
        Debug.Log("Test Storm Triggered");
        FinishTime = Time.time+TimeSpentActive; //note: Do not use unscaled time for this or pausing will break everything!
        NextWaveTime = Time.time+WaveInterval;
    }



    private IEnumerator CleanupWave()
    {
        yield return new WaitForSeconds(LifeTime);
        List<GameObject> Wave = Waves.Dequeue();
        foreach (var salvage in Wave)
        {
            if (salvage!= null)
            {
                Destroy(salvage);
            }
        }
    }


    private void Update()
    {
        if (FinishTime <= Time.time )
        {
            StopSpawning = true;
            if (FinishTime+(LifeTime) <= Time.time)
            {
                Destroy(gameObject);
                return;
            }
            
        }
        if (NextWaveTime <= Time.time &&FinishTime > Time.time)
            {
                SpawnWave();
                NextWaveTime = Time.time + WaveInterval;
            }
    }
}
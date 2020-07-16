using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Weather/New Condition")]
public class WeatherCondition : ScriptableObject
{


   




    [SerializeField] public List<GameObject> SalvagePrefabs = new List<GameObject>();
    [SerializeField] public float MinSpeed = 15;
    [SerializeField] public float MaxSpeed = 25;

    [SerializeField] public float MinSeparation = 1f;
    [SerializeField] public float WaveInterval = 0.5f;

    [SerializeField] public float LifeTime = 5;
    [SerializeField] public float _Density = 0.05f;

    [Header("v DO NOT TOUCH v")]
    public float Density;


    private void OnValidate()
    {
        Density = 1-_Density;
    }
    private void Awake()
    {
        Density = 1-_Density;
    }
}

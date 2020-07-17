using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Weather/New Condition")]
public class WeatherCondition : ScriptableObject
{
    [SerializeField] public SalvageStorm.WeatherConditionData Data;
}

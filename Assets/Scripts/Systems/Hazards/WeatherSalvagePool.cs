using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Weather/New SalvagePool")]
public class WeatherSalvagePool : ScriptableObject
{
     [SerializeField] public List<GameObject> SalvagePrefabs;
}

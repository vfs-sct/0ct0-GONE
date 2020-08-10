using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Weather/New SalvagePool")]
public class WeatherSalvagePool : ScriptableObject
{
     [System.Serializable]
     public struct SalvagePoolData
     {
         public GameObject gameObject;
         public ObjectPool Pool;

          public SalvagePoolData(GameObject G,ObjectPool P)
          {
               gameObject = G;
               Pool = P;
          }
     }



     [SerializeField] public List<SalvagePoolData> SalvageData;
}

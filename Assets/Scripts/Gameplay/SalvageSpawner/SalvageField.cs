using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Salvage/New Salvage Field")]
public class SalvageField : ScriptableObject
{
   [SerializeField] private int SalvagePieceCount = 10;
   public int Amount{get => SalvagePieceCount;}
   [SerializeField] private List<GameObject> SalvagePrefabTypes = new List<GameObject>();

    public GameObject GetRandomSalvagePrefab()
    {
        return SalvagePrefabTypes[Mathf.FloorToInt(Random.value*(SalvagePrefabTypes.Count-1))];
    }

}

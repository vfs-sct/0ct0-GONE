using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Salvage/New Salvage")]
public class Salvage : ScriptableObject
{
    [SerializeField] private List<CraftingModule.CraftingData> SalvageResources = new List<CraftingModule.CraftingData>();
    [SerializeField] private float BaseMultiplier = 1.0f;
    public float BaseMult {get=>BaseMultiplier;}

    public void DoSalvage(Salvagable Source,ResourceInventory Target, float Percentage,float multiplier)
    {
      
    }
}

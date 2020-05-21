using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Salvage/New Salvage")]
public class Salvage : ScriptableObject
{
    [SerializeField] private List<CraftingModule.CraftingData> SalvageResources = new List<CraftingModule.CraftingData>();
    [SerializeField] private float BaseMultiplier = 1.0f;

    public void DoSalvage(Salvagable Source,ResourceInventory Target, float Percentage,float multiplier)
    {
        float NewPrecent = Source.SalvagePercentage+Percentage;
        if (NewPrecent > 1.0f) Percentage = Percentage-(NewPrecent-1.0f);
        foreach (var ResourceData in SalvageResources)
        {
            Target.TryAdd(ResourceData.resource,ResourceData.amount); // TODO prevent overfill
        }
    }
}

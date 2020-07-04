using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Salvage/New Salvage")]
public class Salvage : ScriptableObject
{
    [SerializeField] private List<CraftingModule.ItemRecipeData> SalvageResources = new List<CraftingModule.ItemRecipeData>();
}

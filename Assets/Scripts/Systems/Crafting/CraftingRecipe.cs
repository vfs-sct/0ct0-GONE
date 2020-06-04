using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Crafting/New Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] public string DisplayName = "";
    [SerializeField] private List<CraftingModule.CraftingData> _Input = new List<CraftingModule.CraftingData>();
    public List<CraftingModule.CraftingData> Input{get =>_Input;}
    [SerializeField] private CraftingModule.CraftingData _Output = new CraftingModule.CraftingData();
    public CraftingModule.CraftingData Output{get =>_Output;}
}

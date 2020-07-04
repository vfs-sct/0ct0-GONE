using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Inventory/New Item")]
public class Item : ScriptableObject
{

    [System.Serializable]
    public enum EnumItemType
    {
        Salvage,
        Craftable
    }


    public EnumItemType ItemType;
    [SerializeField] private string _Name;
    [SerializeField] private int _Size;
    [SerializeField] private float _Mass;
    [SerializeField] private Resource _ResourceType;
    [SerializeField] private Recipe _CraftingRecipe;
    public string Name{get =>_Name;}
    public int Size{get =>_Size;}
    public float Mass{get =>_Mass;}
    public bool IsSalvage{get=> ItemType == EnumItemType.Salvage ;}
    public bool IsCraftable{get=> ItemType == EnumItemType.Craftable;}
    public Recipe CraftingRecipe{get=>_CraftingRecipe;}
    public Resource ResourceType{get=>_ResourceType;}
}

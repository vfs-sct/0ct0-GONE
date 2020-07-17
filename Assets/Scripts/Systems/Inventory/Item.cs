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
    [SerializeField] private string _ItemDesc;
    [SerializeField] private Sprite _Icon;
    [SerializeField] private int _ChunkSize;
    [SerializeField] private int _Size;
    [SerializeField] private float _Mass;
    [SerializeField] private Resource _ResourceType;
    [SerializeField] private Recipe _CraftingRecipe;
    [SerializeField] private GameObject _RespawnGO;

    [SerializeField] private float _SalvageTime = 0.5f;
    public float SalvageTime{get=>_SalvageTime;}
    public string Name{get =>_Name;}
    public string ItemDesc { get => _ItemDesc; }
    public Sprite Icon { get => _Icon; }
    public int ChunkSize { get => _ChunkSize; }
    public int Size{get =>_Size;}
    public float Mass{get =>_Mass;}
    public bool IsSalvage{get=> ItemType == EnumItemType.Salvage ;}
    public bool IsCraftable{get=> ItemType == EnumItemType.Craftable;}
    public Recipe CraftingRecipe{get=>_CraftingRecipe;}
    public Resource ResourceType{get=>_ResourceType;}

    //used when the object is dropped from the inventory and spawned back into the world
    public GameObject RespawnGO { get => _RespawnGO; }
}

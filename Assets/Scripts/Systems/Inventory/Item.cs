using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Inventory/New Item")]
public class Item : ScriptableObject
{

    public bool IsResourceItem = false;
    [SerializeField] private string _Name;
    public string Name{get =>_Name;}
    [SerializeField] private int _Size;
    public int Size{get =>_Size;}
    [SerializeField] private float _Mass;
    public float Mass{get =>_Mass;}
    [SerializeField] private Resource _ResourceType;
    public Resource ResourceType{get=>_ResourceType;}
}

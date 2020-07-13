using System.Collections.Generic;
using UnityEngine;

public class RepairableInfo : MonoBehaviour
{
    [SerializeField] private string _DisplayName = null;
    [SerializeField] private int _ID;
    [SerializeField] private RepairableComponent repairableComponent = null;

    private bool _isRepaired = false;
    public string DisplayName { get => _DisplayName; }
    public int ID { get => _ID; }

    public bool IsRepaired()
    {
        return _isRepaired;
    }

    public void SetIsRepaired(bool newIsRepaired)
    {
        _isRepaired = newIsRepaired;
    }

    public List<CraftingModule.ItemRecipeData> RequiredComponents()
    {
        return repairableComponent.RequiredComponents;
    }
}

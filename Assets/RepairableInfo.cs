using System.Collections.Generic;
using UnityEngine;

public class RepairableInfo : MonoBehaviour
{
    [SerializeField] private string _DisplayName = null;
    [SerializeField] private RepairableComponent repairableComponent = null;
    //the satellite that needs to be repaired before this one can be
    [SerializeField] public GameObject previousRepair = null;

    private bool _isRepaired = false;
    public string DisplayName { get => _DisplayName; }

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

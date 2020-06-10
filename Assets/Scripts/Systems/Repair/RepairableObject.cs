using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObject : RepairableComponent
{
    [SerializeField] private GameObject BrokenModel;
    [SerializeField] private GameObject RepairedModel;

    protected override void OnRepair(RepairableComponent parent)
    {
        BrokenModel.SetActive(false);
        RepairedModel.SetActive(true);
    }
}

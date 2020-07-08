using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairModelSwap : MonoBehaviour
{
    [SerializeField] private GameObject DamagedModel;
    [SerializeField] private GameObject RepairedModel;

    public void Repair()
    {
        DamagedModel.SetActive(false);
        RepairedModel.SetActive(true);
    }
}

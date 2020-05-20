using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvagable : MonoBehaviour
{
    [SerializeField] private Tool HarvestingTool = null;
    [SerializeField] private Salvage LinkedSalvage = null;
    public float SalvagePercentage = 0;


    private void Start()
    {

    }
}

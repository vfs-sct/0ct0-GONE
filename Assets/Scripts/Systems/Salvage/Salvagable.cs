using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvagable : MonoBehaviour
{
    [SerializeField] private Tool _HarvestingTool = null;
    public Tool HarvestingTool{get=>_HarvestingTool;}
    [SerializeField] private Salvage _LinkedSalvage = null;
    public Salvage LinkedSalvage{get=>_LinkedSalvage;}
    public float SalvagePercentage = 0;

}

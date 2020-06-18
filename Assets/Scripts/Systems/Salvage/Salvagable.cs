using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvagable : MonoBehaviour
{
    [SerializeField] private Item _SalvageItem;
    public Item SalvageItem{get=>_SalvageItem;}
}

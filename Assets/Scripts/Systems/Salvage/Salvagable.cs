using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvagable : MonoBehaviour
{
    [SerializeField] private Item _SalvageItem;

    [SerializeField] private int _Amount = 0;
    public Item SalvageItem{get=>_SalvageItem;}

    public int Amount{get=>_Amount;}
}

﻿using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public UIAwake UIRoot = null;
    [SerializeField] public Resource fuel = null;

    [SerializeField] public Image barFill = null;
    [SerializeField] public float startHealth = 100;

    private ResourceInventory playerInventory;

    private void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
        fuel.SetInstanceValue(playerInventory, startHealth);
    }

    private void Update()
    {
        float charStat = fuel.GetInstanceValue(playerInventory);
        
        // set fill to health percentage
        //needs a way to get the max value from the resource instead of hardcoding division number
        barFill.fillAmount = charStat / fuel.GetMaximum();
    }
}

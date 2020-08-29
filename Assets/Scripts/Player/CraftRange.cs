using UnityEngine;
using UnityEngine.InputSystem;

public class CraftRange : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] UIModule UIModule = null;
    [SerializeField] Player Player = null;
    [SerializeField] private float AntiSpamDelay = 0.2f;
    [SerializeField] Crafting CraftingPrefab = null;

    private bool canCraft = false;
    private float LastPressedTime;

    void Start()
    {
        if (CraftingPrefab == null)
        {
            CraftingPrefab = UIModule.UIRoot.GetScreen<Crafting>();
        }
        canCraft = false;
    }

    public void OnCraftHotkey(InputAction.CallbackContext context)
    {
        if (LastPressedTime+AntiSpamDelay >  Time.unscaledTime) return; //anti spam

        LastPressedTime = Time.unscaledTime;
        Collider target;
        canCraft = Player.StationInRange(out target);  

        if(canCraft == false || GameManager.isPaused)
        {
            return;
        }
        else
        {
            ResourceInventory foundComp = null;
            if (foundComp == null){ foundComp = target.GetComponent<ResourceInventory>();}
            if (foundComp == null){ foundComp = target.GetComponentInParent<ResourceInventory>();}
            if (foundComp == null){ foundComp = target.GetComponentInChildren<ResourceInventory>();}
            Debug.Log(foundComp);
            //EVAN - craft menu open sound
            Player.GetComponent<InventoryController>().OffloadSalvage(foundComp);
            CraftingPrefab.GetComponent<ShipStorageHUD>().SetStorageOwner(foundComp);
            CraftingPrefab.GetComponent<Crafting>().SetShipInventory(foundComp);
            CraftingPrefab.gameObject.SetActive(true);
            GameManager.Pause();
            Debug.Log("Paused");
        }
    }
}

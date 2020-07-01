using UnityEngine;
using UnityEngine.InputSystem;

public class CraftRange : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] UIModule UIModule = null;
    [SerializeField] Player Player = null;

    [SerializeField] private GameObject CraftingStation;

    Crafting CraftingPrefab = null;

    private bool canCraft = false;

    void Start()
    {
        CraftingPrefab = UIModule.UIRoot.GetScreen<Crafting>();
        canCraft = false;
    }

    public void OnCraftHotkey(InputValue value)
    {
        canCraft = Player.StationInRange(canCraft);  

        if(canCraft == false || GameManager.isPaused)
        {
            return;
        }
        else
        {
            Player.GetComponent<InventoryController>().OffloadSalvage(CraftingStation.GetComponentInParent<ResourceInventory>());
            CraftingPrefab.gameObject.SetActive(true);
            GameManager.Pause();
            Debug.Log("Paused");
        }
    }
}

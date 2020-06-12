using UnityEngine;
using UnityEngine.InputSystem;

public class CraftRange : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject CraftingPrefab = null;
    [SerializeField] Player Player = null;

    private bool canCraft = false;
    // Start is called before the first frame update
    void Start()
    {
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
            CraftingPrefab.SetActive(true);
            GameManager.Pause();
            Debug.Log("Paused");
        }
    }
}

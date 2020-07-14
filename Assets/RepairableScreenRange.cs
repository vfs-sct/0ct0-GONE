using UnityEngine;
using UnityEngine.InputSystem;

public class RepairableScreenRange : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] private Playing playing = null;
    [SerializeField] public GameObject RepairableComponentScreen = null;

    private bool canOpen = false;
    private GameObject currentSat = null;

    public void OnRepairScreenHotkey(InputAction.CallbackContext context)
    {
        //Debug.LogError("Pressed");
        if(canOpen && !GameManager.isPaused)
        {
            //Debug.LogWarning("Can Open");
            //EVAN - menu open sound
            RepairableComponentScreen.SetActive(true);
            GameManager.Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        canOpen = playing.ActivePlayer.RepairComponentsInRange();
    }
}
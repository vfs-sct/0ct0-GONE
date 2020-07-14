using UnityEngine;
using UnityEngine.InputSystem;

public class RepairableScreenRange : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] private Playing playing = null;
    [SerializeField] public StationRepair StationRepairScreen = null;

    private bool canOpen = false;
    private Collider currentSat = null;

    public void OnRepairScreenHotkey(InputAction.CallbackContext context)
    {
        //Debug.LogError("Pressed");
        if(canOpen && !GameManager.isPaused)
        {
            RepairableComponent satComponent = currentSat.GetComponentInParent<RepairableComponent>();
            //Debug.LogWarning("Can Open");
            //EVAN - menu open sound
            if (satComponent != null)
            {
                StationRepairScreen.OpenScreen(satComponent);
                GameManager.Pause();
            }
            else
            {
                Debug.LogError("Did not find Repairable Component on passed collision");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        canOpen = playing.ActivePlayer.RepairComponentsInRange(out currentSat);
    }
}
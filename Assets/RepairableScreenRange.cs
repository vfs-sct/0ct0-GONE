using UnityEngine;
using UnityEngine.InputSystem;
using ScriptableGameFramework;
public class RepairableScreenRange : MonoBehaviour
{
    [SerializeField] private Playing playing = null;
    [SerializeField] public StationRepair StationRepairScreen = null;
    [SerializeField] private float AntiSpamDelay = 0.2f;

    private bool canOpen = false;
    private Collider currentSat = null;
    private float LastPressedTime;

    public void OnRepairScreenHotkey(InputAction.CallbackContext context)
    {
        if (LastPressedTime + AntiSpamDelay > Time.unscaledTime) return; //anti spam

        LastPressedTime = Time.unscaledTime;

        //Debug.LogError("Pressed");
        if (canOpen && !Game.Manager.isPaused)
        {
            RepairableComponent satComponent = currentSat.GetComponentInParent<RepairableComponent>();
            //Debug.LogWarning("Can Open");
            //EVAN - menu open sound
            if (satComponent != null)
            {
                StationRepairScreen.OpenScreen(satComponent);
                Game.Manager.Pause();
                Debug.Log("Paused");
            }
            else
            {
                var repairedComponent = currentSat.GetComponentInParent<RepairedInfo>();
                if (repairedComponent != null)
                {
                    StationRepairScreen.OpenScreen(repairedComponent.GetRepairedComponent()); ;
                }
                else
                {
                    Debug.LogError("Did not find Repairable Component on passed collision");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        canOpen = playing.ActivePlayer.RepairComponentsInRange(out currentSat);
    }
}
using UnityEngine;

public class EndSpeedPrompt : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] GameObject resourceInv = null;
    [SerializeField] GameObject resourceInvContent = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;

    bool hasBeenActivated = false;

    // Update is called once per frame
    void Update()
    {
        if(resourceInv.activeSelf)
        {
            hasBeenActivated = true;
        }
        else
        {
            if(hasBeenActivated)
            {
                TriggerNextPrompt();
            }
        }

        if(!resourceInvContent.activeSelf)
        {
            if (hasBeenActivated)
            {
                TriggerNextPrompt();
            }
        }
    }

    public void TriggerNextPrompt()
    {
        tutorialController.NextPrompt(bufferTime);
    }
}

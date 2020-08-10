using UnityEngine;

public class EndClawPrompt : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    [SerializeField] ClawTool clawTool = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;
    void Update()
    {
        if (clawTool.HasObject())
        {
            tutorialController.NextPrompt(bufferTime);
        }
    }
}

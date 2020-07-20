using UnityEngine;
using UnityEngine.InputSystem;

public class EndScanPrompt : MonoBehaviour
{
    [SerializeField] private Tutorial tutorialController = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;

    public void OnScanSalvage()
    {
        tutorialController.NextPrompt(bufferTime);
    }
}

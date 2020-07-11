using UnityEngine;

//one instance exists per warning type
public class ActivateWarning : MonoBehaviour
{
    [SerializeField] SetWarning warningPanel = null;
    [SerializeField] private string warningText = null;
    //gas cloud post processing is badly named. all warnings actually use gas cloud post processing for their post
    [SerializeField] private GasCloudPostProcessing postProcessing = null;
    //TODO: move message into serialized string on prefab
    private void OnEnable()
    {
        warningPanel.MakeActive(warningText);
    }

    public void DisableWarning()
    {
        postProcessing.Disable(warningPanel.gameObject);
    }
}

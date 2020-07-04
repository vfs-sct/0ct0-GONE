using UnityEngine;

public class ActivateWarning : MonoBehaviour
{
    [SerializeField] SetWarning warningPanel = null;
    [SerializeField] private string warningText = null;
    //TODO: move message into serialized string on prefab
    private void OnEnable()
    {
        warningPanel.MakeActive(warningText);
    }

    private void OnDisable()
    {
        warningPanel.gameObject.SetActive(false);
    }
}

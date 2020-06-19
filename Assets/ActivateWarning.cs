using UnityEngine;

public class ActivateWarning : MonoBehaviour
{
    [SerializeField] SetWarning warningPanel = null;

    //TODO: move message into serialized string on prefab
    private void OnEnable()
    {
        warningPanel.MakeActive("Flammable gas concentrations detected!");
    }

    private void OnDisable()
    {
        warningPanel.gameObject.SetActive(false);
    }
}

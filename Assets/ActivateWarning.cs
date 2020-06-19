using UnityEngine;

public class ActivateWarning : MonoBehaviour
{
    [SerializeField] SetWarning warningPanel = null;

    private void OnEnable()
    {
        warningPanel.MakeActive("Flammable gas concentrations detected!");
    }

    private void OnDisable()
    {
        warningPanel.gameObject.SetActive(false);
    }
}

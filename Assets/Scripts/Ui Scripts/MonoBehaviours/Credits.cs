using UnityEngine;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPrefab;
    public void OnEsc(InputValue value)
    {
        SwitchViewTo(MainMenuPrefab);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

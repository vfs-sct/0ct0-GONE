using UnityEngine;
using UnityEngine.InputSystem;

public class GameHUD : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    public void OnEsc(InputValue value)
    {
        if (!GameManager.isPaused)
        {
            PausePrefab.SetActive(true);
            GameManager.Pause();
            Debug.Log("Paused");
        }
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

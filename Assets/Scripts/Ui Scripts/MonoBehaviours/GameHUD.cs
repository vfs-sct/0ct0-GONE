using UnityEngine;
using UnityEngine.InputSystem;

public class GameHUD : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab = null;
    [SerializeField] GameObject GameoverPrefab = null;
    [SerializeField] GameObject CraftingPrefab = null;
    [SerializeField] GameFrameworkManager GameManager = null;

    public void OnCraftHotkey(InputValue value)
    {
        SwitchViewTo(CraftingPrefab);
    }

    public void OnEsc(InputValue value)
    {
        if (!GameManager.isPaused)
        {
            PausePrefab.SetActive(true);
            GameManager.Pause();
            Debug.Log("Paused");
        }
    }

    public void GameOver()
    {
        if (!GameManager.isPaused)
        {
            GameManager.Pause();
            Debug.Log("Paused");
        }
        GameoverPrefab.SetActive(true);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

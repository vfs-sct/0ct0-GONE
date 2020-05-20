using UnityEngine;
using UnityEngine.InputSystem;

public class GameHUD : MonoBehaviour
{
    [SerializeField] GameObject PausePrefab = null;
    [SerializeField] GameObject GameoverPrefab = null;
    [SerializeField] GameFrameworkManager GameManager = null;

    private void Update()
    {
        
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
            PausePrefab.SetActive(true);
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

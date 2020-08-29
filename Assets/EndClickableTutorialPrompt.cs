using UnityEngine;
using UnityEngine.InputSystem;

public class EndClickableTutorialPrompt : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager = null;
    [SerializeField] private Pause pauseScreen = null;
    [SerializeField] private Tutorial tutorialController = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;

    public void OnEsc(InputValue value)
    {
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(true);
        GameManager.Pause();
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void OnClick()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        tutorialController.NextPrompt(bufferTime);
    }
}

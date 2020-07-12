using UnityEngine;
public class EndToolTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    public void OnSelectTool1()
    {
        tutorialController.NextPrompt();
    }
}

using UnityEngine;
public class EndToolTutorial : MonoBehaviour
{
    [SerializeField] Tutorial tutorialController = null;
    //time before the next popup appears after this ones deactivated
    [SerializeField] float bufferTime = 3f;
    public void OnSelectTool1()
    {
        tutorialController.NextPrompt(bufferTime);
    }
}

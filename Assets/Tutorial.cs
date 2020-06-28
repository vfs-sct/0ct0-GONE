using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialPrompts;
    public int currentPrompt = 0;

    public void FirstPrompt()
    {
        tutorialPrompts[0].SetActive(true);
    }

    public void NextPrompt()
    {
        tutorialPrompts[currentPrompt].SetActive(false);
        currentPrompt++;
        if(tutorialPrompts[currentPrompt] != null)
        {
            tutorialPrompts[currentPrompt].SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}

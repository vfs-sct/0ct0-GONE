using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialPrompts;
    public int currentPrompt = 0;

    public void Start()
    {
        StartCoroutine(Wait(5));
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FirstPrompt();
    }

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

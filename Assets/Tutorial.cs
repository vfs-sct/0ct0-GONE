using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialPrompts;
    public int currentPrompt = 0;

    public void Start()
    {
        StartCoroutine(StartBufferTime(7f));
    }

    System.Collections.IEnumerator StartBufferTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FirstPrompt();
    }

    public void FirstPrompt()
    {
        tutorialPrompts[0].SetActive(true);
    }

    public void NextPrompt(float waitTime)
    {
        StartCoroutine(Wait(waitTime));
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        tutorialPrompts[currentPrompt].SetActive(false);
        yield return new WaitForSeconds(waitTime);
        StartNextPrompt();
    }

    public void StartNextPrompt()
    {
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

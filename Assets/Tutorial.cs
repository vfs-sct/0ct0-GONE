using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Playing playing = null;

    [SerializeField] GameObject[] tutorialPrompts;

    [Header("Do not touch:")]
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

    private void StartNextPrompt()
    {
        currentPrompt++;
        if(currentPrompt < tutorialPrompts.Length)
        {
            tutorialPrompts[currentPrompt].SetActive(true);
        }
        else
        {
            playing.EndTutorial();
            Debug.LogWarning("Tutorial ended");
            gameObject.SetActive(false);
        }
    }

}

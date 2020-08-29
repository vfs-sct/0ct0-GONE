using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] Playing playing = null;

    [SerializeField] GameObject[] tutorialPrompts;
    [SerializeField] public GameObject CarryWeightPrompt = null;

    [SerializeField] public Canvas[] canvases = null;

    [Header("Do not touch:")]
    public int currentPrompt = 0;

    public void EnableInventoryTutorial()
    {
        canvases[1].gameObject.SetActive(true);
        CarryWeightPrompt.SetActive(true);
    }

    public void Hide()
    {
        foreach (var entry in canvases)
        {
            entry.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        foreach (var entry in canvases)
        {
            entry.gameObject.SetActive(true);
        }
    }

    public void Start()
    {
        //Debug.LogWarning("Tutorial: " + PlayerPrefs.GetInt("TutorialEnabled"));
        if (PlayerPrefs.GetInt("TutorialEnabled") == 0)
        {
            //if(!GameManager.isPaused)
            //{
            //    Time.timeScale = 1;
            //}
            playing.EndTutorial();
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(StartBufferTime(4f));
        }
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
            //playing.EndTutorial(); //Jesse - Moving this to be triggered when the player repairs their first component
            Debug.LogWarning("Tutorial ended");
            gameObject.SetActive(false);
        }
    }

}

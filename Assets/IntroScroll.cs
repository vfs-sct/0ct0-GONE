using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroScroll : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Image bgImg = null;

    [Header("Disable Player Control")]
    [SerializeField] MovementController playerMovement = null;

    private string[] introScroll = new string[]
    {
        "...",
        " BOOTING",
        "\n\nModel: 0CT0-314",
        "\n\nTool systems: OK",
        "\n\nMovement systems: OK",
        "\n\nEnergy storage: Critical",
    };

    private float[] waitTimes = new float[]
    {
        1,
        1,
        2,
        3,
        3,
        3,
    };

    private bool fadingIn = true;
    // Start is called before the first frame update
    private float waitTime = 1f;
    private string scrollText;
    private int current = 0;
    private bool doneWaiting = true;
    private float lerpTime = 1f;
    private bool fadingOut = false;
    void Start()
    {
        //keep the game running in the background but disable player controller so that
        //debris has had a chance to spawn and get into a nice place before player enters game
        playerMovement.enabled = false;
        text.SetText(introScroll[current]);
    }

    // Update is called once per frame
    void Update()
    {
        if (doneWaiting == true)
        {
            StartCoroutine(Wait(waitTimes[current]));
        }

        if (fadingOut == true)
        {
            FadeOut();
        }
    }

    public void NewText()
    {
        //EVAN - if you want to accompany text appearing on the screen during the intro with a sound
        scrollText = scrollText + introScroll[current];
        text.SetText(scrollText);
        current++;
        doneWaiting = true;
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        doneWaiting = false;
        yield return new WaitForSeconds(waitTime);
        if (current + 1 < introScroll.Length)
        {
            NewText();
        }
        else
        {
            playerMovement.enabled = true;
            fadingOut = true;
        }
    }

    public void FadeOut()
    {
        if (bgImg.color.a > 0.01f)
        {
            var lerpBG = new Color(bgImg.color.r, bgImg.color.g, bgImg.color.b, 0f);
            bgImg.color = Color.Lerp(bgImg.color, lerpBG, Time.deltaTime * 1f / lerpTime);

            var lerpTxt = new Color(text.color.r, text.color.g, text.color.b, 0f);
            text.color = Color.Lerp(text.color, lerpBG, Time.deltaTime * 1f / lerpTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

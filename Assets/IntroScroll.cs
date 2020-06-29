using UnityEngine;
using TMPro;

public class IntroScroll : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private string[] introScroll = new string[]
    {
        "...",
        " BOOTING",
        "\n\nModel: 0CT0-314",
        "",
    };

    private bool fadingIn = true;
    // Start is called before the first frame update
    private float waitTime = 1f;
    private string scrollText;
    private int current = 0;
    private bool doneWaiting = true;
    void Start()
    {
        text.SetText(introScroll[current]);
    }

    // Update is called once per frame
    void Update()
    {
        if (doneWaiting == true)
        {
            StartCoroutine(Wait(waitTime));
        }
    }

    public void NewText()
    {
        scrollText = scrollText + introScroll[current];
        text.SetText(scrollText);
        current++;
        doneWaiting = true;
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        doneWaiting = false;
        yield return new WaitForSeconds(waitTime);
        if (introScroll[current + 1] != null)
        {
            NewText();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

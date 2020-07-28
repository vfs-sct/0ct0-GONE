using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutroScroll : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Image bgImg = null;
    [SerializeField] float endPosY;

    [Header("Disable Player Control")]
    [SerializeField] MovementController playerMovement = null;
    [SerializeField] Player playerCam = null;

    private bool fadingIn = true;
    private float waitTime = 1f;
    private int current = 0;
    private bool doneWaiting = true;
    private float fadeInLerpTime = 1f;
    private float fadeOutLerpTime = 5f;
    private float scrollLerpTime = 30f;
    private bool fadingOut = false;
    private bool isScrolling = false;
    private Vector3 endPos;

    private void Start()
    {
        var textPos = text.transform.position;
        endPos = new Vector3(textPos.x, endPosY, textPos.z);
        fadingIn = true;
    }

    public void StartOutro()
    {
        fadingIn = true;
        playerMovement.enabled = false;
        playerCam.DisableCam();
    }

    // Update is called once per frame
    void Update()
    {
        if(isScrolling == true)
        {
            Scroll();
        }

        if (fadingIn == true)
        {
            FadeIn();
        }

        if (fadingOut == true)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        var lerpBG = new Color(bgImg.color.r, bgImg.color.g, bgImg.color.b, 1f);
        if (bgImg.color.a < 0.98f)
        {
            bgImg.color = Color.Lerp(bgImg.color, lerpBG, Time.deltaTime * 1f / fadeInLerpTime);
        }
        else
        {
            bgImg.color = lerpBG;

            fadingIn = false;
            isScrolling = true;
        }
    }

    public void Scroll()
    {
        var textPos = text.transform.position;
        if (textPos.y < endPosY - 10)
        {
            textPos = Vector3.Lerp(textPos, endPos, Time.deltaTime * 1f / scrollLerpTime);
        }
        else
        {
            textPos = endPos;
            fadingOut = true;
        }

        text.transform.position = textPos;
    }

    public void FadeOut()
    {
        if (bgImg.color.a > 0.01f)
        {
            var lerpBG = new Color(bgImg.color.r, bgImg.color.g, bgImg.color.b, 0f);
            bgImg.color = Color.Lerp(bgImg.color, lerpBG, Time.deltaTime * 1f / fadeOutLerpTime);

            var lerpTxt = new Color(text.color.r, text.color.g, text.color.b, 0f);
            text.color = Color.Lerp(text.color, lerpBG, Time.deltaTime * 1f / fadeOutLerpTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        doneWaiting = false;
        yield return new WaitForSeconds(waitTime);
    }
}

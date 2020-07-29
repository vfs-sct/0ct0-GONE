using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutroScroll : MonoBehaviour
{
    [SerializeField] private Codex codex;
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Image bgImg = null;
    [SerializeField] public float endPosY;

    [Header("Disable Player Control")]
    [SerializeField] MovementController playerMovement = null;
    [SerializeField] Player playerCam = null;

    private bool fadingIn = false;
    private float waitTime = 1f;
    private bool doneWaiting = true;
    private float fadeInLerpTime = 1f;
    private float fadeOutLerpTime = 5f;
    private float scrollLerpTime = 4f;
    private bool fadingOut = false;
    private bool isScrolling = false;
    private Vector3 endPos;

    private List<string> outroScroll = new List<string>
    {
        "Memory Reconstruction: ",
        "<color=#1BD918>Complete</color>",
    };

    private void Awake()
    {
        var textPos = text.transform.localPosition;
        endPos = new Vector3(textPos.x, 0, textPos.z);

        text.SetText(outroScroll[0]);

        //fadingIn = true;
        StartOutro();
        codex.UnlockNextEntry();
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
            Debug.LogWarning("Fading in");
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
        if (text.transform.localPosition.y < 0)
        {
            Vector3 tarPos = new Vector3(endPos.x, endPos.y + 20, endPos.z);
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, tarPos, Time.deltaTime * 1f / scrollLerpTime);
        }
        else
        {
            isScrolling = false;
            text.transform.localPosition = endPos;
            text.SetText(text.text + outroScroll[1]);
            StartCoroutine(Wait(5f));
        }
    }



    public void FadeOut()
    {
        if (text.color.a > 0.01f)
        {
            //var lerpBG = new Color(bgImg.color.r, bgImg.color.g, bgImg.color.b, 0f);
            //bgImg.color = Color.Lerp(bgImg.color, lerpBG, Time.deltaTime * 1f / fadeOutLerpTime);

            var lerpTxt = new Color(text.color.r, text.color.g, text.color.b, 0f);
            text.color = Color.Lerp(text.color, lerpTxt, Time.deltaTime * 1f / fadeOutLerpTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fadingOut = true;
    }
}

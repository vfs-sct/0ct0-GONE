using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutroScroll : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager = null;
    [SerializeField] private MainMenuState mainMenuState = null;

    [SerializeField] GameObject CommSatAudioReference = null;
    [SerializeField] GameObject AmbienceAudioReference = null;

    //screen the outro sends you to after it's complete, in this case the main menu
    [SerializeField] string nextScene = null;

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
    private float fadeOutLerpTime = 3f;
    private float scrollLerpTime = 3.2f;
    private bool fadingOut = false;
    private bool isScrolling = false;
    private Vector3 endPos;

    public System.Action CodexCallback;

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

        //Debug.LogError("ON");

        //codex.UnlockNextEntry();
        //fadingIn = true;
        StartOutro();
        StartCoroutine(FinalLog(4.4f));
    }

    System.Collections.IEnumerator FinalLog(float holdTime)
    {
        yield return new WaitForSeconds(holdTime);
        CodexCallback();
    }

    public void StartOutro()
    {
        fadingIn = true;
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            playerCam.DisableCam();
        }
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
        if (text.transform.localPosition.y < 0 - 2)
        {
            Vector3 tarPos = new Vector3(endPos.x, endPos.y + 20, endPos.z);
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, tarPos, Time.deltaTime * 1f / scrollLerpTime);
        }
        else
        {
            isScrolling = false;
            text.transform.localPosition = endPos;
            StartCoroutine(DramaticHold(5.2f));
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
            mainMenuState.SetGameCompleted(true);

            // ========================
            //          AUDIO
            // ========================
            if (CommSatAudioReference == null || AmbienceAudioReference == null)
            {
                Debug.LogError("One or both sound references has not been hooked up on the GameOver prefab");
            }
            else
            {
                AkSoundEngine.PostEvent("Env_01_Stop", AmbienceAudioReference);
                AkSoundEngine.PostEvent("Communications_Array_Stop", CommSatAudioReference);
            }

            if (GameManager.isPaused)
            {
                GameManager.UnPause();
            }


            GameManager.LoadScene($"{nextScene}");
        }
    }

    System.Collections.IEnumerator DramaticHold(float holdTime)
    {
        yield return new WaitForSeconds(holdTime);
        AkSoundEngine.PostEvent("Octo_Systems_Text", gameObject);
        text.SetText(outroScroll[0] + outroScroll[1]);
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fadingOut = true;
    }
}

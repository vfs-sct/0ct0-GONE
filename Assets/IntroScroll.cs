using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class IntroScroll : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Image bgImg = null;

    [Header("Disable Player Control")]
    [SerializeField] MovementController playerMovement = null;

    private Dictionary<string, float> introScroll = new Dictionary<string, float>
    {
        { "...", 2},
        { " DIAG", .25f },
        { "NOSTICS: ", .3f },
        { "COMPLETE", 2f },
        { "\n\nModel: ", .75f},
        { "0CT0", .6f },
        { "-", .1f },
        { "3", .07f},
        { "1", .075f},
        { "4", 1.3f},
        { "\n\nTool ", .25f },
        { "and ", .25f },
        { "movement ", .25f},
        { "systems: ", 1.3f},
        { "<color=#1BD918>Functional</color>", 1.3f},
        { "\n\n<color=#DB2D2D>! WARNING !</color>", 1},
        { "\n\nMEMORY ", .5f},
        { "CORRUPTION: ", .5f},
        { "<color=#DB2D2D> 77", 1.1f },
        { ".", .1f},
        { "5", .075f},
        { "%</color>", 1f},
        { "\n\nFUEL ", .5f},
        { "LEVELS: ", .5f},
        { "<color=#DB2D2D>Low</color>", 1},
        { "\n\nPlease ", .25f},
        { "return ", .25f},
        { "to ", .25f},
        { "<color=#FFC63B>station</color> ", .25f},
        { "for ", .25f},
        { "refueling ", 4},
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
        AkSoundEngine.PostEvent("Leave_Comm_Play", gameObject);
        text.SetText(introScroll.ElementAt(current).Key);
    }

    // Update is called once per frame
    void Update()
    {
        if (doneWaiting == true)
        {
            StartCoroutine(Wait(introScroll.ElementAt(current).Value));
            current++;
        }

        if (fadingOut == true)
        {
            FadeOut();
        }
    }

    public void NewText()
    {
        //EVAN - if you want to accompany text appearing on the screen during the intro with a sound
        AkSoundEngine.PostEvent("Octo_Systems_Text", gameObject);
        scrollText = scrollText + introScroll.ElementAt(current).Key;
        text.SetText(scrollText);
        doneWaiting = true;
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        doneWaiting = false;
        yield return new WaitForSeconds(waitTime);
        if (current + 1 <= introScroll.Count)
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
        AkSoundEngine.PostEvent("Leave_Comm_Stop", gameObject);
        AkSoundEngine.PostEvent("Remembering_Oakley", gameObject);
    }
}

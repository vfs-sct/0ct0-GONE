using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI objectiveText = null;
    //how long does it take to fade in/out
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    //time to transition from "objective complete" to text for next objective
    [SerializeField] private float preTextTransitionTime;
    //how long does the player have to read it
    [SerializeField] private float displayTime;
    public bool isFirst = true;

    private int currentEvent = 0;
    private string newObjective = "New Objective: ";
    private string objectiveComplete = "Objective Complete: ";
    private string memoryReconstruction = "Reconstruction at ";
    [SerializeField] private List<Image> images;
    [SerializeField] private List<TextMeshProUGUI> text;
    private string[] objectiveShort = new string[7]
    {
        "Recharge at the station",
        "Salvage Iron and Silicon",
        "Craft and place a Nanite Satellite",
        "Repair the Station's Antenna",
        "Repair the Station's Solar Array",
        "Repair the Station's Power Storage",
        "Get a message home",
    };

    private bool fadingIn = false;
    private bool fadingOut = false;

    private float bkImageAlpha = 1f;
    private bool fadingOutText = false;

    private bool _isPreText = false;

    private bool bannerQueued = false;

    private void OnEnable()
    {
        foreach(var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        foreach (var tmp in text)
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);
        }
    }

    //"Pretext" is the "objective complete" text that shows before the next objective
    public void SetObjectiveText(bool isPreText)
    {
        _isPreText = isPreText;

        if (fadingIn == true && fadingOut == true)
        {
            //queue this banner there's already a banner playing when we try to start it
            bannerQueued = true;
        }
        else
        {
            if (objectiveShort[currentEvent] != null)
            {
                if (isPreText)
                {
                    titleText.SetText(objectiveComplete);
                    objectiveText.SetText(memoryReconstruction + ((currentEvent) * 12.5).ToString() + "%");
                }
                else
                {
                    titleText.SetText(newObjective);
                    objectiveText.SetText(objectiveShort[currentEvent]);
                    currentEvent++;
                }

                gameObject.SetActive(true);
                fadingIn = true;
            }
        }
    }

    public void FadeOutText()
    {
        bool textFinished = false;
        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);

        int finishedCount = 0;
        foreach (var texts in text)
        {
            var lerpToColor = new Color(texts.color.r, texts.color.g, texts.color.b, 0f);
            if (texts.color.a < 0.03f)
            {
                finishedCount++;
            }
            else
            {
                texts.color = Color.Lerp(texts.color, lerpToColor, dT * 1f / fadeOutTime);
            }

            if (finishedCount == text.Count)
            {
                textFinished = true;
            }
        }

        //check if everything's close enough to finished to snap to the end value
        if (textFinished)
        {
            foreach (var tmp in text)
            {
                tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);
            }
            //stop fade out, start fade in of new objective
            _isPreText = false;
            fadingOutText = false;
            SetObjectiveText(false);
        }
    }

    public void FadeIn()
    {
        bool imagesFinished = false;
        bool textFinished = false;

        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);

        //check if images are close enough to their alpha target to be considered finished, otherwise keep lerping
        int finishedCount = 0;
        foreach (var image in images)
        {
            var lerpToColor = new Color(image.color.r, image.color.g, image.color.b, bkImageAlpha);
            if (image.color.a > bkImageAlpha - 0.05f)
            {
                finishedCount++;
            }
            else
            {
                image.color = Color.Lerp(image.color, lerpToColor, dT * 1f / fadeInTime);
            }
            if(finishedCount == images.Count)
            {
                imagesFinished = true;
            }
        }
        //check if text is close enough to its alpha target to be considered finished, otherwise keep lerping
        finishedCount = 0;
        foreach (var texts in text)
        {
            var lerpToColor = new Color(texts.color.r, texts.color.g, texts.color.b, 1f);
            if (texts.color.a > 0.95f)
            {
                finishedCount++;
            }
            else
            {
                texts.color = Color.Lerp(texts.color, lerpToColor, dT * 1f / fadeInTime);
            }

            if (finishedCount == text.Count)
            {
                textFinished = true;
            }
        }

        //check if everything's close enough to finished to snap to the end value
        if(textFinished && imagesFinished)
        {
            foreach (var image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, bkImageAlpha);
            }
            foreach (var tmp in text)
            {
                tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f);
            }

            //stop fade in
            fadingIn = false;

            if (_isPreText)
            {
                //for if we want to fade out the current text and then fade in another objective
                StartCoroutine(Wait(0.2f));
            }
            else
            {
                //for if we want to fade out everything then turn the prefab off
                StartCoroutine(Wait(displayTime));
            }
        }
    }

    public void FadeOut()
    {
        bool imagesFinished = false;
        bool textFinished = false;
        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);

        int finishedCount = 0;
        foreach (var image in images)
        {
            var lerpToColor = new Color(image.color.r, image.color.g, image.color.b, 0f);
            if (image.color.a < 0.02f)
            {
                finishedCount++;
            }
            else
            {
                image.color = Color.Lerp(image.color, lerpToColor, dT * 1f / fadeOutTime);
            }
            if (finishedCount == images.Count)
            {
                imagesFinished = true;
            }
        }

        finishedCount = 0;
        foreach (var texts in text)
        {
            var lerpToColor = new Color(texts.color.r, texts.color.g, texts.color.b, 0f);
            if (texts.color.a < 0.02f)
            {
                finishedCount++;
            }
            else
            {
                texts.color = Color.Lerp(texts.color, lerpToColor, dT * 1f / fadeOutTime);
            }
            if (finishedCount == text.Count)
            {
                textFinished = true;
            }
        }

        if (textFinished && imagesFinished)
        {
            foreach (var image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            foreach (var tmp in text)
            {
                tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);
            }
            fadingOut = false;
            gameObject.SetActive(false);

            if(bannerQueued == true && _isPreText == false)
            {
                bannerQueued = false;
                SetObjectiveText(true);
            }
        }
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (_isPreText)
        {
            //for if we want to fade out the current text and then fade in another objective
            fadingOutText = true;
        }
        else
        {
            //for if we want to fade out everything then turn the prefab off
            fadingOut = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        foreach (var tmp in text)
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);
        }
        StartCoroutine(Wait(5f));
        SetObjectiveText(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn == true)
        {
            FadeIn();
        }
        if (fadingOut == true)
        {
            FadeOut();
        }
        if (fadingOutText == true)
        {
            FadeOutText();
        }
    }
}

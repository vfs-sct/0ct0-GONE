using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI objectiveText = null;
    [SerializeField] private float lerpTime;
    public bool isFirst = true;

    private string newObjective = "New Objective: ";
    private string objectiveComplete = "Objective Complete: ";
    private string memoryReconstruction = "Reconstruction at ";
    private float reconstructionPercent;
    [SerializeField] private List<Image> images;
    [SerializeField] private List<TextMeshProUGUI> text;
    private string[] objectiveShort = new string[8]
    {
        "Refuel at the station",
        "Salvage Iron Debris",
        "",
        "",
        "",
        "",
        "",
        "",
    };

    private bool fadingIn = false;
    private bool fadingOut = false;

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

        if (isFirst == false)
        {

        }
        else
        {
            isFirst = false;
        }
    }

    //"Pretext" is the "objective complete" text that shows before the next objective
    public void SetObjectiveText(bool isPreText, int index)
    {
        if(isPreText)
        {
            titleText.SetText(objectiveComplete);
            objectiveText.SetText(memoryReconstruction + ((index + 1) * 12.5).ToString() + "%");
        }
        else
        {
            titleText.SetText(newObjective);
            objectiveText.SetText(objectiveShort[index]);
        }
        fadingIn = true;
        gameObject.SetActive(true);
    }

    public void FadeIn()
    {
        bool imagesFinished = false;
        bool textFinished = false;
        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);

        int finishedCount = 0;
        foreach (var image in images)
        {
            var lerpToColor = new Color(image.color.r, image.color.g, image.color.b, 1f);
            if (image.color.a > 0.95f)
            {
                image.color = lerpToColor;
                finishedCount++;
            }
            else
            {
                image.color = Color.Lerp(image.color, lerpToColor, dT * 1f / lerpTime);
            }
            if(finishedCount == images.Count)
            {
                imagesFinished = true;
            }
        }
        finishedCount = 0;
        foreach (var texts in text)
        {
            var lerpToColor = new Color(texts.color.r, texts.color.g, texts.color.b, 1f);
            if (texts.color.a > 0.95f)
            {
                texts.color = lerpToColor;
                finishedCount++;
            }
            else
            {
                texts.color = Color.Lerp(texts.color, lerpToColor, dT * 1f / lerpTime);
            }
            if (finishedCount == text.Count)
            {
                textFinished = true;
            }
        }
        if(textFinished && imagesFinished)
        {
            fadingIn = false;
            StartCoroutine(Wait(2f));
        }
    }

    public void FadeOut()
    {
        bool imagesFinished = false;
        bool textFinished = false;
        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);
        foreach (var image in images)
        {
            int finishedCount = 0;
            var lerpToColor = new Color(image.color.r, image.color.g, image.color.b, 0f);
            if (image.color.a < 0.05f)
            {
                image.color = lerpToColor;
                finishedCount++;
            }
            else
            {
                image.color = Color.Lerp(image.color, lerpToColor, dT * 1f / lerpTime);
            }
            if (finishedCount == images.Count)
            {
                imagesFinished = true;
            }
        }
        foreach (var texts in text)
        {
            int finishedCount = 0;
            var lerpToColor = new Color(texts.color.r, texts.color.g, texts.color.b, 0f);
            if (texts.color.a < 0.05f)
            {
                texts.color = lerpToColor;
                finishedCount++;
            }
            else
            {
                texts.color = Color.Lerp(texts.color, lerpToColor, dT * 1f / lerpTime);
                
            }
            if (finishedCount == text.Count)
            {
                textFinished = true;
            }
        }
        if (textFinished && imagesFinished)
        {
            fadingOut = false;
            gameObject.SetActive(false);
        }
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fadingOut = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetObjectiveText(false, 0);
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
    }
}

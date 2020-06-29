using System.Collections;
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
    private Image[] images;
    private TextMeshProUGUI[] text;
    private string[] objectiveShort = new string[8]
    {
        "New Objective: Refuel at the station",
        "New Objective: Salvage Iron Debris",
        "",
        "",
        "",
        "",
        "",
        "",
    };

    private bool fadingIn = false;

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
    private void SetObjectiveText(bool isPreText, int index)
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
        foreach (var image in images)
        {
            int finishedCount = 0;
            var lerpToColor = new Color(image.color.r, image.color.g, image.color.b, 1f);
            if (image.color.a > 1f - 0.05f)
            {
                image.color = lerpToColor;
            }
            else
            {
                image.color = Color.Lerp(image.color, lerpToColor, dT * 1f / lerpTime);
                finishedCount++;
            }
            if(finishedCount == images.Length)
            {
                imagesFinished = true;
            }
        }
        foreach(var text in text)
        {
            int finishedCount = 0;
            var lerpToColor = new Color(text.color.r, text.color.g, text.color.b, 1f);
            if (text.color.a > 1f - 0.05f)
            {
                text.color = lerpToColor;
            }
            else
            {
                text.color = Color.Lerp(text.color, lerpToColor, dT * 1f / lerpTime);
                finishedCount++;
            }
            if (finishedCount == images.Length)
            {
                textFinished = true;
            }
        }
        if(textFinished && imagesFinished)
        {
            fadingIn = false;
        }
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetObjectiveText(false, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingIn == true)
        {
            FadeIn();
        }
    }
}

using TMPro;
using UnityEngine;


public class CommunicationZone : MonoBehaviour
{
    [SerializeField] private EventModule EventModule = null;
    [SerializeField] private CommunicationModule CommunicationManager = null;
    //ideally this would grab the object from the UIRoot functions on start but I'm trying to get stuff done fast lol
    [SerializeField] private GameObject commRangeMsg = null;

    [SerializeField] private float _Radius = 5000;

    public float Radius{get=>_Radius;}

    private int ZoneIndex = -1;

    //UI/player experience stuff
    private float expandTime = 6f;
    private bool isExpanding = false;
    private float targetRadius;
    private float currentRadius;
    private bool doneWaiting = false;
    private bool fadingText = false;
    private TextMeshProUGUI textToFade = null;
    private Color targetColor;

    void Start()
    {
        ZoneIndex = CommunicationManager.AddZone(this);
        //uncomment to see range visualled by default for debug play
        //CommunicationManager.ShowRangeIndicator(0);
        EventModule.SetCommZone(this);
        textToFade = commRangeMsg.GetComponentInChildren<TextMeshProUGUI>();

        //used to fade out UI text
        targetColor = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0f);
    }

    private void Update()
    {
        if(isExpanding) //this is a bit inefficent, it would be better to  use a coroutine
        {
            if (doneWaiting)
            {
                ExpandRange();
            }
        }

        if(fadingText)
        {
            FadingText();
        }
    }

    public void AddRange(float addRange)
    {
        StartCoroutine(Wait(6));
        //radius is its new size from the beginning, the visual expansion of the indicator is just for show
        _Radius += addRange;
        Debug.Log("New comm range: {_Radius}");
        currentRadius = 0.1f;
        targetRadius = _Radius;

        //make indicator tiny before we start expanding it so it can grow out from the ship
        CommunicationManager.ResizeRangeIndicator(0, currentRadius);
        CommunicationManager.ShowRangeIndicator(0);

        //start expanding
        CommunicationManager.SetRange(targetRadius);//actually set the radius :P
        isExpanding = true;
        doneWaiting = false;

    }

    //show the player the comm range has extended
    public void ShowCommMsg()
    {
        commRangeMsg.SetActive(true);
        doneWaiting = false;
        StartCoroutine(Wait(4));
    }
    public void HideCommMsg()
    {
        fadingText = true;
    }

    public void FadingText()
    {
        if(textToFade.color.a <= targetColor.a - 0.3f)
        {
            textToFade.color = targetColor;
            fadingText = false;
            return;
        }
        textToFade.color = Color.Lerp(textToFade.color, targetColor, Time.deltaTime * 1f / 0.5f);
    }

    //this is horrible tangled code
    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        doneWaiting = true;
        if(commRangeMsg.activeSelf)
        {
            commRangeMsg.SetActive(false);
        }
    }

    //when an event extends the comm range, make the indicator visible and show them the range expanding out
    public void ExpandRange()
    {
        if(currentRadius >= targetRadius - (targetRadius * 0.65f))
        {
            currentRadius = targetRadius;
            CommunicationManager.ResizeRangeIndicator(0, currentRadius);
            CommunicationManager.HideRangeIndicator(0);
            isExpanding = false;
            ShowCommMsg();
            return;
        }
        currentRadius = Mathf.Lerp(currentRadius, targetRadius, Time.deltaTime * 1f / expandTime);
        CommunicationManager.ResizeRangeIndicator(0, currentRadius);
    }
}

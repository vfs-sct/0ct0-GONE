//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] public Image blackScreen;
    [SerializeField] public float lerpTime;
    [SerializeField] public Color originalColour;
    private Color lerpToColor;
    [SerializeField] public float delayTime = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        //blackScreen.color = originalColour;
        lerpToColor = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);
        if(delayTime > 0 )
        {
            delayTime -= dT;
            return;
        }
        if (blackScreen.color.a > 0.01f)
        {
            blackScreen.color = Color.Lerp(blackScreen.color, lerpToColor, dT * 1f / lerpTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        blackScreen.color = originalColour;
    }
}

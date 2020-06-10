using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] public Image blackScreen;
    [SerializeField] public float lerpTime;
    private Color lerpToColor;

    // Start is called before the first frame update
    void Start()
    {
        lerpToColor = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (blackScreen.color.a > 0.01f)
        {
            blackScreen.color = Color.Lerp(blackScreen.color, lerpToColor, Time.deltaTime * 1f / lerpTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

//Kristin Ruff-Frederickson | Copyright 2020©
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using ScriptableGameFramework;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Win : MonoBehaviour
{
    [SerializeField] GameObject CommSatAudioReference = null;
    [SerializeField] GameObject AmbienceAudioReference = null;
    
    [Header("Animatable Components")]
    [SerializeField] public float changeSpeed;

    [Header("Next Scene")]
    [SerializeField] string menuScene = null;
    private bool fadeInMenu = false;

    List<Image> imageColours = new List<Image>();
    List<TextMeshProUGUI> textColours = new List<TextMeshProUGUI>();

    private void Awake()
    {
        //Image[] imageChildren = gameObject.GetComponentsInChildren<Image>();
        //foreach (Image child in imageChildren)
        //{
        //    if (child.color != null)
        //    {
        //        imageColours.Add(child);
        //    }
        //}

        //TextMeshProUGUI[] textChildren = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        //foreach (TextMeshProUGUI child in textChildren)
        //{
        //    if (child.color != null)
        //    {
        //        textColours.Add(child);
        //    }
        //}
    }

    private void OnEnable()
    {
        //Cursor.visible = true;
        AkSoundEngine.PostEvent("Remembering_Oakley", gameObject);
        //fadeInMenu = true;
    }

    void Update()
    {
        //if(fadeInMenu == true)
        //{
        //    FadeInMenu();
        //}
    }

    //private void FadeInMenu()
    //{
    //    //check first if we're close enough to snap the alpha to 1f and stop the lerp
    //    if (textColours[textColours.Count - 1].color.a > 1.0f - 0.04f)
    //    {
    //        foreach (var element in imageColours)
    //        {
    //            element.color = new Color(element.color.r, element.color.g, element.color.b, 1f);
    //        }
    //        foreach (var element in textColours)
    //        {
    //            element.color = new Color(element.color.r, element.color.g, element.color.b, 1f);
    //        }

    //        fadeInMenu = false;
    //    }

    //    float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);

    //    float fadeInSpeed = changeSpeed * 0.3f;
    //    //lerp alphas to fade in the game over menu
    //    foreach (var element in imageColours)
    //    {
    //        var targetColor = new Color(element.color.r, element.color.g, element.color.b, 1f);
    //        element.color = Color.Lerp(element.color, targetColor, dT * 1f / fadeInSpeed);
    //    }
    //    foreach (var element in textColours)
    //    {
    //        var targetColor = new Color(element.color.r, element.color.g, element.color.b, 1f);
    //        element.color = Color.Lerp(element.color, targetColor, dT * 1f / fadeInSpeed);
    //    }
    //}

    //private void OnDisable()
    //{
    //    Cursor.visible = false;
    //}

    //void DoMainMenu()
    //{
    //    // ========================
    //    //          AUDIO
    //    // ========================
    //    if (CommSatAudioReference == null || AmbienceAudioReference == null)
    //    {
    //        Debug.LogError("One or both sound references has not been hooked up on the GameOver prefab");
    //    }
    //    else
    //    {
    //        AkSoundEngine.PostEvent("Env_01_Stop", AmbienceAudioReference);
    //        AkSoundEngine.PostEvent("Communications_Array_Stop", CommSatAudioReference);
    //    }

    //    if (GameManager.isPaused)
    //    {
    //        GameManager.UnPause();
    //    }

    //    GameManager.LoadScene($"{menuScene}");
    //}

    //public void OnClickMainMenu()
    //{
    //    DoMainMenu();
    //}
}

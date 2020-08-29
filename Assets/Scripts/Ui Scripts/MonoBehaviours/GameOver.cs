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

public class GameOver : MonoBehaviour
{

    [SerializeField] GameObject ConfirmationPrefab = null;
    [SerializeField] GameObject CommSatAudioReference = null;
    [SerializeField] GameObject AmbienceAudioReference = null;
    
    [Header("Animatable Components")]
    [SerializeField] PostProcessVolume postProcess;
    [SerializeField] public float changeSpeed;
    [SerializeField] public float vigTarget;
    [SerializeField] public float grainTarget;
    [SerializeField] public float saturationTarget;

    [Header("Next Scene")]
    [SerializeField] string menuScene = null;
    private Vignette vignette;
    private ColorGrading colorgrad;
    private Grain grain;

    private float origVigIntensity;
    private float origGrainIntensity;
    private float origSaturation;

    private float vigSpeed;

    private bool deathAnimation = false;
    private bool fadeInMenu = false;

    List<Image> imageColours = new List<Image>();
    List<TextMeshProUGUI> textColours = new List<TextMeshProUGUI>();

    private void Awake()
    {
        postProcess.profile.TryGetSettings<Vignette>(out vignette);
        postProcess.profile.TryGetSettings<Grain>(out grain);
        postProcess.profile.TryGetSettings<ColorGrading>(out colorgrad);
        vignette.active = true;

        Image[] imageChildren = gameObject.GetComponentsInChildren<Image>();
        foreach (Image child in imageChildren)
        {
            if (child.color != null)
            {
                imageColours.Add(child);
            }
        }

        TextMeshProUGUI[] textChildren = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI child in textChildren)
        {
            if (child.color != null)
            {
                textColours.Add(child);
            }
        }
    }

    private void OnEnable()
    {
        Cursor.visible = true;

        //set all alphas on the gameover menu to 0 so we can fade it in when death animation is over
        foreach(var element in imageColours)
        {
            element.color = new Color(element.color.r, element.color.g, element.color.b, 0f);
        }
        foreach (var element in textColours)
        {
            element.color = new Color(element.color.r, element.color.g, element.color.b, 0f);
        }

        origVigIntensity = vignette.intensity.value;
        origGrainIntensity = grain.intensity.value;
        origSaturation = colorgrad.saturation.value;

        //vignette comes in fast than screen fades to black
        vigSpeed = changeSpeed * 0.6f;

        deathAnimation = true;
    }

    void Update()
    {
        if(deathAnimation == true)
        {
            DeathAnimation();
        }
        if(fadeInMenu == true)
        {
            FadeInMenu();
        }
    }

    private void DeathAnimation()
    {
        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);
        grain.intensity.value = Mathf.Lerp(grain.intensity.value, grainTarget, dT * 1f / changeSpeed);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, vigTarget, dT * 1f / vigSpeed);
        colorgrad.saturation.value = Mathf.Lerp(colorgrad.saturation.value, saturationTarget, dT * 1f / changeSpeed);

        if (grain.intensity.value >= grainTarget - 0.03f)
        {
            grain.intensity.value = grainTarget;
            vignette.intensity.value = vigTarget;
            colorgrad.saturation.value = saturationTarget;

            deathAnimation = false;
            fadeInMenu = true;
        }
    }

    private void FadeInMenu()
    {
        //check first if we're close enough to snap the alpha to 1f and stop the lerp
        if (textColours[textColours.Count - 1].color.a > 1.0f - 0.04f)
        {
            foreach (var element in imageColours)
            {
                element.color = new Color(element.color.r, element.color.g, element.color.b, 1f);
            }
            foreach (var element in textColours)
            {
                element.color = new Color(element.color.r, element.color.g, element.color.b, 1f);
            }

            fadeInMenu = false;
        }

        float dT = Mathf.Min(Time.unscaledDeltaTime, 1f / 30f);

        float fadeInSpeed = changeSpeed * 0.3f;
        //lerp alphas to fade in the game over menu
        foreach (var element in imageColours)
        {
            var targetColor = new Color(element.color.r, element.color.g, element.color.b, 1f);
            element.color = Color.Lerp(element.color, targetColor, dT * 1f / fadeInSpeed);
        }
        foreach (var element in textColours)
        {
            var targetColor = new Color(element.color.r, element.color.g, element.color.b, 1f);
            element.color = Color.Lerp(element.color, targetColor, dT * 1f / fadeInSpeed);
        }
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        vignette.intensity.value = origVigIntensity;
        grain.intensity.value = origGrainIntensity;
        colorgrad.saturation.value = origSaturation;
        vignette.active = false;
    }

    public void OnClickLoad()
    {
        Debug.Log("No save data to load");
    }

    //used by the Confirmation screen
    void DoMainMenu()
    {
        // ========================
        //          AUDIO
        // ========================
        if (CommSatAudioReference == null || AmbienceAudioReference == null)
        {
            Debug.LogError("One or both sound references has not been hooked up on the GameOver prefab");
        }
        else
        {
            AkSoundEngine.PostEvent("White_Noise_Env_Stop", AmbienceAudioReference);
            AkSoundEngine.PostEvent("Communications_Array_Stop", CommSatAudioReference);
        }

        if (Game.Manager.isPaused)
        {
            Game.Manager.UnPause();
        }

        Game.Manager.LoadScene($"{menuScene}");
    }

    public void OnClickMainMenu()
    {
        var confirmation = ConfirmationPrefab.GetComponent<Confirmation>();

        confirmation.titleText.SetText("Main Menu?");

        confirmation.bodyText.GetComponent<TMPro.TMP_Text>().SetText("Are you sure you want to return to the main menu?");

        confirmation.clickConfirmCallback = DoMainMenu;

        ConfirmationPrefab.SetActive(true);
    }

    //used by the Confirmation screen
    void DoQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else         
        Application.Quit();   
#endif
       
    }

    public void OnClickQuit()
    {
        var confirmation = ConfirmationPrefab.GetComponent<Confirmation>();

        confirmation.titleText.SetText("Quit?");

        confirmation.bodyText.GetComponent<TMPro.TMP_Text>().SetText("Are you sure you want to quit to desktop?");

        AkSoundEngine.PostEvent("Not_Enough_Resources", gameObject);

        confirmation.clickConfirmCallback = DoQuit;

        ConfirmationPrefab.SetActive(true);
    }
}

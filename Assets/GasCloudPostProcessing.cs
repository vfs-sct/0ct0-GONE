using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GasCloudPostProcessing : MonoBehaviour
{
    [SerializeField] GameObject parent = null;

    [Header("Animatable Components")]
    [SerializeField] PostProcessVolume post;

    [Header("Animation Values")]
    [SerializeField] public float flashSpeed;
    [SerializeField] public float vigMax;
    [SerializeField] public float vigMin;
    [SerializeField] public float chromAbMax;
    [SerializeField] public float chromAbMin;
    [SerializeField] public Color colorgradMax;
    [SerializeField] public Color colorgradMin;

    private Vignette vignette;
    private ChromaticAberration chromAb;
    private ColorGrading colorgrad;

    private bool isDisabling = false;
    private bool isEnabling = false;

    private GameObject warningPanel;

    void Awake()
    {
        post.profile.TryGetSettings<Vignette>(out vignette);
        post.profile.TryGetSettings<ChromaticAberration>(out chromAb);
        post.profile.TryGetSettings<ColorGrading>(out colorgrad);
        isEnabling = true;
    }
    private void OnEnable()
    {
        chromAb.intensity.value = chromAbMin;
        vignette.intensity.value = vigMin;
        colorgrad.colorFilter.value = colorgradMin;
        isEnabling = true;
    }

    public void Disable(GameObject WarningPanel)
    {
        warningPanel = WarningPanel;
        isEnabling = false;
        isDisabling = true;
    }

    private void Enabling()
    {
        chromAb.intensity.value = Mathf.Lerp(chromAb.intensity.value, chromAbMax, Time.deltaTime * 1f / flashSpeed);
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, vigMax, Time.deltaTime * 1f / flashSpeed);
        colorgrad.colorFilter.value = Color.Lerp(colorgrad.colorFilter.value, colorgradMax, Time.deltaTime * 1f / flashSpeed);

        if(chromAb.intensity.value >= chromAbMax - 0.04f)
        {
            isEnabling = false;
        }
    }

    private void Disabling()
    {
        //Debug.LogWarning("DISABLING IS HAPPENING");
        chromAb.intensity.value = Mathf.Lerp(chromAb.intensity.value, chromAbMin, Time.deltaTime * 1f / (flashSpeed / 2));
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, vigMin, Time.deltaTime * 1f / (flashSpeed / 2));
        colorgrad.colorFilter.value = Color.Lerp(colorgrad.colorFilter.value, colorgradMin, Time.deltaTime * 1f / (flashSpeed / 2));

        if (chromAb.intensity.value <= chromAbMin + 0.04f)
        {
            isDisabling = false;
            warningPanel.SetActive(false);  
            parent.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabling == true)
        {
            Enabling();
            return;
        }
        if (isDisabling == true)
        {
            Disabling();
            return;
        }
    }
}

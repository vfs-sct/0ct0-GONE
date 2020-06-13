using UnityEngine;
using UnityEngine.UI;

public class GammaSlider : MonoBehaviour
{
    //NOTE: Slider values operate on a scale of 0 to 1
    [SerializeField] public Slider gammaSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Gamma"))
        {
            gammaSlider.value = PlayerPrefs.GetFloat("Gamma");
        }
        else
        {
            gammaSlider.value = Shader.GetGlobalFloat("gamma");
        }
    }
    public void SetGammaSlider()
    {
        Debug.Log(Shader.GetGlobalFloat("gamma").ToString());
        var newGamma = gammaSlider.value;
        Shader.SetGlobalFloat("gamma", newGamma);
        PlayerPrefs.SetFloat("Gamma", newGamma);
        PlayerPrefs.Save();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GammaSlider : MonoBehaviour
{
    //NOTE: Slider values operate on a scale of 0 to 1
    [SerializeField] public Slider gammaSlider;

    private void Start()
    {
        gammaSlider.value = PlayerPrefs.GetFloat("Gamma");
    }
    public void SetGammaSlider()
    {
        var newGamma = gammaSlider.value;
        Shader.SetGlobalFloat("gamma", newGamma);
        PlayerPrefs.SetFloat("Gamma", newGamma);
        PlayerPrefs.Save();
    }
}

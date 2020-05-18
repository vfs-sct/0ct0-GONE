using UnityEngine;
using UnityEngine.UI;

public class GammaSlider : MonoBehaviour
{
    //NOTE: Slider values operate on a scale of 0 to 1
    [SerializeField] public Slider gammaSlider;
    
    private float newGamma;

    private void Start()
    {
        gammaSlider.value = Shader.GetGlobalFloat("gamma");
    }
    public void SetGammaSlider()
    {
        newGamma = gammaSlider.value;
        Shader.SetGlobalFloat("gamma", newGamma);
    }
}

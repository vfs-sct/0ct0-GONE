using UnityEngine;
using UnityEngine.UI;

public class GammaSlider : MonoBehaviour
{
    //NOTE: Slider values operate on a scale of 0 to 1
    [SerializeField] public Slider gammaSlider;
    [SerializeField] public float defaultGamma = 0.5f;
    private float newGamma;

    private void Start()
    {
        gammaSlider.value = defaultGamma;
    }
    public void SetGammaSlider()
    {
        newGamma = gammaSlider.value;
    }
}

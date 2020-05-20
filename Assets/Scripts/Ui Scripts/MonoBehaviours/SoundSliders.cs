using UnityEngine;
using UnityEngine.UI;

public class SoundSliders : MonoBehaviour
{
    //NOTE: Slider values operate on a scale of 0 to 1
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public float defaultVolume = 0.5f;
    [SerializeField] public string WWiseChannel;
    private float newVolume;

    private void Start()
    {
        volumeSlider.value = defaultVolume;
    }
    public void SetVolumeSlider()
    {
        newVolume = volumeSlider.value;
        AkSoundEngine.SetRTPCValue(WWiseChannel, newVolume);
    }
}

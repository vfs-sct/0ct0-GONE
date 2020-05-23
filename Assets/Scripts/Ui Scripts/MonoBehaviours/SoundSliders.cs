using UnityEngine;
using UnityEngine.UI;

public class SoundSliders : MonoBehaviour
{
    //NOTE: Slider values operate on a scale of 0 to 1
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public string WWiseChannel;
    private float newVolume;
    private float value;

    private void Start()
    {
        if (PlayerPrefs.HasKey(WWiseChannel))
        {
            value = PlayerPrefs.GetFloat(WWiseChannel);
        }
        else
        {
            value = 0;
        }
        //int value_type = 0;
        //AkSoundEngine.GetRTPCValue(WWiseChannel, null, 0, out value, ref value_type);
        volumeSlider.value = value;
    }
    public void SetVolumeSlider()
    {
        newVolume = volumeSlider.value;
        AkSoundEngine.SetRTPCValue(WWiseChannel, newVolume);

        PlayerPrefs.SetFloat(WWiseChannel, newVolume);
        PlayerPrefs.Save();
    }
}

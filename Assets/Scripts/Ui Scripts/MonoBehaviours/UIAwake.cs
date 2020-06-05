using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.UI;

public class UIAwake : MonoBehaviour
{
    [SerializeField] GameObject DebugPrefab = null;
    [SerializeField] public float gammaDefault = 2.2f;

    private Player player = null;
    // Start is called before the first frame update

    public string[] VolumePrefs = new string[]
    {
        "MasterVolume",
        "MusicVolume",
        "SFXVolume",
        "DialogueVolume",
    };

    public Player GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
        return player;
    }

    void Start()
    {
        var camera = Camera.main;

        foreach (var canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.worldCamera = camera;
        }

        camera.gameObject.AddComponent<PostProcessing>().material = Resources.Load<Material>("GammaMaterial");

        if (!PlayerPrefs.HasKey("Gamma"))
        {
            PlayerPrefs.SetFloat("Gamma", gammaDefault);
            Shader.SetGlobalFloat("gamma", gammaDefault);
            PlayerPrefs.Save();
        }
        else
        {
            Shader.SetGlobalFloat("gamma", PlayerPrefs.GetFloat("Gamma"));
        }


        foreach (var volumePref in VolumePrefs)
        {
            if (PlayerPrefs.HasKey(volumePref))
            {
                var value = PlayerPrefs.GetFloat(volumePref);
                AkSoundEngine.SetRTPCValue(volumePref, value);
            }
            else
            {
                float defaultValue = .75f;
                AkSoundEngine.SetRTPCValue(volumePref, defaultValue);
                PlayerPrefs.SetFloat(volumePref, defaultValue);
                PlayerPrefs.Save();
            }
        }

    }

    public void OnDebug(InputValue value)
    {
        if (!DebugPrefab.activeSelf)
        {
            DebugPrefab.SetActive(true);
        }
    }
}
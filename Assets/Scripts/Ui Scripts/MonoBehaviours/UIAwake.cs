using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.UI;

public class UIAwake : MonoBehaviour
{
    [SerializeField] GameObject DebugPrefab = null;
    [SerializeField] public float gammaDefault = 2.2f;
    //inverted default must be 1 or -1
    //currently set to inverted by default
    [SerializeField] public int invertedCamDefault = 1;
    [SerializeField] public float lookSensitivityDefault = 0.9f;
    [SerializeField] public GameObject fadeIn = null;

    private Player player = null;

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

    public void UpdateInvertCam()
    {
        if (player != null)
        {
            player.invertedCam = PlayerPrefs.GetInt("InvertedCam");
        }
        else
        {
            //main menu doesnt have a player so theres nothing to update
            Debug.Log("Did not update camera inversion because player reference is null. Are you in Main Menu, or Gameplay?");
        }
    }

    public void UpdateLookSensitivity()
    {
        if (player != null)
        {
            player.lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity");
        }
        else
        {
            //main menu doesnt have a player so theres nothing to update
            Debug.Log("Did not update look sensitivity because player reference is null. Are you in Main Menu, or Gameplay?");
        }
    }

    void Start()
    {
        Cursor.visible = false;
        var camera = Camera.main;

        foreach (var canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.worldCamera = camera;
        }

        camera.gameObject.AddComponent<PostProcessing>().material = Resources.Load<Material>("GammaMaterial");

        fadeIn.SetActive(true);

        //set camera inversion base on player prefs, or set to default
        if (PlayerPrefs.HasKey("InvertedCam"))
        {
            UpdateInvertCam();
        }
        else
        {
            player.invertedCam = invertedCamDefault;
        }

        //set camera inversion base on player prefs, or set to default
        if (PlayerPrefs.HasKey("LookSensitivity"))
        {
            UpdateLookSensitivity();
        }
        else
        {
            //default sensitivity
            player.lookSensitivity = lookSensitivityDefault;
        }

        //set gamma based on saved player prefs, or set to default
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
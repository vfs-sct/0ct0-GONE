//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.InputSystem;

public class UIAwake : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager = null;
    [SerializeField] GameObject DebugPrefab = null;
    [SerializeField] Texture2D customCursor = null;
    [SerializeField] public float gammaDefault = 2.2f;
    //invertedCamDefault must be either 1 or -1
    //If it is set to 1, the camera will be inverted by default
    [SerializeField] public int invertedCamDefault = 1;
    [SerializeField] public float lookSensitivityDefault = 0.5f;
    [SerializeField] public GameObject fadeIn = null;
    [SerializeField] public Codex codex = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

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
            Debug.Log("Did not update camera inversion because player reference is null. Are you in Main Menu?");
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
            Debug.Log("Did not update look sensitivity because player reference is null. Are you in Main Menu?");
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //find and set the camera so we can apply gamma changes
        var camera = Camera.main;

        //stop cursor from going off the screen
        Cursor.lockState = CursorLockMode.Confined;
        //set custom cursor
        Cursor.SetCursor(customCursor, hotSpot, cursorMode);

        foreach (var canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.worldCamera = camera;
        }

        camera.gameObject.AddComponent<PostProcessing>().material = Resources.Load<Material>("GammaMaterial");

        //fade in from black when switching screens
        fadeIn.SetActive(true);

        if (PlayerPrefs.HasKey("TutorialEnabled") == false)
        {
            PlayerPrefs.SetInt("TutorialEnabled", 1);
            PlayerPrefs.Save();
        }

        //Debug.LogWarning("Awake: " + PlayerPrefs.GetInt("TutorialEnabled"));

        //set camera inversion base on player prefs, or set to default
        if (PlayerPrefs.HasKey("InvertedCam"))
        {
            UpdateInvertCam();
        }
        else
        {
            if (player != null)
            {
                player.invertedCam = invertedCamDefault;
            }
            PlayerPrefs.SetInt("InvertedCam", invertedCamDefault);
            PlayerPrefs.Save();
        }

        //set camera inversion base on player prefs, or set to default
        if (PlayerPrefs.HasKey("LookSensitivity"))
        {
            UpdateLookSensitivity();
        }
        else
        {
            if (player != null)
            {
                //default sensitivity
                player.lookSensitivity = lookSensitivityDefault;
            }
            PlayerPrefs.SetFloat("LookSensitivity", lookSensitivityDefault);
            PlayerPrefs.Save();
        }

        //set gamma based on saved player prefs, or set to default
        if (!PlayerPrefs.HasKey("Gamma"))
        {
            Shader.SetGlobalFloat("gamma", gammaDefault);
            PlayerPrefs.SetFloat("Gamma", gammaDefault);
            PlayerPrefs.Save();
        }
        else
        {
            Shader.SetGlobalFloat("gamma", PlayerPrefs.GetFloat("Gamma"));
        }

        //loop through the saved prefs for each audio channel (master/music/sfx/dialogue) and set the volumes, or set to defaults
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

        //Try to get player right off the bat and pass it to UIAwake
        //Means anything using UIModule can get the player through the UIRoot variable
        gameObject.GetComponent<UIRoot>().player = GetPlayer();

    }

    //open the debug panel when associated input is pressed
    public void OnDebug(InputValue value)
    {
        if (!DebugPrefab.activeSelf)
        {
            DebugPrefab.SetActive(true);
        }
    }
}
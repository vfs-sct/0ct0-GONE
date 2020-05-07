using System.Collections;
using System.Collections.Generic;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/Core/GameManager")]
public class GameFrameworkManager : ScriptableObject
{   
    [SerializeField] 
    private ModuleManager LinkedModuleManager;
    public ModuleManager moduleManager{get =>LinkedModuleManager;}

    [Header("Game State System")]


    [SerializeField]
    private List<GameStateData> GameStates = new List<GameStateData>();
    private List<GameState> CachedGameStates = new  List<GameState>();

    private GameState ActiveState;
    private Scene ActiveScene;

    private float lastTimescale = 1;

    private bool Paused = false;
    public bool isPaused{get => Paused;}

    delegate void GameManagerDelegate(GameFrameworkManager GameManager);
    private GameManagerDelegate OnExitGame;
    private GameManagerDelegate OnPauseGame;
    private GameManagerDelegate OnResumeGame;


    [System.Serializable]
    struct GameStateData
    {
        public bool Enabled;
        public bool IsLinkedToScene;
        public string LinkedScene;
        public GameState State;

        public GameStateData(bool E,bool L, string LS, GameState GM)
        {
            Enabled = E;
            IsLinkedToScene = L;
            LinkedScene = LS;
            State = GM;
        }
    }





    private void DummyGMDelegate(GameFrameworkManager GameManager)
    {
        
    }

    public void Pause()
    {
        lastTimescale = Time.timeScale;
        Time.timeScale = 0;
        moduleManager.StopTicking = true;
        Paused = true;
        if (OnPauseGame != null) OnPauseGame(this);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        moduleManager.StopTicking = false;
        Paused = false;
        if (OnPauseGame != null) OnResumeGame(this);
    }

    
    void GameStateUpdate()
    {
        if (!Application.isPlaying) return;

        if (ActiveState && ActiveState.CanTick) 
        {
            ActiveState.OnUpdate();
        }
    }
   

    private void GameStatesInit()
    {
        CachedGameStates.Clear(); //clear the old cached gamestates
        if (GameStates.Count == 0) return;//exit out if gamestates are empty
        foreach (var StateData in GameStates)
        {
            if (StateData.State != null)
            {
                Debug.Log("Initializing "+ StateData.State + ":\n");
                StateData.State.Initalize();
                CachedGameStates.Add(StateData.State);
            }
        }
    }


    private void MainLoopInit()
    {
        PlayerLoopSystem unityMainLoop = PlayerLoop.GetDefaultPlayerLoop();
        PlayerLoopSystem[] unityCoreSubSystems = unityMainLoop.subSystemList;
        PlayerLoopSystem[] unityCoreUpdate = unityCoreSubSystems[4].subSystemList;
        PlayerLoopSystem ScriptModuleUpdate = new PlayerLoopSystem()
        {
            updateDelegate = LinkedModuleManager.ModuleUpdateTick,
            type = typeof(PlayerLoop)
        };

        PlayerLoopSystem gameStateUpdate = new PlayerLoopSystem()
        {
            updateDelegate = GameStateUpdate,
            type = typeof(PlayerLoop)
        };

        PlayerLoopSystem[] newCoreUpdate = new PlayerLoopSystem[(unityCoreUpdate.Length+1)];
        newCoreUpdate[0] = gameStateUpdate;
        newCoreUpdate[1] = ScriptModuleUpdate;
        newCoreUpdate[2] = unityCoreUpdate[0];
        newCoreUpdate[3] = unityCoreUpdate[1];
        newCoreUpdate[4] = unityCoreUpdate[2];

        unityCoreSubSystems[4].subSystemList = newCoreUpdate;

        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = unityCoreSubSystems;
        PlayerLoop.SetPlayerLoop(systemRoot);
    }

    private void OnEnable()
    {

        //Application.targetFrameRate = 60;
        if (!Application.isEditor) return;
        Initalize();
        Debug.Log("Running GameManager in Editor");
    }

    private void Awake()
    {
        if (Application.isEditor) return;
        Initalize();
        Debug.Log("Running GameManager in Build");
    }

    private void CoreEventsInit()
    {
        OnExitGame += DummyGMDelegate;
        OnPauseGame += DummyGMDelegate;
        OnResumeGame += DummyGMDelegate;
    }

    private void Initalize()
    {

        SceneManager.sceneLoaded +=  OnSceneLoad;

        LinkedModuleManager.Initialize();

        Debug.Log("----------------------------------\n");
        Debug.Log("Linking Core Events\n");
        CoreEventsInit();
        Debug.Log("Finished Linking Core Events\n");

        Debug.Log("Loading Gamestates\n");
        GameStatesInit();
        Debug.Log("Finished Loading Gamestates\n");

        Debug.Log("Injecting Delegates into to Unity Player Loop\n");
        MainLoopInit();
        Debug.Log("Done\n");

        Debug.Log("===============================\n");
        Debug.Log("======Initializion Complete======\n");
        Debug.Log("===============================\n");
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded");
        
        moduleManager.ModuleStartOnLoad();
    }

    public void Start()
    {

    }

    public void QuitGame()
    {
        OnExitGame(this);
        Application.Quit();
    }

    public T GetModule<T>() where T : Module
    {
        return LinkedModuleManager.GetModule<T>();
    }





}

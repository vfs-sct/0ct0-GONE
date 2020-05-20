using System.Collections;
using System.Collections.Generic;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/Core/GameManager")]
public class GameFrameworkManager : ScriptableObject
{   
    [SerializeField] 
    private ModuleManager LinkedModuleManager = null;
    public ModuleManager moduleManager{get =>LinkedModuleManager;}

    [Header("Game State System")]


    [SerializeField] private List<GameStateData> GameStates = new List<GameStateData>();
    private List<GameState> UpdatingGameStates = new  List<GameState>();

    Dictionary<string,GameState> StateSceneLinkDict = new Dictionary<string, GameState>();

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

        public bool ShouldCheckCondition;
        public bool IsLinkedToScene;
        public string LinkedScene;
        public GameState State;

        public GameStateData(bool E,bool CC, bool L, string LS, GameState GM)
        {
            Enabled = E;
            ShouldCheckCondition = CC;
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

    public void LoadSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName,LoadSceneMode.Single);
    }



    
    void GameStateUpdate()
    {
        if (!Application.isPlaying) return;
        CheckStateConditions(); //check game state conditions
        if (ActiveState != null && ActiveState.CanTick) 
        {
            ActiveState.OnUpdate();
        }
    }

    void CheckStateConditions()
    {
        foreach (var State in UpdatingGameStates)
        {
            if (State.ConditionCheck(this))
            {
                ChangeGameState(State);
                break;
            }
        }
    }


    public void ChangeGameState(GameState _GameState)
    {
        if (ActiveState != null)
        {
            Debug.Log(ActiveState);
            ActiveState.OnDeactivate(_GameState);
            GameState LastState = ActiveState;
            ActiveState = _GameState;
            ActiveState.OnActivate(LastState);
        }
            ActiveState = _GameState;
            //ActiveState.OnActivate(null);
            return;
        
    }
   

    private void GameStatesInit()
    {
        UpdatingGameStates.Clear(); //clear the old cached gamestates
        StateSceneLinkDict.Clear(); //clear scene-gamestate dictionary
        //if (GameStates.Count == 0) return;//exit out if gamestates are empty
        foreach (var StateData in GameStates)
        {
            if (StateData.State != null)
            {
                Debug.Log("Initializing "+ StateData.State + ":\n");
                StateData.State.Initalize();
                if (StateData.IsLinkedToScene)
                {
                    StateSceneLinkDict.Add(StateData.LinkedScene,StateData.State);
                }
                if (StateData.ShouldCheckCondition) UpdatingGameStates.Add(StateData.State);
            }
        }
    }


    private void MainLoopInit()
    {
        PlayerLoopSystem unityMainLoop = PlayerLoop.GetDefaultPlayerLoop();
        PlayerLoopSystem[] unityCoreSubSystems = unityMainLoop.subSystemList;
        PlayerLoopSystem[] unityCoreUpdate = unityCoreSubSystems[5].subSystemList;
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
        newCoreUpdate[5] = unityCoreUpdate[3];

        unityCoreSubSystems[5].subSystemList = newCoreUpdate;

        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = unityCoreSubSystems;
        PlayerLoop.SetPlayerLoop(systemRoot);
    }

    private void OnEnable()
    {
        //prevent timescale editor bug
        UnPause();
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

        
        if (StateSceneLinkDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            ChangeGameState(StateSceneLinkDict[SceneManager.GetActiveScene().name]);
        }
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

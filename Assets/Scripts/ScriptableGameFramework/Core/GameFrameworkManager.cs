//Copyright Jesse Rougeau, 2020 ©


using System.Collections;
using System.Collections.Generic;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;
using System.Text;
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

    private GameState _ActiveState;
    public GameState ActiveGameState{get => _ActiveState;}
    private Scene ActiveScene;



    public bool isPaused{get => Time.timeScale == 0;}

    delegate void GameManagerDelegate(GameFrameworkManager GameManager);
    private GameManagerDelegate OnExitGame;
    private GameManagerDelegate OnPauseGame;
    private GameManagerDelegate OnResumeGame;


    [System.Serializable]
    struct GameStateData //struct for editing game state properties in the editor
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

    private void DummyGMDelegate(GameFrameworkManager GameManager) //dummy delegate to avoid doing a nullcheck
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        moduleManager.StopTicking = true;
        if (OnPauseGame != null) OnPauseGame(this);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        moduleManager.StopTicking = false;
        if (OnPauseGame != null) OnResumeGame(this);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName,LoadSceneMode.Single);
    }



    
    void GameStateUpdate()
    {
        if (!Application.isPlaying) return;
        CheckStateConditions(); //check game state conditions
        if (_ActiveState != null && _ActiveState.CanTick) 
        {
            _ActiveState.OnUpdate();
        }
    }

    //check the conditions of all the gamestates
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

    //force a game state change
    public void ChangeGameState(GameState _GameState)
    {
        if (_ActiveState != null)
        {
            _ActiveState.OnDeactivate(_GameState);
            GameState LastState = _ActiveState;
            _ActiveState = _GameState;
            _ActiveState.OnActivate(LastState);
        }
            _ActiveState = _GameState;
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

    //initialize the main player loop
    private void MainLoopInit()
    {
        PrintPlayerLoop(); //debugging info on unity player loop

        //get the current update subsystem from the player loop
        PlayerLoopSystem unityMainLoop = PlayerLoop.GetCurrentPlayerLoop(); 
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
        //create our new subsystem and replace the old one
        PlayerLoopSystem[] newCoreUpdate = new PlayerLoopSystem[6];
        newCoreUpdate[0] = unityCoreUpdate[0]; 
        newCoreUpdate[1] = ScriptModuleUpdate;
        newCoreUpdate[2] = gameStateUpdate;
        newCoreUpdate[3] = unityCoreUpdate[1];
        newCoreUpdate[4] = unityCoreUpdate[2];
        newCoreUpdate[5] = unityCoreUpdate[3];

        unityCoreSubSystems[4].subSystemList = newCoreUpdate;

        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = unityCoreSubSystems;
        PlayerLoop.SetPlayerLoop(systemRoot); //override the old player loop with our new one
    }

    private void OnEnable()
    {
        //prevent timescale editor bug
        UnPause();
        //Application.targetFrameRate = 60;
        Initalize();
    }

    private void Awake()
    {
        if (Application.isEditor) return;
        Initalize();
    }


    //initialize core gameframework delegates
    private void CoreEventsInit()
    {
        OnExitGame += DummyGMDelegate;
        OnPauseGame += DummyGMDelegate;
        OnResumeGame += DummyGMDelegate;
    }

    //main initialization
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

    //wrapper for scene loading
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded");
        
        moduleManager.ModuleStartOnLoad();

        
        if (StateSceneLinkDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            ChangeGameState(StateSceneLinkDict[SceneManager.GetActiveScene().name]);
        }
    }


    //debug print code based off of https://medium.com/@thebeardphantom/unity-2018-and-playerloop-5c46a12a677
    public static void PrintPlayerLoop()
    {
    var def = PlayerLoop.GetCurrentPlayerLoop();
    var sb = new StringBuilder();
    RecursivePlayerLoopPrint(def, sb, 0);
    Debug.Log(sb.ToString());    
    }

    //debug print code based off of https://medium.com/@thebeardphantom/unity-2018-and-playerloop-5c46a12a677
    public static void PrintDefaultPlayerLoop()
    {
    var def = PlayerLoop.GetDefaultPlayerLoop();
    var sb = new StringBuilder();
    RecursivePlayerLoopPrint(def, sb, 0);
    Debug.Log(sb.ToString());    
    }
    
    //debug print code based off of https://medium.com/@thebeardphantom/unity-2018-and-playerloop-5c46a12a677
    private static void RecursivePlayerLoopPrint(PlayerLoopSystem def, StringBuilder sb, int depth)
    {
    if (depth == 0)
    {
        sb.AppendLine("ROOT NODE");
    }
    else if (def.type != null)
    {
        for (int i = 0; i < depth; i++)
        {
            sb.Append("\t");
        }
        sb.AppendLine(def.type.Name);
    }
    if (def.subSystemList != null)
    {
        depth++;
        foreach (var s in def.subSystemList)
        {
            RecursivePlayerLoopPrint(s, sb, depth);
        }
        depth--;
    }
    }


    public void Start()
    {

    }

    //exit the game
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

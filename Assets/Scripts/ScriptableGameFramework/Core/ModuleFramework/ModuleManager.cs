//Copyright Jesse Rougeau, 2020 ©

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace ScriptableGameFramework
{


[CreateAssetMenu(menuName = "GameFramework/Core/ModuleManager")]
public class ModuleManager : ScriptableObject
{
    [SerializeField] 
    private GameFrameworkManager _GameManager;

    delegate void TickModuleUpdateDelegate();
    delegate void OnSceneLoadedDelegate();

    [SerializeField]
    private List<Module> ActiveModules = new List<Module>();
    private Dictionary<System.Type,Module> moduleList = new Dictionary<System.Type,Module>();
    public Dictionary<System.Type,Module> List {get => moduleList;}
    
    public bool StopTicking = true;//public because it's 10pm and I'm lazy


    private List<TickModuleUpdateDelegate> tickingModules = new List<TickModuleUpdateDelegate>();
    private OnSceneLoadedDelegate sceneLoadedDelegate;


    private void emptyDelegate() //dummy delegate to avoid having to do a nullcheck
    {
        
    }


    //module update tick, this uses the playerloop to update NOT mono updates or fixedupdates
    public void ModuleUpdateTick()
    {
        if (!Application.isPlaying) return;
        if (StopTicking) return;
        foreach (TickModuleUpdateDelegate ModuleTickFunc in tickingModules)
        {
            ModuleTickFunc();
        }
    }

    public void ModuleStartOnLoad()
    {
        sceneLoadedDelegate();
    }

    public void Initialize()
    {
        Debug.Log("-= Loading Game framework modules =-\n");
        LoadModules();
        Debug.Log("-Module initalization Complete-\n");
    }


    //build the modulelist and initialize each module
    public void LoadModules()
    {
        moduleList.Clear();

        Debug.Log("=Building Module List=\n");
        string debugModuleList = "";
        
        foreach (var module in ActiveModules)
        {
            moduleList.Add(module.GetType(),module);
            debugModuleList = debugModuleList + "" + module+",";
        }
        Debug.Log(debugModuleList+"\n");


        Debug.Log("-Module List built successfully-\n");

        Debug.Log("--==Initializing Modules==-\n");
        foreach (Module module in ActiveModules)
        {
            Debug.Log("Loading: " + module.GetType()+"\n");
            module.Initialize();
            if (module.RunUpdate)
            {
                tickingModules.Add(module.Update);
                Debug.Log("" + module + " Added to update thread\n");
            }
            if (module.StartOnSceneLoad)
            {
                sceneLoadedDelegate += module.Start;
            }
            Debug.Log(module.GetType() +" Load Complete\n");
        }
        sceneLoadedDelegate += emptyDelegate;//to prevent null pointer
    }

 public T GetModule<T>() where T : Module
    {
        return (T)moduleList[typeof(T)];
    }
}

    
}
//Copyright Jesse Rougeau, 2020 ©

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ScriptableGameFramework
{

//[CreateAssetMenu(menuName = "Systems/<StateName>/<StateName>State")]
public abstract class GameState : ScriptableObject
{
    [SerializeField] public bool CanTick;
    [SerializeField] public long TickRate;


    [Header("State Registries")]
    [SerializeField] private List<string> DefaultBoolStates;
    [SerializeField] private List<string> DefaultIntStates;
    public Dictionary<string,bool> _BoolStateRegistry = new Dictionary<string, bool>();
    public Dictionary<string,int> _IntStateRegistry = new Dictionary<string, int>();

    public void RegisterBoolState(string name,bool defaultValue = false)
    {
        _BoolStateRegistry[name] = defaultValue;
    }

     public void RegisterIntState(string name,int defaultValue = 0)
    {
        _IntStateRegistry[name] = defaultValue;
    }

    public bool GetBoolState(string name)
    {
        return _BoolStateRegistry[name];
    }

    public int GetIntState(string name)
    {
        return _IntStateRegistry[name];
    }


    public virtual bool ConditionCheck(GameFrameworkManager GameManager,GameState CurrentState)
    {
        return true;
    }

    public virtual bool TransitionCheck(GameFrameworkManager GameManager)
    {
        return true;
    }

    public abstract void OnActivate(GameState LastState);

    public abstract void OnDeactivate(GameState NewState);

    public abstract void Reset();

    public virtual void OnUpdate(){}

    public virtual void OnInitialize(){}

    public void Initialize(){
        _BoolStateRegistry.Clear();
        _IntStateRegistry.Clear();
        foreach (var stateName in DefaultBoolStates)
        {
            
            _BoolStateRegistry.Add(stateName,false);
        }
        foreach (var stateName in DefaultIntStates)
        {
            _IntStateRegistry.Add(stateName,0);
        }
        OnInitialize();
    }
}
}
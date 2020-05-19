using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/Playing")]
public class Playing : GameState
{
    //[SerializeField] private ScriptedUI OptionsUI = null;
    //[SerializeField] private ScriptedUI GameHUDUI = null;
    //[SerializeField] private ScriptedUI PauseUI = null;
    //[SerializeField] private ScriptedUI CodexUI = null;
    //[SerializeField] private ScriptedUI ConfirmationUI = null;

    public override bool ConditionCheck(GameFrameworkManager GameManager)
    {
        return false;
    }

    public override void OnActivate(GameState LastState)
    {
    }

    public override void OnDeactivate(GameState NewState)
    {
    }

    public override void Reset()
    {

    }
}

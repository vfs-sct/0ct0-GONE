using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/GameOver")]
public class GameOverState : GameState
{
    //[SerializeField] private ScriptedUI GameoverUI = null;
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

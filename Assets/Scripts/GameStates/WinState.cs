using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Gamemode/Win State")]
public class WinState : GameState
{
    
    private Player _ActivePlayer = null;
    [SerializeField] private EventModule EventManager;

    public override bool ConditionCheck(GameFrameworkManager GameManager)
    {
        return (GameManager.ActiveGameState.GetType() == typeof(Playing)) && (EventManager.EventListComplete);
    }
    public override void OnActivate(GameState LastState)
    {
        if (LastState.GetType() == typeof(Playing)) //get the player object if the last state was gameplay
        {
            _ActivePlayer = (LastState as Playing).ActivePlayer; //wheeeee casting is fun!
        }
        Debug.Log("You won!");
        _ActivePlayer.Win();
    }

    public override void OnDeactivate(GameState NewState)
    {
        
    }

    public override void Reset()
    {
        
    }
}

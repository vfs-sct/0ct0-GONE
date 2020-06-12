using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/GameOver")]
public class GameOverState : GameState
{
    [SerializeField] private Playing PlayingState;

     [SerializeField] private CommunicationModule RelayController;
    [SerializeField] private Resource FuelResource;

    bool OutOfFuel = false;

    public override bool ConditionCheck(GameFrameworkManager GameManager)
    {     
        if (PlayingState.ActivePlayer == null) return false; //Do not go to gameover if the player is null, Prevents error spam
        return (
            (PlayingState.ActivePlayer.Inventory.GetResource(FuelResource) == 0) //check if the player is out of fuel
            || (!RelayController.InRange) //check if the player is out of range
            );
    }

    public override void OnActivate(GameState LastState)
    {
        PlayingState.ActivePlayer.GameOver();
    }

    public override void OnDeactivate(GameState NewState)
    {
        
    }

    public override void Reset()
    {
    }
}

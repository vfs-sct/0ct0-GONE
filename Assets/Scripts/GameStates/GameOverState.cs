using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/GameOver")]
public class GameOverState : GameState
{
    [SerializeField] private Playing PlayingState;
    [SerializeField] private Resource FuelResource;
    public override bool ConditionCheck(GameFrameworkManager GameManager)
    {
        return (PlayingState.ActivePlayer != null)&&(PlayingState.ActivePlayer.Inventory.GetResource(FuelResource) == 0);
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

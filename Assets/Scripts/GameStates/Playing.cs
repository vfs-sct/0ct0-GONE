using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/Playing")]
public class Playing : GameState
{



    [SerializeField] private Player _ActivePlayer;
    public Player ActivePlayer{get=>_ActivePlayer;}

    public void RegisterPlayer(Player newPlayer)
    {
        _ActivePlayer = newPlayer;
    }

    public override void OnActivate(GameState LastState)
    {
        Debug.Log("Starting Gameplay");
    }

    public override void OnDeactivate(GameState NewState)
    {

    }

    public override void Reset()
    {

    }
}

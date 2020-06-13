using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/Playing")]
public class Playing : GameState
{



    private Player _ActivePlayer;
    [SerializeField] private CommunicationModule RelayController;
    public Player ActivePlayer{get=>_ActivePlayer;}

    public void RegisterPlayer(Player newPlayer)
    {
        _ActivePlayer = newPlayer;
    }

    public override void OnActivate(GameState LastState)
    {
        Debug.Log("Starting Gameplay");
        RelayController.SetPlayer(_ActivePlayer.gameObject);
        RelayController.Start();
    }

    public override void OnDeactivate(GameState NewState)
    {
        RelayController.Reset();
    }

    public override void Reset()
    {
        _ActivePlayer = null;
    }
}

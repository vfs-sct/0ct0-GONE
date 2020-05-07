using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/Playing")]
public class Playing : GameState
{
    public override bool ConditionCheck(GameFrameworkManager GameManager)
    {
        throw new System.NotImplementedException();
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

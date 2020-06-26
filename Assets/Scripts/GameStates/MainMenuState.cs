using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Gamemode/MainMenu")]
public class MainMenuState : GameState
{
    //[SerializeField] private UIModule UIManager;
    //[SerializeField] private ScriptedUI MainMenuUI = null;
    //[SerializeField] private ScriptedUI OptionsUI = null;
    //[SerializeField] private ScriptedUI CreditsUI = null;
    

    public override bool ConditionCheck(GameFrameworkManager GameManager,GameState CurrentState)
    {
        return true;
    }

    public override void OnActivate(GameState LastState)
    {
        Debug.Log("Main Menu");
        //Debug.Log("I'm alive");
        //UIManager.CreateInstance(MainMenuUI);
        //UIManager.Show(MainMenuUI, 0);

        //UIManager.CreateInstance(CreditsUI);
        //UIManager.Hide(CreditsUI, 0);

        //UIManager.CreateInstance(OptionsUI);
        //UIManager.Hide(OptionsUI, 0);
    }

    public override void OnDeactivate(GameState NewState)
    {
        
    }

    public override void Reset()
    {
    }
}

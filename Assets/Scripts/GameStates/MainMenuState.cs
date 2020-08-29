using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableGameFramework;

[CreateAssetMenu(menuName = "Core/Gamemode/MainMenu")]
public class MainMenuState : GameState
{
    //[SerializeField] private UIModule UIManager;
    //[SerializeField] private ScriptedUI MainMenuUI = null;
    //[SerializeField] private ScriptedUI OptionsUI = null;
    //[SerializeField] private ScriptedUI CreditsUI = null;
    [SerializeField] private SaveFile saveFile = null;

    private bool _completedGame = false;

    public bool IsGameCompleted()
    {
        return _completedGame;
    }

    public void SetGameCompleted(bool isComplete)
    {
        if(isComplete == true)
        {
            //game is finished so wipe save
            saveFile.Reset();
        }
        _completedGame = isComplete;
    }


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
        _completedGame = false;
    }
}

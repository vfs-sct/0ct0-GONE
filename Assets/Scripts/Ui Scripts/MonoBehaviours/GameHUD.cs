//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] GameObject PausePrefab = null;
    [SerializeField] GameObject GameoverPrefab = null;
    [SerializeField] GameObject CraftingPrefab = null;

    [Header("Tools")]
    [SerializeField] ToolController playerTools = null;
    [SerializeField] HorizontalLayoutGroup contentGroup = null;
    [SerializeField] GameObject defaultToolBox = null;

    [Header("Do not touch")]
    public Player player = null;
    public GameObject selectedToolText = null;
    public TextMeshProUGUI objectiveText = null;

    private void Start()
    {
        if(player == null)
        {
            player = UIRoot.GetPlayer();
        }
        playerTools = player.GetComponent<ToolController>();
        PopulateToolbar();
    }

    public void PopulateToolbar()
    {
        int hotkey = 1;
        Debug.Log(playerTools.GetEquiptTools().Count);
        foreach (var tool in playerTools.GetEquiptTools())
        {
            var newTool = CreateToolBox();
            newTool.transform.SetParent(contentGroup.transform);
            var getObject = newTool.GetComponent<GetObjects>();
            getObject.GetToolText().SetText(tool.displayName);
            getObject.GetHotkeyText().SetText("[ " + hotkey.ToString() + " ]");
            getObject.GetToolIcon().sprite = tool.toolIcon;
            hotkey++;
        }
    }

    public GameObject CreateToolBox()
    {
        return Instantiate(defaultToolBox);
    }

    public void SetObjectiveText(string updateObjective)
    {
        objectiveText.SetText(updateObjective);
    }

    public void OnEsc(InputValue value)
    {
        if (!GameManager.isPaused)
        {
            PausePrefab.SetActive(true);
            GameManager.Pause();
            Debug.Log("Paused");
        }
    }

    public void GameOver()
    {
        if (!GameManager.isPaused)
        {
            GameManager.Pause();
            Debug.Log("Paused");
        }
        GameoverPrefab.SetActive(true);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

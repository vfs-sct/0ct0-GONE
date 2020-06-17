//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using System.Collections.Generic;
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
    [SerializeField] TextMeshProUGUI objectDistance = null;

    [Header("Tools")]
    [SerializeField] ToolController playerTools = null;
    [SerializeField] GameObject gooGlueBar = null;
    [SerializeField] HorizontalLayoutGroup contentGroup = null;
    [SerializeField] GameObject defaultToolBox = null;
    [SerializeField] Color enabledBGColour;
    [SerializeField] Color disabledBGColour;
    [SerializeField] Color enabledTextColour;
    [SerializeField] Color disabledTextColour;

    [Header("Do not touch")]
    public Player player = null;
    public GameObject selectedToolText = null;
    public TextMeshProUGUI objectiveText = null;

    private List<GameObject> toolList = new List<GameObject>();
    private int currentTool = -1;
    private string prevTool = null;

    private void Update()
    {
        if(player.mouseCollision == null)
        {
            objectDistance.SetText("");
        }
        else
        {
            objectDistance.SetText(player.collisionDistance.ToString() + "m");
        }
    }

    private void Start()
    {
        if(player == null)
        {
            player = UIRoot.GetPlayer();
        }
        playerTools = player.GetComponent<ToolController>();
        PopulateToolbar();
    }

    private void PopulateToolbar()
    {
        int hotkey = 1;
        //Debug.Log(playerTools.GetEquiptTools().Count);
        foreach (var tool in playerTools.GetEquiptTools())
        {
            var newTool = CreateToolBox();
            newTool.transform.SetParent(contentGroup.transform);
            var getObject = newTool.GetComponent<GetObjects>();
            getObject.GetToolText().SetText(tool.displayName);
            getObject.GetHotkeyText().SetText("[ " + hotkey.ToString() + " ]");
            getObject.GetToolIcon().sprite = tool.toolIcon;

            toolList.Add(newTool);
            hotkey++;
        }
    }

    public void SwitchActiveTool(int newTool)
    {
        if(currentTool != -1)
        {
            var lastToolObj = toolList[currentTool].GetComponent<GetObjects>();
            lastToolObj.GetToolIcon().color = enabledBGColour;
            lastToolObj.GetToolText().color = enabledTextColour;
            lastToolObj.GetHotkeyText().color = enabledTextColour;
            lastToolObj.GetToolText().SetText(prevTool);
        }

        var newToolObj = toolList[newTool].GetComponent<GetObjects>();
        newToolObj.GetToolIcon().color = disabledBGColour;
        newToolObj.GetToolText().color = disabledTextColour;
        newToolObj.GetHotkeyText().color = disabledTextColour;
        prevTool = newToolObj.GetToolText().text;
        newToolObj.GetToolText().SetText("Active");

        currentTool = newTool;
    }

    public void NoToolSelected()
    {
        if (currentTool != -1)
        {
            var lastToolObj = toolList[currentTool].GetComponent<GetObjects>();
            lastToolObj.GetToolIcon().color = enabledBGColour;
            lastToolObj.GetToolText().color = enabledTextColour;
            lastToolObj.GetHotkeyText().color = enabledTextColour;
            lastToolObj.GetToolText().SetText(prevTool);
        }
        currentTool = -1;
    }

    private GameObject CreateToolBox()
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

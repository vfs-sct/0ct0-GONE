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
    [SerializeField] GameObject InventoryPrefab = null;
    [SerializeField] public GameObject GasCloudAlertPrefab = null;
    [SerializeField] TextMeshProUGUI objectDistance = null;
    [SerializeField] public ObjectivePopUp objectivePopUp = null;

    [Header("Tools")]
    [SerializeField] ToolController playerTools = null;
    [SerializeField] GameObject gooGlueBar = null;
    [SerializeField] HorizontalLayoutGroup contentGroup = null;
    [SerializeField] GameObject defaultToolBox = null;
    [SerializeField] Sprite enabledSprite = null;
    [SerializeField] Sprite disabledSprite = null;
    [SerializeField] Color enabledBGColour;
    [SerializeField] Color disabledBGColour;
    [SerializeField] Color enabledTextColour;
    [SerializeField] Color disabledTextColour;

    [Header("Grabbed by Tool Controller")]
    [SerializeField] public TextMeshProUGUI equippedToolText = null;

    [Header("Do not touch")]
    public Player player = null;
    public GameObject selectedToolText = null;
    public TextMeshProUGUI objectiveText = null;

    private List<GameObject> toolList = new List<GameObject>();
    //keep track of which tool in the list is goo glue since it needs an additional bar
    private int gooGlueIndex;
    private int currentTool = -1;
    private string prevTool = null;

    private void Update()
    {
        //used for showing how far away moused over objects are
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
        gooGlueBar.SetActive(false);
        if (player == null)
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
            if(tool.GetType() == typeof(RepairTool))
            {
                gooGlueIndex = hotkey - 1;
            }
            var newTool = CreateToolBox();
            newTool.transform.SetParent(contentGroup.transform);
            var getObject = newTool.GetComponent<GetObjects>();
            getObject.GetToolText().SetText(tool.displayName);
            getObject.GetHotkeyText().SetText("[ " + hotkey.ToString() + " ]");
            getObject.GetButtonImage().sprite = enabledSprite;
            //getObject.GetToolIcon().sprite = tool.toolIcon;
            getObject.GetToolIcon().color = disabledTextColour;

            toolList.Add(newTool);
            hotkey++;
        }
    }

    public void OnInventoryHotkey(InputValue value)
    {
        if (!GameManager.isPaused)
        {
            InventoryPrefab.SetActive(true);
            GameManager.Pause();
        }
    }

    public void SwitchActiveTool(int newTool)
    {
        //Debug.Log("New Tool Index: " + newTool);
        //Debug.Log("Goo Glue Index: " + gooGlueIndex);
        if(currentTool != -1)
        {
            if(currentTool == gooGlueIndex)
            {
                gooGlueBar.SetActive(false);
            }
            var lastToolObj = toolList[currentTool].GetComponent<GetObjects>();
            lastToolObj.GetButtonImage().color = enabledBGColour;
            lastToolObj.GetButtonImage().sprite = enabledSprite;
            lastToolObj.GetToolText().color = enabledTextColour;
            lastToolObj.GetHotkeyText().color = enabledTextColour;
            lastToolObj.GetToolIcon().color = enabledTextColour;
            lastToolObj.GetToolText().SetText(prevTool);
        }

        //turn on the goo glue fuel bar if the new tool is the repair tool
        //if (newTool == gooGlueIndex)
        //{
        //    gooGlueBar.SetActive(true);
        //}

        var newToolObj = toolList[newTool].GetComponent<GetObjects>();
        newToolObj.GetButtonImage().color = disabledBGColour;
        newToolObj.GetButtonImage().sprite = disabledSprite;
        newToolObj.GetToolText().color = disabledTextColour;
        newToolObj.GetHotkeyText().color = disabledTextColour;
        newToolObj.GetToolIcon().color = disabledTextColour;
        prevTool = newToolObj.GetToolText().text;
        newToolObj.GetToolText().SetText("Active");

        currentTool = newTool;
    }

    public void NoToolSelected()
    {
        if (currentTool != -1)
        {
            var lastToolObj = toolList[currentTool].GetComponent<GetObjects>();
            lastToolObj.GetButtonImage().color = enabledBGColour;
            lastToolObj.GetToolText().color = enabledTextColour;
            lastToolObj.GetHotkeyText().color = enabledTextColour;
            lastToolObj.GetToolText().SetText(prevTool);
        }
        gooGlueBar.SetActive(false);
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

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
    //[SerializeField] public UpgradeBar healthUpgrade = null;
    [SerializeField] public UpgradeBar fuelUpgrade = null;
    [SerializeField] public ObjectivePanel objectivePanel = null;

    [Header("Tool Progress Bar")]
    [SerializeField] public Image progressBarBG = null;
    [SerializeField] public Image progressBarFill = null;

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

    [SerializeField] private Color ToolInRangeColor;

    [SerializeField] private Color ToolOutOfRangeColor;

    [Header("Grabbed by Tool Controller")]
    [SerializeField] public TextMeshProUGUI equippedToolText = null;
    [SerializeField] public RawImage equippedToolIcon = null;

    [Header("Do not touch")]
    public Player player = null;
    public GameObject selectedToolText = null;
    public List<TextMeshProUGUI> objectiveText = new List<TextMeshProUGUI>();

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
            if (player.TargetedObject == null)
            {
                objectDistance.SetText("");
            }
            else
            {
                Salvagable TargetSalvage = player.TargetedObject.GetComponentInChildren<Salvagable>();
                if (TargetSalvage != null)
                {
                    objectDistance.SetText(TargetSalvage.SalvageItem.Name + " | " + player.collisionDistance.ToString() + "m");
                }
                else
                {
                    objectDistance.SetText(player.collisionDistance.ToString() + "m");
                }
            }
           
            if (playerTools.CurrentTool != null)
            {
                if ( playerTools.CurrentTool.ToolRange > 0)
                {
                    if (playerTools.CurrentTool.ToolRange >= player.collisionDistance)
                    {
                        objectDistance.color = Color.green;
                    }
                    else 
                    {
                        objectDistance.color = Color.red;
                    }
                    return;
                }
            }
            objectDistance.color = Color.white;
            
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
            if (tool.GetType() == typeof(RepairTool))
            {
                gooGlueIndex = hotkey - 1;
            }
            var newTool = CreateToolBox();
            newTool.transform.SetParent(contentGroup.transform);

            var getObject = newTool.GetComponent<GetObjects>();
            getObject.GetToolText().SetText(tool.displayName);
            getObject.GetHotkeyText().SetText("[ " + hotkey.ToString() + " ]");
            getObject.GetButtonImage().sprite = enabledSprite;
            getObject.GetToolIcon().sprite = tool.toolIcon;
            getObject.GetToolIcon().color = enabledTextColour;

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

        //make the current tool look selectable again
        if(currentTool != -1)
        {
            if (currentTool == gooGlueIndex)
            {
                gooGlueBar.SetActive(false);
            }
            var lastToolObj = toolList[currentTool].GetComponent<GetObjects>();
            lastToolObj.GetButtonImage().color = enabledBGColour;
            lastToolObj.GetButtonImage().sprite = enabledSprite;
            lastToolObj.GetToolText().color = enabledTextColour;
            lastToolObj.GetHotkeyText().color = enabledTextColour;
            lastToolObj.GetToolIcon().color = enabledTextColour;
            lastToolObj.GetToolText().SetText(playerTools.GetEquiptTools()[currentTool].displayName);
        }

        //turn on the goo glue fuel bar if the new tool is the repair tool
        if (newTool == gooGlueIndex)
        {
            gooGlueBar.SetActive(true);
        }

        //make the new tool look unselectable
        var newToolObj = toolList[newTool].GetComponent<GetObjects>();
        newToolObj.GetButtonImage().color = disabledBGColour;
        newToolObj.GetButtonImage().sprite = disabledSprite;
        newToolObj.GetToolText().color = disabledTextColour;
        newToolObj.GetHotkeyText().color = disabledTextColour;
        newToolObj.GetToolIcon().color = disabledTextColour;
        newToolObj.GetToolText().SetText("Active");

        currentTool = newTool;
    }

    public void NoToolSelected()
    {
        if (currentTool != -1)
        {
            var lastToolObj = toolList[currentTool].GetComponent<GetObjects>();
            lastToolObj.GetButtonImage().color = enabledBGColour;
            lastToolObj.GetButtonImage().sprite = enabledSprite;
            lastToolObj.GetToolText().color = enabledTextColour;
            lastToolObj.GetHotkeyText().color = enabledTextColour;
            lastToolObj.GetToolIcon().color = enabledTextColour;
            lastToolObj.GetToolText().SetText(playerTools.GetEquiptTools()[currentTool].displayName);
        }
        gooGlueBar.SetActive(false);
        currentTool = -1;
    }

    private GameObject CreateToolBox()
    {
        return Instantiate(defaultToolBox);
    }

    public void SetObjectiveText(string updateObjective, int index)
    {
        objectiveText[index].SetText(updateObjective);
    }

    public void OnEsc(InputValue value)
    {
        if (!GameManager.isPaused)
        {
            PausePrefab.SetActive(true);
            GameManager.Pause();
            //Debug.Log("Paused");
        }
    }

    public void GameOver()
    {
        if (!GameManager.isPaused)
        {
            GameManager.Pause();
            //Debug.Log("Paused");
        }
        AkSoundEngine.PostEvent("Death", gameObject);
        GameoverPrefab.SetActive(true);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

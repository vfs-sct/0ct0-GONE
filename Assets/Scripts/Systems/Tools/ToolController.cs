using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolController : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager = null;
    [SerializeField] private UIModule UIModule = null;
    [SerializeField] private List<Tool> EquiptTools = new List<Tool>();
    
    private GameHUD gameHUD = null;
    private RawImage toolIconLarge = null;
    private TextMeshProUGUI toolText = null;

    private Tool _CurrentTool = null;
    public Tool CurrentTool{get=>_CurrentTool;}
    private bool CurrentToolIsActive = false;

    private Player _LinkedPlayer;
    public Player LinkedPlayer{get=>_LinkedPlayer;}

    [SerializeField] private InventoryController _PlayerInventory;
    public InventoryController PlayerInventory{get=>_PlayerInventory;}


    public GameObject Target{get=>_LinkedPlayer.TargetedObject;}

    public List<Tool> GetEquiptTools()
    {
        return EquiptTools;
    }
    
    public void SwitchTool(int ToolIndex)
    {
        //EVAN
        //sound for switching tools - could have kind of a mechanical clunk

        Debug.Assert(ToolIndex < EquiptTools.Count && ToolIndex >= 0);
        if (CurrentToolIsActive) return; //dont switch tools if the current tool is in use
        if (_CurrentTool != null)  _CurrentTool.Deselect(this);
        _CurrentTool = EquiptTools[ToolIndex];
        _CurrentTool.Select(this);

        //make text box correct size to accomodate image being enabled
        toolText.SetText(_CurrentTool.displayName);
        var textRect = toolText.GetComponent<RectTransform>();
        var sizeDelta = textRect.sizeDelta;
        sizeDelta.y = 30;
        sizeDelta.x = 90;
        toolText.fontSizeMax = 25;
        textRect.sizeDelta = sizeDelta;

        //enable image
        toolIconLarge.gameObject.SetActive(true);
        toolIconLarge.texture = _CurrentTool.toolIconLarge;
        //update the tools in the toolbar to reflect which is selected
        gameHUD.SwitchActiveTool(ToolIndex);
    }

    public void DeselectTool()
    {
        //EVAN
        //sound for deselecting a tool. deselecting tools may get cut, so probably lower priority
        Debug.Log("DeselectTool");
        if (CurrentToolIsActive) 
        {
            DeactiveTool_Internal();
        }
        if (_CurrentTool != null) _CurrentTool.Deselect(this);
        _CurrentTool = null;

        //turn off tool icon since no tool is selected
        toolIconLarge.gameObject.SetActive(false);

        //expand text size since image is turned off
        toolText.SetText("No Tool Selected");
        var textRect = toolText.GetComponent<RectTransform>();
        var sizeDelta = textRect.sizeDelta;
        sizeDelta.y = 90;
        toolText.fontSizeMax = 72;
        textRect.sizeDelta = sizeDelta;

        //make all tools in the toolbar appear enabled
        gameHUD.NoToolSelected();
    }

    public void ActivateTool()
    {
        if (!GameManager.isPaused)
        {
            if (_CurrentTool == null | CurrentToolIsActive) return;
            CurrentToolIsActive = _CurrentTool.Activate(this, _LinkedPlayer.TargetedObject);
        }
    }

    public void DeactivateTool()
    {
        if (!GameManager.isPaused)
        {
            if (_CurrentTool == null) return;
            CurrentToolIsActive = !_CurrentTool.Deactivate(this, _LinkedPlayer.TargetedObject);
        }
        
    }
    private void DeactiveTool_Internal()
    {
        CurrentToolIsActive = false;
        //gameHUD.NoToolSelected();
        if (_CurrentTool == null) return;
        _CurrentTool.Deactivate(this,_LinkedPlayer.TargetedObject);
        
    }


    void Start()
    {
        _LinkedPlayer = gameObject.GetComponent<Player>();
        gameHUD = UIModule.UIRoot.GetScreen<GameHUD>();
        toolIconLarge = gameHUD.equippedToolIcon;
        toolIconLarge.gameObject.SetActive(false);
        toolText = gameHUD.equippedToolText;
        Debug.Assert(toolText != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentToolIsActive)
        {
            if (_CurrentTool == null || !_CurrentTool.WhileActive(this,_LinkedPlayer.TargetedObject))
            {
                DeactiveTool_Internal();
                return;
            }
            _CurrentTool.WhileActive(this,_LinkedPlayer.TargetedObject);
        }
    }
}

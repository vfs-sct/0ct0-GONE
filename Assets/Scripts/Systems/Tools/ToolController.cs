using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolController : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager = null;
    [SerializeField] private UIModule UIModule = null;
    [SerializeField] private List<Tool> EquiptTools = new List<Tool>();
    
    private GameHUD gameHUD = null;
    private TextMeshProUGUI toolText = null;

    private Tool CurrentTool = null;

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
        Debug.Assert(ToolIndex < EquiptTools.Count && ToolIndex >= 0);
        if (CurrentToolIsActive) return; //dont switch tools if the current tool is in use
        if (CurrentTool != null)  CurrentTool.Deselect(this);
        CurrentTool = EquiptTools[ToolIndex];
        CurrentTool.Select(this);
        toolText.SetText(CurrentTool.displayName);
        //update the tools in the toolbar to reflect which is selected
        gameHUD.SwitchActiveTool(ToolIndex);
    }

    public void DeselectTool()
    {
        Debug.Log("DeselectTool");
        if (CurrentToolIsActive) 
        {
            DeactiveTool_Internal();
        }
        if (CurrentTool != null) CurrentTool.Deselect(this);
        CurrentTool = null;
        toolText.SetText("No Tool Selected");
        //make all tools in the toolbar appear enabled
        gameHUD.NoToolSelected();
    }

    public void ActivateTool()
    {
        if (!GameManager.isPaused)
        {
            if (CurrentTool == null | CurrentToolIsActive) return;
            CurrentToolIsActive = CurrentTool.Activate(this, _LinkedPlayer.TargetedObject);
        }
    }

    public void DeactivateTool()
    {
        if (!GameManager.isPaused)
        {
            if (CurrentTool == null) return;
            CurrentToolIsActive = !CurrentTool.Deactivate(this, _LinkedPlayer.TargetedObject);
        }
        
    }
    private void DeactiveTool_Internal()
    {
        CurrentToolIsActive = false;
        gameHUD.NoToolSelected();
        if (CurrentTool == null) return;
        CurrentTool.Deactivate(this,_LinkedPlayer.TargetedObject);
        
    }


    void Start()
    {
        _LinkedPlayer = gameObject.GetComponent<Player>();
        gameHUD = UIModule.UIRoot.GetScreen<GameHUD>();
        toolText = gameHUD.equippedToolText;
        Debug.Assert(toolText != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentToolIsActive)
        {
            if (CurrentTool == null || !CurrentTool.WhileActive(this,_LinkedPlayer.TargetedObject))
            {
                DeactiveTool_Internal();
                return;
            }
            CurrentTool.WhileActive(this,_LinkedPlayer.TargetedObject);
        }
    }
}

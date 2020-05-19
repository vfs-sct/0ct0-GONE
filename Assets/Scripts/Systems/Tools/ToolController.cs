using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{

    [SerializeField] private List<Tool> EquiptTools = new List<Tool>();

    private Tool CurrentTool = null;

    private bool CurrentToolIsActive = false;

    public void SwitchTool(int ToolIndex)
    {
        Debug.Assert(ToolIndex < EquiptTools.Count && ToolIndex >= 0);
        if (CurrentToolIsActive) return; //dont switch tools if the current tool is in use
        if (CurrentTool != null)  CurrentTool.Deselect(this);
        CurrentTool = EquiptTools[ToolIndex];
        CurrentTool.Select(this);
    }

    public void DeselectTool()
    {
        if (CurrentToolIsActive) 
        {
            DeactiveTool_Internal();
        }
        if (CurrentTool != null) CurrentTool.Deselect(this);
        CurrentTool = null;
    }

    public void ActivateTool()
    {
        if (CurrentTool == null | CurrentToolIsActive) return;
        CurrentToolIsActive = CurrentTool.Activate(this,null);
    }

    public void DeactivateTool()
    {
        if (CurrentTool == null) return;
        CurrentToolIsActive = !CurrentTool.Deactivate(this,null);
        
    }
    private void DeactiveTool_Internal()
    {
        if (CurrentTool == null) return;
        CurrentTool.Deactivate(this,null);
        CurrentToolIsActive = false;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (CurrentToolIsActive)
        //{
        //    if (!CurrentTool.WhileActive(this))
        //    {
        //        DeactiveTool_Internal();
        //        return;
        //    }
        //    CurrentTool.WhileActive(this);
        //}
    }
}

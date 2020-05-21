using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{

    [SerializeField] private List<Tool> EquiptTools = new List<Tool>();

    private Tool CurrentTool = null;

    private bool CurrentToolIsActive = false;

    private GameObject _Target;
    public GameObject Target{get=>_Target;}

    public void SetTarget(GameObject NewTarget)
    {
        _Target = NewTarget;
    }

    public void ClearTarget()
    {
        SetTarget(null);
    }
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
        CurrentToolIsActive = CurrentTool.Activate(this,_Target);
    }

    public void DeactivateTool()
    {
        if (CurrentTool == null) return;
        CurrentToolIsActive = !CurrentTool.Deactivate(this,_Target);
        
    }
    private void DeactiveTool_Internal()
    {
        if (CurrentTool == null) return;
        CurrentTool.Deactivate(this,_Target);
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

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
        CurrentTool = EquiptTools[ToolIndex];
    }

    public void ActivateTool()
    {
        if (CurrentTool == null) return;
        CurrentToolIsActive = true;
        CurrentTool.Activate(this);
    }

    public void DeactivateTool()
    {
        if (CurrentTool == null) return;
        CurrentToolIsActive = false;
        CurrentTool.Deactivate(this);
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentToolIsActive)
        {
            CurrentTool.WhileActive(this);
        }
    }
}

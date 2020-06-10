using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolController : MonoBehaviour
{

    [SerializeField] private List<Tool> EquiptTools = new List<Tool>();
    [SerializeField] public TextMeshProUGUI toolText = null;
    


    private Tool CurrentTool = null;

    private bool CurrentToolIsActive = false;

    private Player _LinkedPlayer;
    public Player LinkedPlayer{get=>_LinkedPlayer;}
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
        if (toolText != null)
        {
            toolText.SetText(CurrentTool.displayName);
        }
        else
        {
            Debug.Log("A reference to the tool text in UI has not been hooked up on the player.");
        }
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
        CurrentToolIsActive = false;
        if (CurrentTool == null) return;
        CurrentTool.Deactivate(this,_Target);
        
    }


    void Start()
    {
        _LinkedPlayer = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentToolIsActive)
        {
            if (!CurrentTool.WhileActive(this,_Target))
            {
                DeactiveTool_Internal();
                return;
            }
            CurrentTool.WhileActive(this,_Target);
        }
    }
}

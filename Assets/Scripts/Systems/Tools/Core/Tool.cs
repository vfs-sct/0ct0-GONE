using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//[CreateAssetMenu(menuName = "Systems/Tools/<Toolname>")]
public abstract class Tool : ScriptableObject
{
    public delegate void ToolEvent(ToolController owner,GameObject target);

    [SerializeField] private bool _ActivateIsToggle = false;
    public bool ActivateIsToggle{get=>_ActivateIsToggle;}

    public ToolEvent OnActivateEvent;
    public ToolEvent OnDeactivateEvent;
    public ToolEvent WhileActiveEvent;
    public ToolEvent OnSelectEvent;
    public ToolEvent OnDeselectEvent;

    protected string ToolName = "";
    protected abstract void OnActivate(ToolController owner,GameObject target);
    protected abstract void OnDeactivate(ToolController owner,GameObject target);
    protected abstract void OnWhileActive(ToolController owner,GameObject target);
    protected abstract void OnSelect(ToolController owner);
    protected abstract void OnDeselect(ToolController owner);

    protected abstract bool ActivateCondition(ToolController owner,GameObject target);
    protected abstract bool DeactivateCondition(ToolController owner,GameObject target);
    protected abstract bool LoopCondition(ToolController owner,GameObject target);



    public void Select(ToolController owner)
    {
        OnSelect(owner);
        if (OnSelectEvent != null) OnSelectEvent(owner,null);
    }

    public void Deselect(ToolController owner)
    {
        OnDeselect(owner);
        if (OnDeselectEvent != null) OnDeselectEvent(owner,null);
    }

    public bool Activate(ToolController owner,GameObject target)
    {
        if (!ActivateCondition(owner,target)) return false;
        OnActivate(owner,target);
        if (OnActivateEvent != null) OnActivateEvent(owner,target);
        return true;
    }

    public bool Deactivate(ToolController owner,GameObject target)
    {
        if (!DeactivateCondition(owner,target)) return false;
        OnDeactivate(owner,target);
        if (OnDeactivateEvent != null) OnDeactivateEvent(owner,target);
        return true;
    }

    public bool WhileActive(ToolController owner,GameObject target)
    {
        if (!LoopCondition(owner,target)) return false;
        WhileActive(owner,target);
        if (WhileActiveEvent != null) WhileActiveEvent(owner,target);
        return true;
    }



}

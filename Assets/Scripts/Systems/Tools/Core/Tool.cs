using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class Tool : ScriptableObject
{
    public delegate void ToolEvent(ToolController owner);

    public ToolEvent OnActivateEvent;
    public ToolEvent OnDeactivateEvent;
    public ToolEvent WhileActiveEvent;

    protected string ToolName = "";
    public abstract void OnActivate();
    public abstract void OnDeactivate();
    public abstract void OnWhileActive();

    public void Activate(ToolController owner)
    {
        if (OnActivateEvent != null) OnActivateEvent(owner);
    }

    public void Deactivate(ToolController owner)
    {
        if (OnDeactivateEvent != null) OnDeactivateEvent(owner);
    }

    public void WhileActive(ToolController owner)
    {
        if (WhileActiveEvent != null) WhileActiveEvent(owner);
    }



}

using UnityEngine;

//[CreateAssetMenu(menuName = "Systems/Tools/<Toolname>")]
public abstract class Tool : ScriptableObject
{
    public delegate void ToolEvent(ToolController owner,GameObject target);

    [SerializeField] private bool _ActivateIsToggle = false;
    [SerializeField] public string displayName = "";
    [SerializeField] public Sprite toolIcon = null;
    [SerializeField] protected float _ToolRange = 5.0f;
    public float ToolRange{get=>_ToolRange;}
    public bool ActivateIsToggle{get=>_ActivateIsToggle;}

    public ToolEvent OnActivateEvent;
    public ToolEvent OnDeactivateEvent;
    public ToolEvent WhileActiveEvent;
    public ToolEvent OnSelectEvent;
    public ToolEvent OnDeselectEvent;

    protected abstract void OnActivate(ToolController owner,GameObject target);
    protected abstract void OnDeactivate(ToolController owner,GameObject target);
    protected abstract void OnWhileActive(ToolController owner,GameObject target);
    protected abstract void OnSelect(ToolController owner);
    protected abstract void OnDeselect(ToolController owner);

    protected abstract bool ActivateCondition(ToolController owner,GameObject target);
    protected abstract bool DeactivateCondition(ToolController owner,GameObject target);
    protected abstract bool LoopCondition(ToolController owner,GameObject target);

    private bool IsActive = false;

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
        IsActive = true;
        return true;
    }

    public bool Deactivate(ToolController owner,GameObject target)
    {
        if (!DeactivateCondition(owner,target)) return false;
        OnDeactivate(owner,target);
        if (OnDeactivateEvent != null) OnDeactivateEvent(owner,target);
        IsActive = false;
        return true;
    }

    public bool WhileActive(ToolController owner,GameObject target)
    {
        if (!LoopCondition(owner,target)) return false;
        OnWhileActive(owner,target);
        if (WhileActiveEvent != null) WhileActiveEvent(owner,target);
        return true;
    }



}

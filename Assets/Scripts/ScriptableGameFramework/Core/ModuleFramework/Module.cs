using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module: ScriptableObject
{

    [Header("Module Settings")]
    [SerializeField]
    public ModuleManager _ModuleManager;

    [SerializeField]
    public bool RunUpdate = false;
    [SerializeField] 
    public bool StartOnSceneLoad = false;

    public virtual void Start()
    {

    }

    public abstract void Reset();

    public virtual void Initialize()
    {

    }

    public virtual void Update()
    {

    }
}

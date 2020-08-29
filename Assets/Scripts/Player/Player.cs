﻿using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using ScriptableGameFramework;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ToolController))]
public class Player : MonoBehaviour
{
    [Header("Game:")]
    [SerializeField] private GameFrameworkManager GameManager  = null;
    [SerializeField] protected UIModule UIRootModule = null;
    [SerializeField] private Playing PlayingState = null;
    [SerializeField] private LayerMask TargetableMask;
    [SerializeField] public Camera PlayerCamera = null;

    [SerializeField] private Transform CrosshairTransform;
    [SerializeField] public EventModule EventModule = null;

    [Header("Player:")]
    [SerializeField] private float TargetingDistance = 1000.0f;

    [SerializeField] private float MaxRangefinderDistance = 1000.0f;

    [SerializeField] private Resource FuelResource = null;

    [SerializeField] private PlayerCamera CameraScript = null;

    [SerializeField] private ResourceInventory LinkedInventory = null;

    [SerializeField] private Transform _ObjectHoldPosition;

    public Transform ObjectHoldPosition{get=>_ObjectHoldPosition;}
    public ResourceInventory Inventory{get=>LinkedInventory;}

    [Header("UI Elements:")]
    [SerializeField] private GameObject CraftingTooltip = null;
    [SerializeField] private GameObject RefuellingTooltip = null;
    [SerializeField] private GameObject RepairableScreenTooltip = null;
    [SerializeField] private GameObject OffloadNanitesTooltip = null;

    private GameOver GameOverScreen = null;
    private Win WinScreen = null;

    [SerializeField] private ScannerComponent Scanner;
    [SerializeField] private MovementController LinkedMovementController;
    [SerializeField] private ToolController LinkedToolController;

    [Header("PlayerPref Options:")]
    //used by UI/playerprefs to invert camera
    public int invertedCam = 1;
    public float lookSensitivity;

    [Header("Do not touch:")]
    public Collider mouseCollision = null;
    public GameObject mouseCollisionRoot = null;
    public float collisionDistance;

    

    private Collider _TargetCollider;

    private Vector3 RotationInput = new Vector3();

    private int LastToolSelectedIndex = -1;

    //used for revertin mats on target/highlight objects when theyre deselected
    private GameObject targetObject = null;


    private GameObject _TargetedObject;
    public GameObject TargetedObject {get=> _TargetedObject;}



    private bool disableCam = false;

    public void SetTarget(GameObject newTarget)
    {
        _TargetedObject = newTarget;
    }

    public void DisableCam()
    {
        disableCam = true;
    }

    public void EnableCam()
    {
        disableCam = false;
    }

    public void OnSelectTool1(InputAction.CallbackContext context)//goo glue
    {
        if (!GameManager.isPaused)
        {
            if (LastToolSelectedIndex == 0)
            {
                ResetSelectedTool();
                return;
            }
            LastToolSelectedIndex = 0;
            LinkedToolController.SwitchTool(0);
        }
    }
    public void OnSelectTool2(InputAction.CallbackContext context)//ScrewDriver
    {
        if (!GameManager.isPaused)
        {
            if (LastToolSelectedIndex == 1)
            {
                ResetSelectedTool();
                return;
            }
            LastToolSelectedIndex = 1;
            LinkedToolController.SwitchTool(1);
        }
    }
    public void OnSelectTool3(InputAction.CallbackContext context)//Claw
    {
        if (!GameManager.isPaused)
        {
            if (LastToolSelectedIndex == 2)
            {
                ResetSelectedTool();
                return;
            }
            LastToolSelectedIndex = 2;
            LinkedToolController.SwitchTool(2);
        }
    }

    public void OnSelectTool4(InputAction.CallbackContext context)//Laser Cutter
    {
        if (!GameManager.isPaused)
        {
            if (LastToolSelectedIndex == 3)
            {
                ResetSelectedTool();
                return;
            }
            LastToolSelectedIndex = 3;
            LinkedToolController.SwitchTool(3);
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        RotationInput.z = context.action.ReadValue<float>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //Debug.Log(invertedCam);
        RotationInput.y = context.action.ReadValue<Vector2>().x * lookSensitivity;
        RotationInput.x = context.action.ReadValue<Vector2>().y * invertedCam * lookSensitivity;
    }

    private void UpdateCamera()
    {
        CameraScript.RotateCamera(RotationInput);
    }


    private void UpdateCharacterRotation()
    {
        LinkedMovementController.SetRotationTarget(CameraScript.Root.transform.rotation.eulerAngles);
    }


    public void TriggerTool(InputAction.CallbackContext context)
    {
        if (context.performed) OnActivateTool(context);
        if (context.canceled) OnDeactivateTool(context);
    }

    public void OnActivateTool(InputAction.CallbackContext context)
    {
        //Debug.Log("Press");
        LinkedToolController.ActivateTool();

    }

    public void OnDeactivateTool(InputAction.CallbackContext context)
    {
        //Debug.Log("Release");
        LinkedToolController.DeactivateTool();
    }

    private void ResetSelectedTool()
    {
        LastToolSelectedIndex = -1;
        LinkedToolController.DeselectTool();
    }

    public void OnScanSalvage(InputAction.CallbackContext context)
    {
        Scanner.DoScan();
    }

    //used to put the default shader back on a highlighted or targeted object once it is no longer highlighted/targeted
    public void RevertMaterial(GameObject prevObj, Material prevMat)
    {
        //Debug.Log("No target highlighted/selected");
        if (prevObj != null)
        {
            prevObj.GetComponentInChildren<MeshRenderer>().material = prevMat;
        }
    }

    public void GameOver()
    {
        GameManager.Pause();
        GameOverScreen.gameObject.SetActive(true);
    }

    public void Win()
    {
        //GameManager.Pause();
        if(WinScreen == null) WinScreen = UIRootModule.UIRoot.GetScreen<Win>();
        WinScreen.gameObject.SetActive(true);
        //UIRootModule.UIRoot.GetScreen<Codex>().ResetCodexLocks();
    }

    private void Awake()
    {
        PlayingState.RegisterPlayer(this);
        invertedCam = PlayerPrefs.GetInt("InvertedCam");

        GameOverScreen = UIRootModule.UIRoot.GetScreen<GameOver>();
        WinScreen = UIRootModule.UIRoot.GetScreen<Win>();
    }

    public void ShowTooltips()
    {
        //exit early
        if(mouseCollision == null)
        {
            CraftingTooltip.SetActive(false);
            RefuellingTooltip.SetActive(false);
            RepairableScreenTooltip.SetActive(false);
            OffloadNanitesTooltip.SetActive(false);
            return;
        }

        //get the parent of our collision if there is one
        var root = mouseCollision.gameObject.transform;
        while (root.parent != null)
        {
            root = root.parent;
        }

        if (root.tag == "Station")
        {
            CraftingTooltip.SetActive(true);
            RefuellingTooltip.SetActive(true);
            RepairableScreenTooltip.SetActive(false);
        }
        else if(root.tag == "Crafting")
        {
            CraftingTooltip.SetActive(true);
            RefuellingTooltip.SetActive(false);
            RepairableScreenTooltip.SetActive(false);
        }
        else if (root.tag == "Refuel")
        {
            RefuellingTooltip.SetActive(true);
            RepairableScreenTooltip.SetActive(false);
        }
        else if(root.tag == "Repairable")
        {
            RepairableScreenTooltip.SetActive(true);
            CraftingTooltip.SetActive(false);
            RefuellingTooltip.SetActive(false);
        }
        else if(root.tag == "NaniteFactory")
        {
            OffloadNanitesTooltip.SetActive(true);
        }
    }


    public bool StationInRange(out Collider target)
    {
        bool canInteract = false;

        if (mouseCollision != null)
        {
            //Debug.Log($"CRAFT COLLISION: {mouseCollisionRoot.tag}, {mouseCollisionRoot.name}");
            //Debug.Log(mouseCollision);
            
            //can craft only at station
            if(mouseCollisionRoot.tag == "Crafting" || mouseCollisionRoot.tag == "Station")
            {
                canInteract = true;
            }
        }
        target = mouseCollision;
        return canInteract;
    }

    public bool StationInRange()
    {
        Collider temp;
        return StationInRange(out temp);
    }

    public bool RepairComponentsInRange(out Collider target)
    {
        bool canOpen = false;
        if (mouseCollision != null)
        {
            //can refuel at a station or anything tagged refuel
            if (mouseCollisionRoot.tag == "Repairable")
            {
                canOpen = true;
            }
        }
        //Debug.LogError("CanOpen from Player" + canOpen);
        target = mouseCollision;
        return canOpen;
    }

    public bool RefuelInRange()
    {
        bool canRefuel = false;
        if (mouseCollision != null)
        {
            //Debug.Log($"CRAFT COLLISION: {mouseCollisionRoot.tag}, {mouseCollisionRoot.name}");

            //can refuel at a station or anything tagged refuel
            if (mouseCollisionRoot.tag == "Refuel" || mouseCollisionRoot.tag == "Station")
            {
                canRefuel = true;
            }
        }

        return canRefuel;
    }

    //if the collision object is a child, find the parent and return it
    public GameObject GetMouseCollisionRoot(Collider mouseCollision)
    {
        if (mouseCollision != null)
        {
            var root = mouseCollision.gameObject.transform;
            while (root.parent != null)
            {
                root = root.parent;
            }

            return root.gameObject;
        }

        return null;
    }

    //raycast to see if mouse is hovering anything
    public Collider GetMouseCollision()
    {
        RaycastHit TargetHit;
        if (Physics.Raycast(CrosshairTransform.position, PlayerCamera.transform.forward, out TargetHit, MaxRangefinderDistance, TargetableMask))
        {
            //collision distance is used by the HUD to display how far away the object the player is looking at is
            collisionDistance = (float)(Math.Round(TargetHit.distance, 1));
            
            _TargetCollider = TargetHit.collider;
            targetObject = _TargetCollider.gameObject;
            if (TargetHit.distance < TargetingDistance) _TargetedObject = _TargetCollider.gameObject;
            if (_TargetCollider.GetComponentInChildren<MeshRenderer>() == null)
            {
                _TargetCollider = null;
            }
        }
        else
        {
            _TargetCollider = null;
        }

        return _TargetCollider;
    }



    private void Update()
    {
        if (GameManager.isPaused)
        {
            return;
        }
        //one raycast is done and then passed to everything that needs to use it (tooltips, highlight, target, etc)
        mouseCollision = GetMouseCollision();
        //if the thing hit by the raycast is a child, mouseCollisionRoot will get the top level parent gameobject
        mouseCollisionRoot = GetMouseCollisionRoot(mouseCollision);
        //TryHighlight();
        ShowTooltips();
        if (disableCam == false)
        {
            EventModule.UpdateEvents(gameObject);
            UpdateCamera();
            UpdateCharacterRotation();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ToolController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager  = null;
    [SerializeField] private Playing PlayingState;
    [SerializeField] private LayerMask TargetableMask;
    [SerializeField] public Camera PlayerCamera;
    [SerializeField] public EventModule EventModule;

    [SerializeField] private PlayerSatelliteHolder SatHolder;

    [SerializeField] private float TargetingDistance = 1000.0f;

    [SerializeField] private Resource FuelResource;

    [SerializeField] private PlayerCamera CameraScript;

    [SerializeField] private ResourceInventory LinkedInventory;
    public ResourceInventory Inventory{get=>LinkedInventory;}

    [SerializeField] private GameOver GameOverScreen;
    [SerializeField] private GameOver WinScreen;
    [SerializeField] private GameObject CraftingTooltip = null;
    [SerializeField] private GameObject RefuellingTooltip = null;

    private MovementController LinkedMovementController;
    private ToolController LinkedToolController;

    private Collider _TargetCollider;

    private Vector3 RotationInput = new Vector3();

    private int LastToolSelectedIndex = -1;

    private GameObject targetObject = null;
    private Material lastTargetMat = null;
    private GameObject highlightObject = null;
    private Material lastHighlightMat = null;

    //used by UI/playerprefs to invert camera
    public int invertedCam;
    public float lookSensitivity;

    public Collider mouseCollision = null;

    public void OnSelectTool1()//goo glue
    {
        if (LastToolSelectedIndex == 0)
        {
            ResetSelectedTool();
            return;
        }
        LastToolSelectedIndex = 0;
        LinkedToolController.SwitchTool(0);
    }
    public void OnSelectTool2()//ScrewDriver
    {
        if (LastToolSelectedIndex == 1)
        {
            ResetSelectedTool();
            return;
        }
        LastToolSelectedIndex = 1;
        LinkedToolController.SwitchTool(1);
    }
    public void OnSelectTool3()//Claw
    {
        if (LastToolSelectedIndex == 2)
        {
            ResetSelectedTool();
            return;
        }
        LastToolSelectedIndex = 2;
        LinkedToolController.SwitchTool(2);
    }

    public void OnSelectTool4()//Laser Cutter
    {
        if (LastToolSelectedIndex == 3)
        {
            ResetSelectedTool();
            return;
        }
        LastToolSelectedIndex = 3;
        LinkedToolController.SwitchTool(3);
    }

    public void OnRoll(InputValue value)
    {
        RotationInput.z = value.Get<float>();
    }

    public void OnLook(InputValue value)
    {
        RotationInput.y = value.Get<Vector2>().x * lookSensitivity;
        RotationInput.x = value.Get<Vector2>().y * invertedCam * lookSensitivity;
    }

    private void UpdateCamera()
    {
        CameraScript.RotateCamera(RotationInput);
    }


    private void UpdateCharacterRotation()
    {
        LinkedMovementController.SetRotationTarget(CameraScript.Root.transform.rotation.eulerAngles);
    }


    public void OnActivateTool()
    {
        LinkedToolController.ActivateTool();

        //testing code for satellite placement
        //if (LastToolSelectedIndex == -1) SatHolder.Place();
    }

    public void OnDeactiveTool()
    {
        LinkedToolController.DeactivateTool();
    }

    private void ResetSelectedTool()
    {
        LastToolSelectedIndex = -1;
        LinkedToolController.DeselectTool();
    }

    private void OnEnable()
    {
        
    }

    public void OnLockTarget()
    {
        TryTarget();
    }

    private void TryTarget()
    {
        _TargetCollider = mouseCollision;

        if (_TargetCollider != null && _TargetCollider.name != "Player" && _TargetCollider.tag != "Cloud") 
        {
            var root = mouseCollision.gameObject.transform;
            while (root.parent != null)
            {
                root = root.parent;
            }
            //don't target crafting stations
            if (root.tag != "Refuel")
            {
                //is there an already targeted object that needs to be untargeted
                if (targetObject != null)
                {
                    Debug.Log("Old target: " + targetObject);
                    targetObject.GetComponentInChildren<MeshRenderer>().material = lastTargetMat;
                }

                if (_TargetCollider.gameObject == highlightObject)
                {
                    lastTargetMat = lastHighlightMat;

                    lastHighlightMat = null;
                }
                else
                {
                    lastTargetMat = _TargetCollider.GetComponentInChildren<MeshRenderer>().material;
                }

                highlightObject = null;
                targetObject = _TargetCollider.gameObject;
                LinkedToolController.SetTarget(targetObject);
                Debug.Log("Targeted: " + targetObject);

                _TargetCollider.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("TargetHighlightMaterial");
            }
        }
        else
        {
            RevertMaterial(targetObject, lastTargetMat);
            targetObject = null;
            lastTargetMat = null;
            LinkedToolController.ClearTarget();
        }
    }

    //used to put the default shader back on a highlighted or targeted object once it is no longer highlighted/targeted
    public void RevertMaterial(GameObject prevObj, Material prevMat)
    {
        Debug.Log("No target highlighted/selected");
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
        GameManager.Pause();
        WinScreen.gameObject.SetActive(true);
    }


    private void Awake()
    {
        PlayingState.RegisterPlayer(this);
    }


    private void Start()
    {
        LinkedMovementController = GetComponent<MovementController>();
        LinkedToolController = GetComponent<ToolController>();
    }

    public void ShowTooltips()
    {
        //exit early
        if(mouseCollision == null)
        {
            CraftingTooltip.SetActive(false);
            RefuellingTooltip.SetActive(false);
            return;
        }

        //get the parent of our collision if there is one
        var root = mouseCollision.gameObject.transform;
        while (root.parent != null)
        {
            root = root.parent;
        }

        if (root.tag == "Refuel")
        {
            CraftingTooltip.SetActive(true);
            RefuellingTooltip.SetActive(true);
        }
    }

    private void TryHighlight()
    {
        //just bc i dont wanna rename everything rn
        _TargetCollider = mouseCollision;

        if (_TargetCollider != null && _TargetCollider.name != "Player" && _TargetCollider.tag != "Cloud" && _TargetCollider.gameObject != targetObject)
        {
            //is there a previously highlighted object that needs to be unhighlighted
            if (highlightObject != null)
            {
                //Debug.Log("Old highlight: " + highlightObject);
                highlightObject.GetComponentInChildren<MeshRenderer>().material = lastHighlightMat;
            }
            highlightObject = _TargetCollider.gameObject;
            //Debug.Log("Highlighted: " + highlightObject);

            lastHighlightMat = _TargetCollider.GetComponentInChildren<MeshRenderer>().material;
            _TargetCollider.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("HoverHighlightMaterial");
        }
        else if(_TargetCollider != null && _TargetCollider.gameObject != targetObject)
        {
            RevertMaterial(highlightObject, lastHighlightMat);
            highlightObject = null;
            lastHighlightMat = null;
        }
    }
    public bool StationInRange(bool canCraft)
    {
        if (mouseCollision != null)
        {
            var root = mouseCollision.gameObject.transform;
            while(root.parent != null)
            {
                root = root.parent;
            }

            Debug.Log($"CRAFT COLLISION: {root.tag}, {root.name}");

            if(root.tag == "Refuel")
            {
                canCraft = true;
            }
            else
            {
                canCraft = false;
            }
        }
        else
        {
            canCraft = false;
        }

        return canCraft;
    }

    public Collider GetMouseCollision()
    {
        RaycastHit TargetHit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out TargetHit, TargetingDistance, TargetableMask))
        {
            _TargetCollider = TargetHit.collider;

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
        mouseCollision = GetMouseCollision();
        TryHighlight();
        ShowTooltips();
        EventModule.UpdateEvents(gameObject);
        UpdateCamera();
        UpdateCharacterRotation();
    }


}

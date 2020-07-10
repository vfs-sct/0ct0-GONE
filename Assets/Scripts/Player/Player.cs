using UnityEngine;
using System;
using UnityEngine.InputSystem;


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
    [SerializeField] public EventModule EventModule = null;

    [Header("Player:")]
    [SerializeField] private float TargetingDistance = 1000.0f;

    [SerializeField] private Resource FuelResource = null;

    [SerializeField] private PlayerCamera CameraScript = null;

    [SerializeField] private ResourceInventory LinkedInventory = null;

    [SerializeField] private Transform _ObjectHoldPosition;

    public Transform ObjectHoldPosition{get=>_ObjectHoldPosition;}
    public ResourceInventory Inventory{get=>LinkedInventory;}

    [Header("UI Elements:")]
    [SerializeField] private GameObject CraftingTooltip = null;
    [SerializeField] private GameObject RefuellingTooltip = null;
    [SerializeField] private GameObject TargetingTooltip = null;

    private GameOver GameOverScreen = null;
    private Win WinScreen = null;

    [SerializeField] private ScannerComponent Scanner;

    [Header("PlayerPref Options:")]
    //used by UI/playerprefs to invert camera
    public int invertedCam = 1;
    public float lookSensitivity;

    [Header("Do not touch:")]
    public Collider mouseCollision = null;
    public GameObject mouseCollisionRoot = null;
    public float collisionDistance;

    private MovementController LinkedMovementController;
    private ToolController LinkedToolController;

    private Collider _TargetCollider;

    private Vector3 RotationInput = new Vector3();

    private int LastToolSelectedIndex = -1;

    //used for revertin mats on target/highlight objects when theyre deselected
    private GameObject targetObject = null;
    private Material lastTargetMat = null;
    private GameObject highlightObject = null;
    private Material lastHighlightMat = null;
    private Material HighlightMaterial = null;

    public void OnSelectTool1()//goo glue
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
    public void OnSelectTool2()//ScrewDriver
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
    public void OnSelectTool3()//Claw
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

    public void OnSelectTool4()//Laser Cutter
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

    public void OnRoll(InputValue value)
    {
        RotationInput.z = value.Get<float>();
    }

    public void OnLook(InputValue value)
    {
        //Debug.Log(invertedCam);
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
        if (targetObject != null) LinkedToolController.SetTarget(targetObject);
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

    public void OnScanSalvage()
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
        GameManager.Pause();
        WinScreen.gameObject.SetActive(true);
        //UIRootModule.UIRoot.GetScreen<Codex>().ResetCodexLocks();
    }

    private void Awake()
    {
        PlayingState.RegisterPlayer(this);
        invertedCam = PlayerPrefs.GetInt("InvertedCam");
        HighlightMaterial  = Resources.Load<Material>("HighlightMaterial");
    }

    private void Start()
    {
        GameOverScreen = UIRootModule.UIRoot.GetScreen<GameOver>();
        WinScreen = UIRootModule.UIRoot.GetScreen<Win>();
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

        if (root.tag == "Station")
        {
            CraftingTooltip.SetActive(true);
            RefuellingTooltip.SetActive(true);
        }
        if (root.tag == "Refuel")
        {
            RefuellingTooltip.SetActive(true);
        }
    }

    private void TryHighlight()
    {
        if (mouseCollision != null && mouseCollision.name != "Player" && mouseCollision.tag != "Cloud") // && mouseCollision.gameObject != targetObject
        {
            //is there a previously highlighted object that needs to be unhighlighted?
            if (highlightObject != null)
            {
                //Debug.Log("Old highlight: " + highlightObject);
                highlightObject.GetComponentInChildren<MeshRenderer>().material = lastHighlightMat;
            }
            highlightObject = mouseCollision.gameObject;
            targetObject = mouseCollision.gameObject;
            //Debug.Log("Highlighted: " + highlightObject);

            lastHighlightMat = mouseCollision.GetComponentInChildren<MeshRenderer>().material;
            MeshRenderer TargetMeshRender = mouseCollision.GetComponentInChildren<MeshRenderer>();
           
            Salvagable TargetSalvage = mouseCollision.GetComponentInChildren<Salvagable>();
            if (TargetSalvage != null)
            {
                TargetMeshRender.material.color = TargetSalvage.SalvageItem.ResourceType.ResourceColor;
                TargetMeshRender.material = TargetSalvage.SalvageItem.ResourceType.ResourceHighlight;
            }
            else
            {
                TargetMeshRender.material = HighlightMaterial;
            }
        }
    }
    public bool StationInRange()
    {
        bool canInteract = false;

        if (mouseCollision != null)
        {
            //Debug.Log($"CRAFT COLLISION: {mouseCollisionRoot.tag}, {mouseCollisionRoot.name}");
            //Debug.Log(mouseCollision);
            
            //can craft only at station
            if(mouseCollisionRoot.tag == "Station")
            {
                canInteract = true;
            }
            else
            {
                canInteract = false;
            }
        }

        return canInteract;
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
            else
            {
                canRefuel = false;
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
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out TargetHit, TargetingDistance, TargetableMask))
        {
            //collision distance is used by the HUD to display how far away the object the player is looking at is
            collisionDistance = (float)(Math.Round(TargetHit.distance, 1));
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
        //one raycast is done and then passed to everything that needs to use it (tooltips, highlight, target, etc)
        mouseCollision = GetMouseCollision();
        //if the thing hit by the raycast is a child, mouseCollisionRoot will get the top level parent gameobject
        mouseCollisionRoot = GetMouseCollisionRoot(mouseCollision);
        TryHighlight();
        ShowTooltips();
        EventModule.UpdateEvents(gameObject);
        UpdateCamera();
        UpdateCharacterRotation();
    }
}

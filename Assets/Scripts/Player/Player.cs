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
    //[SerializeField] protected UIModule UIRootModule = null;
    [SerializeField] private Playing PlayingState = null;
    [SerializeField] private LayerMask TargetableMask;
    [SerializeField] public Camera PlayerCamera = null;
    [SerializeField] public EventModule EventModule = null;

    [SerializeField] private PlayerSatelliteHolder SatHolder = null;

    [Header("Player:")]
    [SerializeField] private float TargetingDistance = 1000.0f;

    [SerializeField] private Resource FuelResource = null;

    [SerializeField] private PlayerCamera CameraScript = null;

    [SerializeField] private ResourceInventory LinkedInventory = null;
    public ResourceInventory Inventory{get=>LinkedInventory;}

    [Header("UI Elements:")]
    [SerializeField] private GameOver GameOverScreen = null;
    [SerializeField] private GameOver WinScreen = null;
    [SerializeField] private GameObject CraftingTooltip = null;
    [SerializeField] private GameObject RefuellingTooltip = null;
    [SerializeField] private GameObject TargetingTooltip = null;

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

    public void OnLockTarget()
    {
        TryTarget();
    }

    private void TryTarget()
    {
        if (mouseCollision != null && mouseCollision.name != "Player" && mouseCollision.tag != "Cloud") 
        {
            //don't target crafting stations
            if (mouseCollisionRoot.tag != "Refuel")
            {
                //is there an already targeted object that needs to be untargeted
                if (targetObject != null)
                {
                    Debug.Log("Old target: " + targetObject);
                    targetObject.GetComponentInChildren<MeshRenderer>().material = lastTargetMat;
                }

                if (mouseCollision.gameObject == highlightObject)
                {
                    lastTargetMat = lastHighlightMat;

                    lastHighlightMat = null;
                }
                else
                {
                    lastTargetMat = mouseCollision.GetComponentInChildren<MeshRenderer>().material;
                }

                highlightObject = null;
                targetObject = mouseCollision.gameObject;
                LinkedToolController.SetTarget(targetObject);
                //Debug.Log("Targeted: " + targetObject);

                mouseCollision.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("TargetHighlightMaterial");
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
        if (mouseCollision != null && mouseCollision.name != "Player" && mouseCollision.tag != "Cloud" && mouseCollision.gameObject != targetObject)
        {
            //is there a previously highlighted object that needs to be unhighlighted?
            if (highlightObject != null)
            {
                //Debug.Log("Old highlight: " + highlightObject);
                highlightObject.GetComponentInChildren<MeshRenderer>().material = lastHighlightMat;
            }

            highlightObject = mouseCollision.gameObject;
            //Debug.Log("Highlighted: " + highlightObject);

            lastHighlightMat = mouseCollision.GetComponentInChildren<MeshRenderer>().material;
            mouseCollision.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("HoverHighlightMaterial");

            if(mouseCollisionRoot.tag != "Refuel")
            {
                TargetingTooltip.SetActive(true);
            }
        }
        else if(mouseCollision != null && mouseCollision.gameObject != targetObject)
        {
            RevertMaterial(highlightObject, lastHighlightMat);
            highlightObject = null;
            lastHighlightMat = null;
        }

        if (mouseCollision == null || mouseCollision.gameObject == targetObject || mouseCollisionRoot.tag == "Refuel")
        {
            TargetingTooltip.SetActive(false);
        }
    }
    public bool StationInRange(bool canCraft)
    {
        if (mouseCollision != null)
        {
            //Debug.Log($"CRAFT COLLISION: {mouseCollisionRoot.tag}, {mouseCollisionRoot.name}");

            if(mouseCollisionRoot.tag == "Refuel")
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

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
    [SerializeField] private Camera PlayerCamera;

    [SerializeField] private PlayerSatelliteHolder SatHolder;

    [SerializeField] private float TargetingDistance = 1000.0f;

    [SerializeField] private Resource FuelResource;

    [SerializeField] private PlayerCamera CameraScript;

    [SerializeField] private ResourceInventory LinkedInventory;
    public ResourceInventory Inventory{get=>LinkedInventory;}

    [SerializeField] private GameOver GameOverScreen;

    private MovementController LinkedMovementController;
    private ToolController LinkedToolController;

    private Collider _TargetCollider;

    private Vector3 RotationInput = new Vector3();

    private int LastToolSelectedIndex = -1;

    private GameObject targetObject = null;
    private Material lastTargetMat = null;

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
        RotationInput.y = value.Get<Vector2>().x;
        RotationInput.x = value.Get<Vector2>().y;
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
        RaycastHit TargetHit; 
        if (Physics.Raycast(PlayerCamera.transform.position,PlayerCamera.transform.forward,out TargetHit,TargetingDistance,TargetableMask))
        {
            _TargetCollider = TargetHit.collider;
        }  
        else 
        {
            _TargetCollider = null;
        }
        Debug.Log(_TargetCollider);
        if (_TargetCollider!= null) 
        {
            LinkedToolController.SetTarget(_TargetCollider.gameObject);
            targetObject = _TargetCollider.gameObject;
            lastTargetMat = _TargetCollider.GetComponentInChildren<MeshRenderer>().material;
            _TargetCollider.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("TargetHighlightMaterial");
        }
        else
        {
            if(targetObject != null)
            {
                targetObject.GetComponent<MeshRenderer>().material = lastTargetMat;
            }
            targetObject = null;
            lastTargetMat = null;
            LinkedToolController.SetTarget(null);
        }
    }

    public void GameOver()
    {
        GameManager.Pause();
        GameOverScreen.gameObject.SetActive(true);
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
    



    private void Update()
    {
        UpdateCamera();
        UpdateCharacterRotation();
    }


}

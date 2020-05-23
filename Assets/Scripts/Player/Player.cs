using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ToolController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager  = null;
    [SerializeField] private LayerMask TargetableMask;

    [SerializeField] private Camera PlayerCamera;

    [SerializeField] private float TargetingDistance = 1000.0f;

    [SerializeField] private Resource FuelResource;

    [SerializeField] private ResourceInventory LinkedInventory;

    [SerializeField] private GameOver GameOverScreen;

    private MovementController LinkedMovementController;
    private ToolController LinkedToolController;

    private Collider _TargetCollider;

    
    
    private int LastToolSelectedIndex = -1;

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

    public void OnActivateTool()
    {
        LinkedToolController.ActivateTool();
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
        }
        else
        {
            LinkedToolController.SetTarget(null);
        }
    }

    private void GameOver()
    {
        GameManager.Pause();
        GameOverScreen.gameObject.SetActive(true);
    }


    private void Start()
    {
        LinkedMovementController = GetComponent<MovementController>();
        LinkedToolController = GetComponent<ToolController>();
    }
    
    private void Update()
    {
        Debug.Log("PlayerFuel = "+LinkedInventory.GetResource(FuelResource));
        if (LinkedInventory.GetResource(FuelResource) == 0)
        {

            GameOver();
        }
    }


}

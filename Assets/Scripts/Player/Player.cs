using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(ToolController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager  = null;

    private MovementController LinkedMovementController;
    private ToolController LinkedToolController;

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

    private void Start()
    {
        LinkedMovementController = GetComponent<MovementController>();
        LinkedToolController = GetComponent<ToolController>();
    }
    
    private void Update()
    {
        
    }


}

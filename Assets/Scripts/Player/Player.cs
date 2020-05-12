using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager  = null;
    [SerializeField] private InputModule _InputManager = null;

    public InputModule InputManager{get =>_InputManager;}

    private MovementController LinkedMovementController;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        LinkedMovementController = GetComponent<MovementController>();
        LinkedMovementController.SetInputManager(InputManager);
    }


    
    void Update()
    {
        
    }
}

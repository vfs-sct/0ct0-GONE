using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameFrameworkManager GameManager  = null;

    private MovementController LinkedMovementController;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        LinkedMovementController = GetComponent<MovementController>();
    }


    
    void Update()
    {
        
    }
}

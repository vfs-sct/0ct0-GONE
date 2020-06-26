using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    [SerializeField] private float MoveSpeedofAsteroid = 10f;

    private void Update()
    {
        transform.position += Vector3.left * MoveSpeedofAsteroid * Time.deltaTime;
    }
}

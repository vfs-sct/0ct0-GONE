using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{

    // Random Chance


    // Spawner properties
    public float SpawnTimer = 200f;
    public float SpawnHeight;
    public float SpawnWidth;
    public GameObject Asteroid;

    private float timer = 0f;
    private bool isSpawning = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("==== Player Entered ThreatField ====");
            SpawnChance();
        }
    }

    private void SpawnChance()
    {
        if(Random.value >= 0.5) // 50 percent chance everytime Octo enters the field.
        {
            Debug.Log("==== ACTIVATE ASTEROID EVENT ====");
            isSpawning = true;
        }
        else
        {
            Debug.Log("==== ASTEROID DID NOT SPAWN ====");
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("==== Player Exited ThreatField ====");
            isSpawning = false;
        }
    }

    private void Update()
    {
        if (isSpawning == true)
        {
            Debug.Log("==== Spawning Asteroids ====");
            SpawnAsteroid();
        }
        else 
        {
            return;
        }
    }

    private void SpawnAsteroid()
    {
        if(timer < SpawnTimer)
        {
            GameObject newAsteroid = Instantiate(Asteroid);
            newAsteroid.transform.position = transform.position + new Vector3(0, Random.Range(-SpawnHeight,SpawnHeight), Random.Range(-SpawnWidth, SpawnWidth));
            Destroy(newAsteroid, 15f);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
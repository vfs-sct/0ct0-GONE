using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
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
            isSpawning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("==== Player Exited ThreatField ====");
            isSpawning = false;
        }
        Debug.Log(isSpawning);
    }

    private void Update()
    {
        if (isSpawning == true)
        {
            SpawnAsteroid();
        }
        else 
        {
            return;
        }
    }

    private void SpawnAsteroid()
    {
        Debug.Log("==== Spawning Asteroids ====");
        
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float nextSpawnTime;


    [SerializeField]
    private GameObject[] objectSpawner;
    [SerializeField]
    private float spawnDelay = 10;


    private void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        nextSpawnTime = Time.time + spawnDelay;
        Instantiate(objectSpawner[0], transform.position, transform.rotation);
    }

    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }
}

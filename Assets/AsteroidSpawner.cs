using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidController prefab;
    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int spawnAmount = 5;
    private float trajectoryOffset = 20f;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn),spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            spawnDirection = new Vector3(spawnDirection.x, 0, spawnDirection.y);
            Vector3 spawnPoint = transform.position + spawnDirection;

            float offset = Random.Range(-trajectoryOffset, trajectoryOffset);
            Quaternion rotation = Quaternion.AngleAxis(offset, Vector3.up);

            AsteroidController asteroid = Instantiate(prefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            
            asteroid.Trajectory(rotation * -spawnDirection);
        }
    }
}

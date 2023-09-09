using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : MonoBehaviour
{
    public GameObject missilePrefab;   
    public float initialSpawnInterval = 2f;   
    public float minSpawnInterval = 0.5f;     
    public float spawnIntervalDecreaseRate = 0.1f;  
    public int initialMissileCount = 1;    
    public int maxMissileCount = 10;      
    public float screenOffset = 1.0f;     

    private float nextSpawnTime;

    private int currentMissileCount;

    public bool isGameStarted = false;

    public AudioManager audioManager;


    private void Start()
    {
        nextSpawnTime = Time.time + initialSpawnInterval;

        currentMissileCount = initialMissileCount;
    }

    private void Update()
    {
        if (isGameStarted)
        {
            if (Time.time >= nextSpawnTime && currentMissileCount < maxMissileCount)
            {
                for (int i = 0; i < currentMissileCount; i++)
                {
                    SpawnMissile();
                }

                nextSpawnTime = Time.time + Mathf.Max(initialSpawnInterval - spawnIntervalDecreaseRate * currentMissileCount, minSpawnInterval);

                currentMissileCount++;
            }
        }
    }

    private void SpawnMissile()
    {
        if (missilePrefab != null)
        {
            // Determine a random position outside the screen bounds
            Vector3 spawnPosition = CalculateSpawnPosition();

            GameObject missile = Instantiate(missilePrefab, spawnPosition, Quaternion.identity);            
        }
        else
        {
            Debug.LogError("Missile prefab not set in the spawner!");
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Randomly choose a side of the screen (top, bottom, left, or right)
        int side = Random.Range(0, 4);

        // Calculate the spawn position based on the chosen side
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0: // Top side
                spawnPosition = new Vector3(Random.Range(0f, screenWidth), screenHeight + screenOffset, 1f);
                break;
            case 1: // Bottom side
                spawnPosition = new Vector3(Random.Range(0f, screenWidth), -screenOffset, 1f);
                break;
            case 2: // Left side
                spawnPosition = new Vector3(-screenOffset, Random.Range(0f, screenHeight), 1f);
                break;
            case 3: // Right side
                spawnPosition = new Vector3(screenWidth + screenOffset, Random.Range(0f, screenHeight), 1f);
                break;
        }

        // Convert the screen position to world space
        return Camera.main.ScreenToWorldPoint(spawnPosition);
    }

    public void EnableThis()
    {
        isGameStarted = true;
    }

    public void DisableThis()
    {
        isGameStarted = false;
    }
}
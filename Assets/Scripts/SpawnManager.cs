using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject coinPrefab; 
    public GameObject powerUpPrefab; 

    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 3f; 

    private bool canSpawn = true; 

    void Start()
    {
        
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (canSpawn)
        {
            
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

            
            yield return new WaitForSeconds(spawnDelay);

            
            GameObject objectToSpawn = null;

            if (Random.value < 0.5f)
            {                               
                objectToSpawn = coinPrefab;
            }
            else
            {
                
                objectToSpawn = powerUpPrefab;
            }

           
            Vector3 spawnPosition = new Vector3(
                (Random.Range(Camera.main.transform.position.x - Camera.main.orthographicSize, Camera.main.transform.position.x + Camera.main.orthographicSize) + 10f),
                (Random.Range(Camera.main.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize) - 10f), 1f);

            
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}

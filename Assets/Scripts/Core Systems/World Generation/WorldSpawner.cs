using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class WorldSpawner : MonoBehaviour
{
    public float spawnRange = 10.0f;

    public float maxEnemies;
    public float minEnemies;
    float maxEnemySpawn;

    public float maxPickups;
    public float pickupVariation;
    public float maxChests;
    public float chestVariation;
    float actualChests;
    float actualPickups;


    [SerializeField]
    List<GameObject> enemies;
    [SerializeField]
    List<GameObject> pickups;
    [SerializeField]
    List<GameObject> chests;
    [SerializeField]
    GameObject shopkeeper;

    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject playerSpawn;
    [SerializeField] bool spawnPlayer;
    [SerializeField] bool spawnAtStart;

    [SerializeField] GameObject player;

    Vector3 mapPoint;

    public static Action PlayLevelMusic;
    public static Action PlayDesertMusic;

    void Start()
    {
        PlayLevelMusic?.Invoke();
        PlayDesertMusic?.Invoke();

        if (spawnAtStart)
        {
            maxEnemySpawn = UnityEngine.Random.Range(minEnemies, maxEnemies);
            actualChests = Mathf.Clamp(UnityEngine.Random.Range(maxChests - chestVariation, maxChests + chestVariation), 0, maxChests + chestVariation);
            actualPickups = Mathf.Clamp(UnityEngine.Random.Range(maxPickups - pickupVariation, maxPickups + pickupVariation), 0, maxPickups + pickupVariation);

            for (int i = 0; i < maxEnemySpawn; i++)
            {
                SpawnEnemies();
            }
            for (int j = 0; j < actualPickups; j++)
            {
                SpawnPickups();
            }
            for (int k = 0; k < actualChests; k++)
            {
                SpawnChests();
            }

            SpawnShop();

            // Once all stuff has been spawned in, enable the enemy spawner to start spawning enemies regularly
            if (enemySpawner) enemySpawner.SetActive(true);

            if (spawnPlayer)
            {
                // Move the player to the spawn point
                if (DontDestroy.instance != null)
                {
                    GameObject player = DontDestroy.instance.gameObject;
                    player.transform.position = playerSpawn.transform.position;
                }
            }
        }
    }

    public void StartSpawning(float range)
    {
        //spawnRange = range;

        maxEnemySpawn = UnityEngine.Random.Range(minEnemies, maxEnemies);
        actualChests = Mathf.Clamp(UnityEngine.Random.Range(maxChests - chestVariation, maxChests + chestVariation), 0, maxChests + chestVariation);
        actualPickups = Mathf.Clamp(UnityEngine.Random.Range(maxPickups - pickupVariation, maxPickups + pickupVariation), 0, maxPickups + pickupVariation);

        for (int i = 0; i < maxEnemySpawn; i++)
        {
            SpawnEnemies();
        }
        for (int j = 0; j < actualPickups; j++)
        {
            SpawnPickups();
        }
        for (int k = 0; k < actualChests; k++)
        {
            SpawnChests();
        }

        SpawnShop();

        // Once all stuff has been spawned in, enable the enemy spawner to start spawning enemies regularly
        if(enemySpawner) enemySpawner.SetActive(true);

        if (spawnPlayer)
        {
            // Move the player to the spawn point
            if (DontDestroy.instance != null)
            {
                GameObject player = DontDestroy.instance.gameObject;
                player.transform.position = playerSpawn.transform.position;
            }
        }
    }

    /* 
     * These functions attempt to spawn each object, recursively. You want to do this because if not, then there's a chance
     * that the object won't spawn if it can't find a point. So tell it to keep trying until it does spawn it.
    */

    void SpawnEnemies()
    {
        if (SpawnRandomPoint(transform.position, spawnRange, out mapPoint))
        {
            Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Count)], mapPoint, Quaternion.identity);
        }
        else { SpawnEnemies(); }
    }

    void SpawnPickups()
    {
        if (SpawnRandomPoint(transform.position, spawnRange, out mapPoint))
        {
            Instantiate(pickups[UnityEngine.Random.Range(0, pickups.Count)], mapPoint, Quaternion.identity);
        }
        else { SpawnPickups(); }
    }

    void SpawnChests()
    {
        if (SpawnRandomPoint(transform.position, spawnRange, out mapPoint))
        {
            Instantiate(chests[UnityEngine.Random.Range(0, chests.Count)], mapPoint, Quaternion.identity);
        }
        else { SpawnChests(); }
    }

    void SpawnShop()
    {
        if (SpawnRandomPoint(transform.position, spawnRange, out mapPoint))
        {
            Instantiate(shopkeeper, new(mapPoint.x, mapPoint.y + 1, mapPoint.z), Quaternion.identity);
        }
        else { SpawnShop(); }
    }

    bool SpawnRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, 1))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}

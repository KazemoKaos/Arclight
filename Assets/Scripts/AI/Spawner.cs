using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<SpawnCard> spawnableEnemies;
    SpawnCard currentEnemy;
    SpawnCard mostExpensiveEnemy;
    int currentEnemyCount;          // How many as been spawned
    public int maxEnemyCount;       // The max amount of enemies that can be spawned
    public float spawnDelay;        // The amount of delay between spawning enemies
    float currentSpawnDelay;
    float timer;

    float creditTimer;

    bool spawning;

    Vector3 spawnPoint;
    [SerializeField] float spawnRange;

    ScalingManager scale;

    public float credits;

    private void Start()
    {
        currentSpawnDelay = spawnDelay;
        scale = ScalingManager.instance;

        float temp = 0f;

        foreach(SpawnCard card in spawnableEnemies)
        {
            if(card.cost > temp) { mostExpensiveEnemy = card; temp = mostExpensiveEnemy.cost; }
        }
    }


    private void Update()
    {
        if (currentEnemyCount < maxEnemyCount)
        {
            if (timer >= currentSpawnDelay && !spawning)
            {
                spawning = true;
                SpawnEnemy();
            }
            else { timer += Time.deltaTime; }
        }

        if(creditTimer >= 1f)
        {
            credits += 0.75f * (1 + 0.4f * scale.diffScale);
            creditTimer = 0f;
        }
        else { creditTimer += Time.deltaTime; }
    }

    void SpawnEnemy()
    {
        if (GetNewEnemy())
        {
            if (SpawnRandomPoint(DontDestroy.instance.transform.position, spawnRange, out spawnPoint))      // Spawn near the player
            {
                GameObject temp = Instantiate(currentEnemy.spawnObject);
                temp.GetComponent<NavMeshAgent>().Warp(spawnPoint);
                credits -= currentEnemy.cost;
                currentEnemyCount += 1;
                timer = 0f;
                currentSpawnDelay = Random.Range(.5f, spawnDelay);
                spawning = false;
            }
            else { SpawnEnemy(); }
        }
        else
        {
            timer = 0f;
            currentSpawnDelay = Random.Range(1, spawnDelay);
            spawning = false;
        }
    }

    bool GetNewEnemy()
    {
        currentEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
        if(currentEnemy.cost > credits) { return false; }     // If the enemy is too expensive
        if(currentEnemy.cost * 6 < credits) { return false; } // If the amount of credits is more than 6 times the value
        if(currentEnemy.cost < mostExpensiveEnemy.cost && credits >= mostExpensiveEnemy.cost * 2) { currentEnemy = mostExpensiveEnemy; return true; }   // Can afford something more expensive

        return true;
    }

    bool SpawnRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
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

    void EnemyDefeat()
    {
        currentEnemyCount--;
    }

    private void OnEnable()
    {
        AbstractEnemy.EnemyDefeat += EnemyDefeat;
    }

    private void OnDisable()
    {
        AbstractEnemy.EnemyDefeat += EnemyDefeat;
    }
}

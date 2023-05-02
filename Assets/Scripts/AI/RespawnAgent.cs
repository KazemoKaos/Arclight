using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RespawnAgent : MonoBehaviour
{
    [SerializeField]
    public GameObject AgentPrefab;

    void respawnEnemyAgent()
    {
        //Respawn agent
        GameObject floor = GameObject.Find("Floor");
        Vector3 destination = new(
            Random.Range(floor.transform.localScale.x * -5f, floor.transform.localScale.x * 5f),
            1f,
            Random.Range(floor.transform.localScale.x * -5f, floor.transform.localScale.x * 5f)
            );
        Instantiate(AgentPrefab, destination, Quaternion.identity);
        Debug.Log("AgentRespawned/B was pressed");
    }

    void OnEnable()
    {
        InputManager.onSpawnEnemy += respawnEnemyAgent;
    }

    void OnDisable()
    {
        InputManager.onSpawnEnemy -= respawnEnemyAgent;
    }
}

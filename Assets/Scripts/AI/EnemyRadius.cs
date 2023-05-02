using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] BaseEnemyAI baseEnemyAI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (enemyAI != null)
            {
                enemyAI.player = other.gameObject.transform;
                enemyAI.playerInSightRange = true;
            }
            else if(baseEnemyAI != null)
            {
                baseEnemyAI.player = other.gameObject.transform;
                baseEnemyAI.playerInSightRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(enemyAI != null)
            {
                enemyAI.playerInSightRange = false;
            }
            else if(baseEnemyAI != null)
            {
                baseEnemyAI.playerInSightRange = false;
            }
        }
    }
}

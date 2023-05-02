using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : AbstractEnemy
{
    private NavMeshAgent agent;
    [HideInInspector] public Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform attackPoint;

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] public bool playerInSightRange, playerInAttackRange;

    // Level
    EnemyUI enemyUI;


    private void Awake()
    {
        enemyUI = GetComponent<EnemyUI>();
        agent = GetComponent<NavMeshAgent>();
        lootDrops = GetComponent<LootManager>();
    }

    private void Update()
    {
        //Check for sight and attack range
        if(playerInSightRange)
        {
            if(Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                playerInAttackRange = true;
            }
        }

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        coeff = levelGrab.coeff;
    }

    private void Patroling()
    {
        if (!walkPointSet) SpawnRandomPoint(transform.position, walkPointRange, out walkPoint);

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    bool SpawnRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        result = Vector3.zero;

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, 1))
            {
                result = hit.position;
                walkPointSet = true;
                return true;
            }
        }
        walkPointSet = false;
        return false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            EnemyProjectile ep = rb.gameObject.GetComponent<EnemyProjectile>();
            ep.baseDamage = baseDamage;
            ep.owner = enemyName;
            rb.velocity = transform.forward * shootForce;
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    protected override void UpdateStats()
    {
        enemyUI.UpdateLevelUI();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : AbstractEnemy
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

    [Header("Animations")]
    [SerializeField] public Animator anims;
    public float dampMovement;
    public float destroyTime;

    [Header("Audio")]
    AudioSource audioSource;
    [SerializeField] List<AudioClip> walkNoises;
    [SerializeField] AudioClip attackNoise;

    [Header("Colliders")]
    [SerializeField] MeshCollider meshCollider;
    [SerializeField] SphereCollider critCollider;

    //Attacking
    enum EnemyAttackType { MELEE, RANGE };
    [SerializeField] EnemyAttackType attackType;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] float meleeRange;
    bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] public bool playerInSightRange, playerInAttackRange;

    // Level
    BaseEnemyUI enemyUI;


    private void Start()
    {
        enemyUI = GetComponent<BaseEnemyUI>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        lootDrops = GetComponent<LootManager>();
    }

    private void Update()
    {
        //Check for sight and attack range
        if (playerInSightRange)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                playerInAttackRange = true;
            }
        }

        anims.SetFloat("Movement", Mathf.Clamp01(Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z)), dampMovement, Time.deltaTime);
        if (agent.velocity.sqrMagnitude > 0.1f && !audioSource.isPlaying) { audioSource.PlayOneShot(walkNoises[Random.Range(0, walkNoises.Count)]); }

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
        if(attackType == EnemyAttackType.MELEE)
        {
            if (Vector3.Distance(transform.position, player.position) < meleeRange)
            {
                if(Physics.Linecast(transform.position, player.position, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("Damagable"))
                    {
                        //Make sure enemy doesn't move
                        agent.SetDestination(transform.position);

                        if (!alreadyAttacked)
                        {
                            anims.Play("MeleeAttack", 0, 0f);
                            audioSource.PlayOneShot(attackNoise);
                            player.GetComponent<IDamageable>().Damage(baseDamage, enemyName);

                            alreadyAttacked = true;
                            Invoke(nameof(ResetAttack), timeBetweenAttacks);
                        }
                    }
                }
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }
        else if(attackType == EnemyAttackType.RANGE)
        {
            //Make sure enemy doesn't move
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                EnemyProjectile ep = rb.GetComponent<EnemyProjectile>();
                ep.baseDamage = baseDamage;
                ep.owner = enemyName;
                rb.velocity = transform.forward * shootForce;

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    public void Die()
    {
        agent.isStopped = true;
        agent.enabled = false;
        enabled = false;
        meshCollider.enabled = false;
        critCollider.enabled = false;
        Destroy(gameObject, destroyTime);
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

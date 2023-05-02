using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechBoss : AbstractEnemy
{
    [Header("Components")]
    NavMeshAgent agent;
    Animator animations;
    Transform player;
    BossHealth health;
    [SerializeField] List<AudioClip> walkNoises;
    AudioSource audioSource;
    [SerializeField] AudioSource attackSource;

    [Header("Movement")]
    [SerializeField] float walkPointRange;
    [SerializeField] LayerMask whatIsGround;
    public float dampMovement;
    Vector3 walkPoint;
    bool walkPointSet;

    [Header("Attacks")]
    [SerializeField] float dampRotateTime;
    [SerializeField] float sightRange;
    [SerializeField] LayerMask whatIsPlayer;

    [Header("STOMP ATTACK")]
    [SerializeField] float stompForce;                  // Stomp knockback to player
    [SerializeField] float meleeRange;                  // How close in order for stomp to activate
    [SerializeField] float meleeDamage;                 // How much damage stomp does
    [SerializeField] float stompTime;                   // Stomp cooldown time
    [SerializeField] AudioClip stompSound;
    bool stompAttack;

    [Header("RAPID ATTACK")]
    [SerializeField] GameObject rapidProjectile;        // Projectile spawned
    [SerializeField] float rapidFireDamage;             // Damage done by projectile
    [SerializeField] float rapidFireAmount;             // How many times to shoot
    [SerializeField] float rapidFireTimeBetween;        // Time between shots
    [SerializeField] float rapidFireTime;               // Attack cooldown
    [SerializeField] float rapidShootForce;             // Projectile speed
    [SerializeField] GameObject[] rapidSpawnPoint;      // Where the projectile spawns from
    [SerializeField] AudioClip rapidFireSound;
    bool rapidAttack;

    [Header("LASER ATTACK")]
    [SerializeField] float laserDamage;                 // How much damage laser does
    [SerializeField] GameObject laserProjectile;        // The laser spawned
    [SerializeField] float laserChargeTime;             // Laser charge time before firing
    [SerializeField] float laserTime;                   // Laser cooldown time
    [SerializeField] AudioClip laserSound;
    bool laserAttack;

    [Header("DASH ATTACK")]
    [SerializeField] float dashSpeed;
    [SerializeField] float ramDamage;
    [SerializeField] float dashTime;
    [SerializeField] CapsuleCollider dashCollider;
    [SerializeField] AudioClip dashSound;
    float maxDashTime = 3f;  // How long before giving up on the dash
    float dashTimer;    // The timer for the dash
    Vector3 dashDirection;
    bool dashAttack;

    public bool playerInSightRange;
    bool ableToAttack;
    bool attacking;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animations = GetComponent<Animator>();
        health = GetComponent<BossHealth>();
        audioSource = GetComponent<AudioSource>();

        agent.updateRotation = false;
        ableToAttack = true;
        health.enabled = true;
    }

    private void Update()
    {
        // Set the movement animation float
        if (!dashAttack)
        {
            animations.SetFloat("Movement", Mathf.Clamp01(Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z)), dampMovement, Time.deltaTime);
            if(agent.velocity.sqrMagnitude > 0.1f && !audioSource.isPlaying) { audioSource.PlayOneShot(walkNoises[Random.Range(0, walkNoises.Count)]); }
        }

        // Check if player is able to be seen and then attack
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        Collider[] hits = Physics.OverlapSphere(transform.position, sightRange);
        foreach (var hit in hits) {  if (hit.gameObject.layer == LayerMask.NameToLayer("Player")) { player = hit.gameObject.transform; } }

        // Actions
        if (agent.enabled)  // If the enemy is currently active (not dead)
        {
            if (!playerInSightRange) Patroling();
            else if (!playerInSightRange && attacking) MoveToPlayer();
            if (playerInSightRange) AttackPlayer();
        }
        
        if(health.GetHealth == 0) { agent.enabled = false; animations.SetBool("Dead", true); StopAllCoroutines(); }

        // Attack bools (I couldn't figure out how to do these in Coroutine)
        if (dashAttack) { DashAttack(); }

        // Agent look in the direction of their movement (destination)
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon && !attacking) transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    // ---------------------- MOVEMENT ----------------------
    /// <summary>
    /// Walk around to a random available point
    /// </summary>

    private void Patroling()
    {
        if (!walkPointSet) GetRandomPoint(transform.position, walkPointRange, out walkPoint);

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    /// <summary>
    /// Finds a random point
    /// </summary>
    void GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, 1))
            {
                result = hit.position;
                walkPointSet = true;
                return;
            }
        }
        GetRandomPoint(center, range, out result);
    }

    /// <summary>
    /// Moves the boss to the player position
    /// </summary>
    void MoveToPlayer()
    {
        agent.SetDestination(player.position);
        walkPointSet = true;
    }


    // ---------------------- ATTACKS ----------------------
    void AttackPlayer()
    {
        // Always look at the player when in the attack state (except when dashing forward)
        if (!dashAttack)
        {
            // Look at the player
            Vector3 look = player.position - transform.position;
            look.y = 0f;
            Quaternion rot = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * dampRotateTime);
        }

        // If the player is in the stomp range of the boss, do the stomp action
        if (Vector3.Distance(player.position, transform.position) <= meleeRange && !stompAttack && !dashAttack) { StompAttack(); }

        // If able to attack and not currently already attacking
        if (ableToAttack && !attacking)
        {
             walkPointSet = false;

            // Stop the enemy from moving
            agent.SetDestination(transform.position);

            // ------------------------------ Choose an attack to use ------------------------------

            // Choose a random attack
            int temp = Random.Range(1, 4);

            switch (temp)
            {
                case 1:     // Dash towards the player and damage them on hit
                    ableToAttack = false;
                    attacking = true;
                    dashAttack = true;
                    dashDirection = player.position;
                    dashDirection = new Vector3(dashDirection.x, transform.position.y, dashDirection.z);
                    dashCollider.enabled = true;
                    animations.Play("Ram");
                    attackSource.PlayOneShot(dashSound);
                    transform.LookAt(dashDirection);
                    break;
                case 2:     // Rapidly fire multiple shots at the player
                    ableToAttack = false;
                    attacking = true;
                    rapidAttack = true;
                    StartCoroutine(RapidAttack());
                    break;
                case 3:     // Charge a heavy beam for [3] seconds. Once done charging grab the latest player location and fire in that direction. Has small shield in front on it while charging
                    break;
                default:    // Something broke
                    Debug.LogError("Mech boss attack error");
                    break;
            }
        }

        // Not able to attack nor is currently attacking
        else if (!walkPointSet && !attacking)
        {
            // Do one of 2 actions: Walk around slightly or stand still
            int randomAction = Random.Range(1, 3);

            if (randomAction == 1)         // Walk around slightly
            {
                GetRandomPoint(transform.position, 3f, out walkPoint);
                agent.SetDestination(walkPoint);
                walkPointSet = true;
            }
            else { walkPointSet = true; }   // Don't move
        }
    }

    /// <summary>
    /// Cooldown for boss attacks
    /// </summary>
    IEnumerator AttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        ableToAttack = true;
    }

    /// <summary>
    /// Cooldown for stomp
    /// </summary>
    IEnumerator StompCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        stompAttack = false;
    }

    /// <summary>
    /// The dash attack
    /// </summary>
    void DashAttack()
    {
        transform.position = Vector3.MoveTowards(transform.position, dashDirection, dashSpeed * Time.deltaTime);

        if(dashTimer >= maxDashTime)
        {
            dashAttack = false;
            attacking = false;
            agent.enabled = true;
            dashCollider.enabled = false;
            dashTimer = 0f;

            StartCoroutine(AttackCooldown(dashTime));
        }
        else { dashTimer += Time.deltaTime; }

        if (Vector3.Distance(dashDirection, transform.position) < 1f)
        {
            dashAttack = false;
            attacking = false;
            agent.enabled = true;
            dashCollider.enabled = false;
            dashTimer = 0f;

            StartCoroutine(AttackCooldown(dashTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<IDamageable>().Damage(ramDamage * damageModifier, enemyName);
            dashCollider.enabled = false;
        }
    }

    /// <summary>
    /// The stomp attack
    /// </summary>
    void StompAttack()
    {
        stompAttack = true;
        animations.Play("Stomp");
        attackSource.PlayOneShot(stompSound);

        Collider[] hits = Physics.OverlapSphere(transform.position, sightRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                player.GetComponent<IDamageable>().Damage(meleeDamage * damageModifier, enemyName);
                player.GetComponent<Rigidbody>().AddForce(transform.forward * stompForce, ForceMode.Impulse);
            }
        }
        StartCoroutine(StompCooldown(stompTime));
    }

    /// <summary>
    /// The rapid fire attack
    /// </summary>
    IEnumerator RapidAttack()
    {
        for(int i = 0; i < rapidFireAmount; i++)
        {
            attackSource.PlayOneShot(rapidFireSound);
            GameObject temp = Instantiate(rapidProjectile);
            temp.transform.position = rapidSpawnPoint[Random.Range(0, rapidSpawnPoint.Length)].transform.position;
            EnemyProjectile ep = temp.GetComponent<EnemyProjectile>();
            ep.baseDamage = rapidFireDamage * damageModifier;
            ep.owner = enemyName;

            Vector3 direction = player.position - temp.transform.localPosition;
            temp.GetComponent<Rigidbody>().velocity = (direction / direction.magnitude)  * rapidShootForce;

            yield return new WaitForSeconds(rapidFireTimeBetween);
        }

        attacking = false;
        rapidAttack = false;

        StartCoroutine(AttackCooldown(rapidFireTime));
    }
}

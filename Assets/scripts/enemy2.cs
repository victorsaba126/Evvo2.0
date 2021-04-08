using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy2 : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase}
    public State state;

    private NavMeshAgent agent;
    private Animator animator;
    private PlayerController player;
    private TriggerSFX sfx;
    public LayerMask whatIsGround, whatIsPlayer;

    
    //type of enemy and health
    public bool semiboss;
    public bool dead = false;
    public float health;


    //states
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange, Soundon;


    //GO animations 
    Vector3 playerLook;
    public GameObject enemySound;
    public GameObject bossSound;
    public GameObject portal;
    public GameObject soul;
    private Puerta_final anim;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private Vector3 PosProjectile;
    public float yProject = 0.6f;


    [Header("Patrol")]
    public float timeStopped = 1;
    private float timeCounter = 0;
    public Transform[] nodes;
    private int currentNode = 0;
    private bool nearNode = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>();

        SetIdle();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (agent.velocity.normalized != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
        switch (state)
        {
            case State.Idle:
                {
                    IdleUpdate();
                    break;
                }
            case State.Patrol:
                {
                    PatrolUpdate();
                    break;
                }
            case State.Chase:
                {
                    ChaseUpdate();
                    break;
                }
            

        }
    }

    private void IdleUpdate()
    {
        if (playerInSightRange)
        {
            enemySound.SetActive(true);
            SetChase();
            return;
        }

        timeCounter += Time.deltaTime;


        if (timeCounter >= timeStopped)
        {
            if (nodes[currentNode] == null)
            {
                return;
            }
            if (!nearNode)
                GoNextNode();

            else
                GoNearNode();
            SetPatrol();
        }
    }

    private void PatrolUpdate()
    {
        if (playerInSightRange)
        {
            SetChase();
            enemySound.SetActive(true);
            return;
        }

        if (agent.remainingDistance <= 0.1f)
        {
            SetIdle();
        }
    }

    private void ChaseUpdate()
    {
        if (!playerInSightRange)
        {
            enemySound.SetActive(false);
            SetIdle();
            nearNode = true;
            return;

        }

        agent.SetDestination(player.transform.position);
        if (playerInAttackRange)
        {
            AttackPlayer();
        }


    }

    private void SetIdle()
    {
        agent.isStopped = true;
        timeCounter = 0;
        state = State.Idle;
    }

    private void SetPatrol()
    {
        agent.isStopped = false;

        state = State.Patrol;
    }

    private void SetChase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        state = State.Chase;
    }

    private void GoNextNode()
    {
        currentNode++;

        if (currentNode >= nodes.Length)
        {
            currentNode = 0;
        }

        agent.SetDestination(nodes[currentNode].position);
        if (agent.velocity.normalized != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    private void GoNearNode()
    {
        nearNode = false;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < nodes.Length; i++)
        {
            if (minDistance < Vector3.Distance(transform.position, nodes[i].position))
            {
                minDistance = Vector3.Distance(transform.position, nodes[i].position);
                currentNode = i;
            }
        }

        agent.SetDestination(nodes[currentNode].position);
        if (agent.velocity.normalized != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        playerLook = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerLook);

        if (!alreadyAttacked)
        {
            //Attack code here
            PosProjectile = new Vector3(transform.position.x, transform.position.y + yProject, transform.position.z);
            Rigidbody rb = Instantiate(projectile, PosProjectile + (transform.forward * 1.2f), Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        if (semiboss)
        {
            portal.SetActive(true);
            soul.SetActive(true);
            dead = true;
            anim.puertaFinal();
            sfx.SemibossDead();
        }
        else
        {

        }
        Destroy(gameObject);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            Debug.Log("Me ha dado");
            TakeDamage(1);
        }
    }

    private void OnDrawGiszmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);


    }


 }
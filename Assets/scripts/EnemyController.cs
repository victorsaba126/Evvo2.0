using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;

    public PlayerController player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public bool semiboss;
    public bool dead = false;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private Vector3 PosProjectile;
    public float yProject;
    

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, Soundon;
    Vector3 playerLook;
    private float timeCounter=0;
    public GameObject enemySound;
    public GameObject bossSound;
    public GameObject portal;
    public GameObject soul;

    private TriggerSFX sfx;
    private Puerta_final anim;

    private void Awake()
    {
       
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        sfx = FindObjectOfType<TriggerSFX>();
        anim = FindObjectOfType<Puerta_final>();
        timeCounter = 0;
    }

    private void Update()
    {
        //Debug.Log("enemy " + transform.position.y + ", player " + player.transform.position.y);
        //check vision y rango de ataque
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            Soundon = false;
            timeCounter = 0;
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            Soundon = true;
            timeCounter++;
        }
        if (playerInSightRange && playerInAttackRange )
        {

            AttackPlayer();
        }

        if (Soundon && timeCounter == 1 && semiboss == false)
        {
            Instantiate(enemySound);
        }
        if (Soundon && timeCounter == 1 && semiboss == true)
        {
            bossSound.SetActive(true);
        }

    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            if (agent.velocity.normalized != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            }
            


            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //WalkPoint reached
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }

    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

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
            Rigidbody rb = Instantiate(projectile, PosProjectile+(transform.forward*1.2f), Quaternion.identity).GetComponent<Rigidbody>();

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
        if(other.tag == "Attack")
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

    // Update is called once per frame
   
}

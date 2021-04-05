using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum enemyState { trebleMode, bassMode };

    public enemyState currentState;

    public NavMeshAgent agent;

    public Transform playerTransform;

    public LayerMask whatIsGround;

    public LayerMask whatIsPlayer;

    public Vector3 walkPoint;

    bool hasWalkPoint;

    public float walkPointRange;

    public float timeBetweenAttacks;

    bool hasAttacked;

    public float sightRange;
    public float attackRange;

    public bool playerInSightRange;
    public bool playerInAttackRange;

    public GameObject enemyProjectilePrefab;

    public float enemyHealth = 3;

    // Start is called before the first frame update
    void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        float randomNumber = Random.Range(0f, 2f);
        Debug.Log(randomNumber);
        if (randomNumber > 1f)
        {
            currentState = enemyState.trebleMode;
        }
        else
        {
            currentState = enemyState.bassMode;
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrol();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            Attack();
        }

        if (currentState == enemyState.trebleMode)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (currentState == enemyState.bassMode)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Patrol()
    {
        if (!hasWalkPoint)
        {
            SearchForWalkPoint();
        }
        if (hasWalkPoint)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            hasWalkPoint = false;
        }
    }

    void SearchForWalkPoint()
    {
        float randomXCoordinate = Random.Range(-walkPointRange, walkPointRange);
        float randomYCoordinate = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomXCoordinate, transform.position.z + randomYCoordinate);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            hasWalkPoint = true;
        }
    }

    void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);
        if (!hasAttacked)
        {
            GameObject projectile = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Impulse);
            Destroy(projectile, 2f);
            hasAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        hasAttacked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot1" && currentState == enemyState.trebleMode)
        {
            enemyHealth -= 1;
        }
        if (collision.gameObject.tag == "Shot2" && currentState == enemyState.bassMode)
        {
            enemyHealth -= 1;
        }
    }
}

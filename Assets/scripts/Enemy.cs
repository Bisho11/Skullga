using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 1;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    GameObject player;
    Animator animator;
    NavMeshAgent agent;
    float timePassed;
    float newDestinationCD = 0.5f;
    bool dead;


    void Start()
    {
        dead = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        if (health > 0)
        {
            animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

            if (timePassed >= attackCD)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    animator.SetTrigger("attack");
                    timePassed = 0;
                }
            }
            timePassed += Time.deltaTime;

            if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
            }
            newDestinationCD -= Time.deltaTime;
            transform.LookAt(player.transform);
        }
    }

    

    public void TakeDamage(float damageAmount)
    {
        
        health -= damageAmount;
        if (health > 0)
        {
            animator.SetTrigger("damage");
        }

        else if (!dead)
        {

            animator.SetTrigger("death");
            dead = true;
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;

    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;

    Transform target;
    Animator animator;
    NavMeshAgent navMeshAgent;
    EnemyHealth enemyHealth;
    PlayerStealth playerStealth;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        playerStealth = FindObjectOfType<PlayerStealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStealth.isStealth) 
        {
            isProvoked = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);        

        if (isProvoked)
        {
            EngageTarget();
        }
        if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        else 
        {
            isProvoked = false;
            animator.SetBool("Attack", false);
            animator.SetBool("Move", false);
        }
    }

    void ChaseTarget()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Move", true);
        navMeshAgent.SetDestination(target.position);
    }

    void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
            
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();

        }
    }

    void AttackTarget()
    {
        animator.SetBool("Attack", true);
        Debug.Log(name + "has seeked and is attacking" + target.name);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void DamageTaken()
    {
        isProvoked = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, navMeshAgent.stoppingDistance);

    }
}

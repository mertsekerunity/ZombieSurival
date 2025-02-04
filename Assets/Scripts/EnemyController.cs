using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;

    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;
    bool isAttacking = false;

    Animator animator;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator.SetFloat("MoveSpeed", 0);
    }

    // Update is called once per frame
    void Update()
    {   isAttacking = false;
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        else 
        {
            animator.SetTrigger("Reset");
            animator.SetFloat("MoveSpeed", 0);
        }
    }

    void ChaseTarget()
    {
        animator.SetFloat("MoveSpeed", 1);
        navMeshAgent.SetDestination(target.position);
    }

    void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
            
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
            isAttacking = true;
        }
    }

    void AttackTarget()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("Attack");
        }
        //isAttacking = false;
        Debug.Log(name + "has seeked and is attacking" + target.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

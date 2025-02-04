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
    {
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
        if (!isAttacking)
        {
            animator.SetFloat("MoveSpeed", 1);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }
    }

    void EngageTarget()
    {
        if (!isAttacking)
        {
            if (distanceToTarget >= navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }
            else if (distanceToTarget <= navMeshAgent.stoppingDistance)
            {
                StartCoroutine(AttackTarget());
            }
        }
    }

    IEnumerator AttackTarget()
    {
        isAttacking = true;
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Attack");

        Debug.Log(name + " has seeked and is attacking " + target.name);

        yield return new WaitForSeconds(1.5f);

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            isAttacking = false;
            navMeshAgent.isStopped = false;
            ChaseTarget();
        }
        else
        {
            isAttacking = false;
        }
        //Debug.Log(name + "has seeked and is attacking" + target.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

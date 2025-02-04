using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    Animator animator;
    float destroyDelay = 2f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            animator.SetTrigger("Dead");
            GetComponent<NavMeshAgent>().enabled = false;
            Destroy(gameObject, destroyDelay);
        }
    }
}

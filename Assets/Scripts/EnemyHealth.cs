using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    Animator animator;
    float destroyDelay = 1.5f;
    bool isDead = false;

    public bool IsDead() {  return isDead; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("DamageTaken");

        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Death();
            Destroy(gameObject, destroyDelay);
        }
    }

    void Death()
    {
        if (isDead) { return; }
        isDead = true;
        GetComponent<NavMeshAgent>().enabled = false;
        animator.SetTrigger("Dead");
    }
}

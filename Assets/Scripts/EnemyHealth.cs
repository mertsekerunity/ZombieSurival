using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    Animator animator;
    float destroyDelay = 1f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            animator.SetFloat("MoveSpeed", 0);
            animator.SetTrigger("Dead");
            //Destroy(gameObject, destroyDelay);
        }
    }
}

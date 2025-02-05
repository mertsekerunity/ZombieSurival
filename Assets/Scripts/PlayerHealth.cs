using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}

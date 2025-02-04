using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    void ProcessRaycast()
    {
        RaycastHit hit;

        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("I hit" + hit.transform.name);
            PlayHitEffect(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if(target==null) { return; }

            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void PlayHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(impact, 0.05f);
    }
}

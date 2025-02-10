using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoUsableText;
    [SerializeField] TextMeshProUGUI ammoReservedText;

    PlayerStealth playerStealth;

    public WeaponType weaponType;
    public bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true; 
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStealth = FindObjectOfType<PlayerStealth>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        if (weaponType == WeaponType.Rifles)
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        if (weaponType == WeaponType.Shotguns || weaponType == WeaponType.Pistols)
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            canShoot = false;
            ammoSlot.ReloadWeapon(ammoType);
            canShoot = true;
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        if (ammoSlot.GetCurrentUsableAmmo(ammoType) > 0)
        {
            playerStealth.isStealth = false;
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("I hit" + hit.transform.name);
            PlayHitEffect(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target == null) { return; }

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

    void DisplayAmmo()
    {
        int currentUsableAmmo = ammoSlot.GetCurrentUsableAmmo(ammoType);
        int currentReservedAmmo = ammoSlot.GetCurrentReservedAmmo(ammoType);

        ammoUsableText.text = currentUsableAmmo.ToString();
        ammoReservedText.text = currentReservedAmmo.ToString();
    }
}

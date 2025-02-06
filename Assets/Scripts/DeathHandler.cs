using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] FirstPersonController firstPersonController;

    Weapon weapon;
    WeaponSwitcher weaponSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
        weapon = FindObjectOfType<Weapon>();
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled=true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        firstPersonController.enabled = false;
        weapon.canShoot = false;
        weaponSwitcher.enabled = false;
    }
}

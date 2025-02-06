using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 30f;
    [SerializeField] float addIntensity = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(restoreAngle);
            other.GetComponentInChildren<Flashlight>().AddLightIntensity(addIntensity);
            Destroy(gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]

    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoUsableAmount;
        public int ammoReservedAmount;
    }

    public int GetCurrentUsableAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoUsableAmount;
    }

    public int GetCurrentReservedAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoReservedAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoUsableAmount--;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

    public void ReloadWeapon(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoUsableAmount += GetAmmoSlot(ammoType).ammoReservedAmount;
        GetAmmoSlot(ammoType).ammoReservedAmount = 0;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoReservedAmount += ammoAmount;
    }
}

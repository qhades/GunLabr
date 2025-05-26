using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class WeaponReloadedEvent : MonoBehaviour
{
    public event Action<WeaponReloadedEvent, WeaponReloaedEventArgs> OnWeaponReloaded;

    public void CallWeaponReloadedEvent(Weapon weapon)
    {
        OnWeaponReloaded?.Invoke(this, new WeaponReloaedEventArgs() { weapon = weapon });
    }
}

public class WeaponReloaedEventArgs : EventArgs
{
    public Weapon weapon;
}

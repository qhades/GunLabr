using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class WeaponFireEvent : MonoBehaviour
{
    public event Action<WeaponFireEvent, WeaponFireEventArgs> OnWeaponFired;

    public void CallWeaponFiredEvent(Weapon weapon)
    {
        OnWeaponFired?.Invoke(this, new WeaponFireEventArgs() { weapon = weapon });
    }
}

public class WeaponFireEventArgs : EventArgs
    {
        public Weapon weapon;
    }

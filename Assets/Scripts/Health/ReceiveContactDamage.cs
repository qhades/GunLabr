using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ReceiveContactDamage : MonoBehaviour
{
    #region Header
    [Header("The contact damage amount to receive")]
    #endregion
    [SerializeField] private int contactDamageAmount;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void TakeContactDamage(int damageAmonut = 0)
    {
        if (contactDamageAmount > 0)
            damageAmonut = contactDamageAmount;

        health.TakeDamage(damageAmonut);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckPositiveValue(this, nameof(contactDamageAmount), contactDamageAmount, true);
    }
#endif
    #endregion
}
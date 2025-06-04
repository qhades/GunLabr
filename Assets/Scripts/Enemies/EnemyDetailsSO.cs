using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/Enemy Detail")]
public class EnemyDetailsSO : ScriptableObject
{
    #region Header BASE ENEMY DETAILS
    [Space(10)]
    [Header("BASE ENEMY DETAILS")]
    #endregion
    public string enemyName;

    public GameObject enemyPrefab;

    public float chaseDistance = 50f;

    #region Header ENEMY WEAPON SETTING
    [Space(10)]
    [Header("ENEMY WEAPON SETTING")]
    #endregion
    public WeaponDetailsSO enemyWeapon;
    public float firingIntervalMin = 0.1f;
    public float firingIntervalMax = 1f;
    public float firingDurationMin = 1f;
    public float firingDurationMax = 2f;
    public bool firingLineOfSignRequired;

    #region Header ENEMY HEALTH
    [Space(10)]
    [Header("ENEMY HEALTH")]
    #endregion
    public EnemyHealthDetails[] enemyHealthDetailsArray;
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {        
        HelperUltilities.ValidateCheckEmptyString(this, nameof(enemyName), enemyName);
        HelperUltilities.ValidateCheckNullValue(this, nameof(enemyPrefab), enemyPrefab);
        HelperUltilities.ValidateCheckPositiveValue(this, nameof(chaseDistance), chaseDistance, false);

        HelperUltilities.ValidateCheckPositiveRange(this, nameof(firingDurationMin), firingDurationMin, nameof(firingDurationMax), firingDurationMax, false);
        HelperUltilities.ValidateCheckPositiveRange(this, nameof(firingIntervalMin), firingIntervalMin, nameof(firingIntervalMax), firingIntervalMax, false);

        if (isImmuneAfterHit)
        {
            HelperUltilities.ValidateCheckPositiveValue(this, nameof(hitImmunityTime), hitImmunityTime, false);
        }
    }
#endif
    #endregion
}

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

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckNullValue(this, nameof(enemyPrefab), enemyPrefab);
        HelperUltilities.ValidateCheckEmptyString(this, nameof(enemyName), enemyName);
    }
#endif
    #endregion
}

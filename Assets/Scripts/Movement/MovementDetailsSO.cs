using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "Scriptable Objects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{
    #region Header MOVEMENT DETAILS
    [Space(10)]
    [Header("MOVEMENT DETAILS")]
    #endregion
    public float minMoveSpeed = 8f;
    public float maxMoveSpeed = 8f;
    public float rollSpeed;
    public float rollDistance;
    public float rollCoolDownTime;

    public float GetMoveSpeed()
    {
        if (minMoveSpeed == maxMoveSpeed)
        {
            return minMoveSpeed;
        }
        else
        {
            return Random.Range(minMoveSpeed, maxMoveSpeed);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckPositiveRange(this, nameof(minMoveSpeed), minMoveSpeed, nameof(maxMoveSpeed), maxMoveSpeed, false);

        if (rollDistance != 0f || rollSpeed != 0f || rollCoolDownTime != 0)
        {
            HelperUltilities.ValidateCheckPositiveValue(this, nameof(rollDistance), rollDistance, false);
            HelperUltilities.ValidateCheckPositiveValue(this, nameof(rollSpeed), rollSpeed, false);
            HelperUltilities.ValidateCheckPositiveValue(this, nameof(rollCoolDownTime), rollCoolDownTime, false);
        }
    }
#endif
    #endregion
}

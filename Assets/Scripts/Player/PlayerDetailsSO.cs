using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    #region Header PLAYER BASE DETAILS
    [Space(10)]
    [Header("PLAYER BASE DETAILS")]
    #endregion
    public string playerCharacterName;
    public GameObject playerPrefab;
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header PLAYER HEALTH
    [Space(10)]
    [Header("PLAYER HEALTH")]
    #endregion
    public int playerHealthAmount;

    #region Header WEAPON
    [Space(10)]
    [Header("WEAPON")]
    #endregion
    public WeaponDetailsSO startingWeapon;
    public List<WeaponDetailsSO> startingWeaponList;

    #region Header OTHERS
    [Space(10)]
    [Header("OTHERS STATS")]
    #endregion
    public Sprite playerMiniMapIcon;
    public Sprite playerHandsprite;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUltilities.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUltilities.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);
        HelperUltilities.ValidateCheckNullValue(this, nameof(startingWeapon), startingWeapon);
        HelperUltilities.ValidateCheckNullValue(this, nameof(playerMiniMapIcon), playerMiniMapIcon);
        HelperUltilities.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
        HelperUltilities.ValidateCheckNullValue(this, nameof(playerHandsprite), playerHandsprite);
        HelperUltilities.ValidateCheckEnumarableValues(this, nameof(startingWeaponList), startingWeaponList);
    }
#endif
    #endregion

}

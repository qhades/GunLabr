using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Audio;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    #region DUNGEON
    [Space(10)]
    [Header("DUNGEON")]
    #endregion
    public RoomNodeTypeListSO roomNodeTypeList;

    #region PLAYER SELECTION
    [Space(10)]
    [Header("PLAYER SELECTION")]
    #endregion
    public GameObject playerSelectionPrefab;

    #region Header PLAYER
    [Space(10)]
    [Header("PLAYER")]
    #endregion
    public List<PlayerDetailsSO> playerDetailsList;
    public CurrentPlayerSO currentPlayer;

    #region Header MUSIC
    [Space(10)]
    [Header("MUSIC")]
    #endregion
    public AudioMixerGroup musicMasterMixerGroup;
    public AudioMixerSnapshot musicOnFullSnapshot;
    public AudioMixerSnapshot musicLowSnapshot;
    public AudioMixerSnapshot musicOffSnapshot;

    #region MATERIALS
    [Space(10)]
    [Header("MATERIALS")]
    #endregion
    public Material dimmedMaterial;

    public Material litMaterial;
    public Shader variableLitShader;

    #region Header UI
    [Space(10)]
    [Header("UI")]
    #endregion
    public GameObject ammoIconPrefab;
    public GameObject heartPrefab;

    #region Header SPECIAL TILEMAP TILES
    [Space(10)]
    [Header("SPECIAL TILEMAP TILES")]
    #endregion
    public TileBase[] enemyUnwalkableCollisionTilesArray;
    public TileBase preferredEnemyPathTile;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckNullValue(this, nameof(roomNodeTypeList), roomNodeTypeList);
        HelperUltilities.ValidateCheckNullValue(this, nameof(playerSelectionPrefab), playerSelectionPrefab);
        HelperUltilities.ValidateCheckEnumarableValues(this, nameof(playerDetailsList), playerDetailsList);
        HelperUltilities.ValidateCheckNullValue(this, nameof(currentPlayer), currentPlayer);
        HelperUltilities.ValidateCheckNullValue(this, nameof(litMaterial), litMaterial);
        HelperUltilities.ValidateCheckNullValue(this, nameof(dimmedMaterial), dimmedMaterial);
        HelperUltilities.ValidateCheckNullValue(this, nameof(variableLitShader), variableLitShader);
        HelperUltilities.ValidateCheckNullValue(this, nameof(ammoIconPrefab), ammoIconPrefab);
        HelperUltilities.ValidateCheckNullValue(this, nameof(preferredEnemyPathTile), preferredEnemyPathTile);
        HelperUltilities.ValidateCheckNullValue(this, nameof(musicMasterMixerGroup), musicMasterMixerGroup);
        HelperUltilities.ValidateCheckNullValue(this, nameof(musicOnFullSnapshot), musicOnFullSnapshot);
        HelperUltilities.ValidateCheckNullValue(this, nameof(musicLowSnapshot), musicLowSnapshot);
        HelperUltilities.ValidateCheckNullValue(this, nameof(musicOffSnapshot), musicOffSnapshot);
        HelperUltilities.ValidateCheckEnumarableValues(this, nameof(enemyUnwalkableCollisionTilesArray), enemyUnwalkableCollisionTilesArray);
    }
#endif
    #endregion
}

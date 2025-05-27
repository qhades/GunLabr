using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Dungeon/Room")]
public class RoomTemplateSO : ScriptableObject
{
    [HideInInspector] public string guid;

    #region Header ROOM PREFAB
    [Space(10)]
    [Header("ROOM PREFAB")]
    #endregion Header ROOM PREFAB

    public GameObject prefab;

    [HideInInspector] public GameObject previousPrefab; // this is used to regenerate the guid if the so is copied and the prefab is changed


    #region Header ROOM CONFIGURATION
    [Space(10)]
    [Header("ROOM CONFIGURATION")]
    #endregion Header ROOM CONFIGURATION

    public RoomNodeTypeSO roomNodeType;  

    public Vector2Int lowerBounds;
    
    public Vector2Int upperBounds;

    [SerializeField] public List<Doorway> doorwayList;

    public Vector2Int[] spawnPositionArray;

    #region Header ENEMY DETAILS
    [Space(10)]
    [Header("ENEMY DETAILS")]
    #endregion
    public List<SpawnableObjectByLevel<EnemyDetailsSO>> enemiesByLevelList;
    public List<RoomEnemySpawnParameters> roomEnemySpawnParametersList;

    /// <summary>
    /// Returns the list of Entrances for the room template
    /// </summary>
    public List<Doorway> GetDoorwayList()
    {
        return doorwayList;
    }

    #region Validation

#if UNITY_EDITOR

    // Validate SO fields
    private void OnValidate()
    {
        // Set unique GUID if empty or the prefab changes
        if (guid == "" || previousPrefab != prefab)
        {
            guid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }

        HelperUltilities.ValidateCheckNullValue(this, nameof(prefab), prefab);
        HelperUltilities.ValidateCheckNullValue(this, nameof(roomNodeType), roomNodeType);

        HelperUltilities.ValidateCheckEnumarableValues(this, nameof(doorwayList), doorwayList);

        if (enemiesByLevelList.Count > 0 || roomEnemySpawnParametersList.Count > 0)
        {
            HelperUltilities.ValidateCheckEnumarableValues(this, nameof(enemiesByLevelList), enemiesByLevelList);
            HelperUltilities.ValidateCheckEnumarableValues(this, nameof(roomEnemySpawnParametersList), roomEnemySpawnParametersList);

            foreach(RoomEnemySpawnParameters roomEnemySpawnParameters in roomEnemySpawnParametersList)
            {
                HelperUltilities.ValidateCheckNullValue(this, nameof(roomEnemySpawnParameters.dungeonLevel), roomEnemySpawnParameters.dungeonLevel);
                HelperUltilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minTotalEnemiesToSpawn), roomEnemySpawnParameters.minTotalEnemiesToSpawn, nameof(roomEnemySpawnParameters.maxTotalEnemiesToSpawn), roomEnemySpawnParameters.maxTotalEnemiesToSpawn, true);
                HelperUltilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minConcurrentEnemies), roomEnemySpawnParameters.minConcurrentEnemies, nameof(roomEnemySpawnParameters.maxConcurrentEnemies), roomEnemySpawnParameters.maxConcurrentEnemies, false);
                HelperUltilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minSpawnInterval), roomEnemySpawnParameters.minSpawnInterval, nameof(roomEnemySpawnParameters.maxSpawnInterval), roomEnemySpawnParameters.maxSpawnInterval, true);

                bool isEnemyTypesListForDungeonLevel = false;

                foreach(SpawnableObjectByLevel<EnemyDetailsSO> dungeonObjectBylevel in enemiesByLevelList)
                {
                    if (dungeonObjectBylevel.dungeonLevel == roomEnemySpawnParameters.dungeonLevel && dungeonObjectBylevel.spawnableObjectRatioList.Count > 0)
                    {
                        isEnemyTypesListForDungeonLevel = true;

                        HelperUltilities.ValidateCheckNullValue(this, nameof(dungeonObjectBylevel.dungeonLevel), dungeonObjectBylevel.dungeonLevel);

                        foreach(SpawnableObjectRatio<EnemyDetailsSO> dungeonObjectRatio in dungeonObjectBylevel.spawnableObjectRatioList)
                        {
                            HelperUltilities.ValidateCheckNullValue(this, nameof(dungeonObjectRatio.dungeonObject), dungeonObjectRatio.dungeonObject);
                            HelperUltilities.ValidateCheckPositiveValue(this, nameof(dungeonObjectRatio.ratio), dungeonObjectRatio.ratio, false);
                        }
                    }
                }

                if (isEnemyTypesListForDungeonLevel == false && roomEnemySpawnParameters.dungeonLevel != null)
                {
                    Debug.Log("No enemy types specified in for dungeon level" + roomEnemySpawnParameters.dungeonLevel.levelName + " in gameobject " + this.name.ToString());
                }

            }
        }

        // Check spawn positions populated
        HelperUltilities.ValidateCheckEnumarableValues(this, nameof(spawnPositionArray), spawnPositionArray);
    }

#endif

    #endregion Validation
}
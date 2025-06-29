using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName = "Scriptable Objects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    #region Header
    [Header("Only flag RoomNodeTypes that should be visible in the editor")]
    #endregion Header
    public bool displayInNodeGraphEditor = true;
    #region Header
    [Header("One type should be a Corridor")]
    #endregion Header
    public bool isCorridor;
    #region Header
    [Header("One type should be a CorridorNS")]
    #endregion Header
    public bool isCorridorNS;
    #region Header
    [Header("One type should be a CorridorEW")]
    #endregion Header
    public bool isCorridorEW;
    #region Header
    [Header("One type should be a Entrance")]
    #endregion Header
    public bool isEntrance;
    #region Header
    [Header("One type should be a Boss Room")]
    #endregion Header
    public bool isBossRoom;
    #region Header
    [Header("One type should be None (Unassigned)")]
    #endregion Header
    public bool isNone;

    #region Validate
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
    }
#endif
    #endregion
}

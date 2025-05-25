using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarTest : MonoBehaviour
{
    private InstantiatedRoom instantiatedRoom;
    private Grid grid;
    private Tilemap frontTilemap;
    private Tilemap pathtilemap;
    private Vector3Int startGridPosition;
    private Vector3Int endGridPosition;
    private TileBase startPathTile;
    private TileBase finishPathTile;

    private Vector3Int noValue = new Vector3Int(9999, 9999, 9999);
    private Stack<Vector3> pathStack;

    private void OnEnable()
    {
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
    }

    private void Start()
    {
        startPathTile = GameResources.Instance.preferredEnemyPathTile;
        finishPathTile = GameResources.Instance.enemyUnwalkableCollisionTilesArray[0];
    }

    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        pathStack = null;
        instantiatedRoom = roomChangedEventArgs.room.instantiatedRoom;
        frontTilemap = instantiatedRoom.transform.Find("Grid/Tilemap4_Front").GetComponent<Tilemap>();
        grid = instantiatedRoom.transform.GetComponentInChildren<Grid>();
        startGridPosition = noValue;
        endGridPosition = noValue;

        SetUpPathTilemap();
    }

    private void SetUpPathTilemap()
    {
        Transform tilemapCloneTransform = instantiatedRoom.transform.Find("Grid/Tilemap4_Front(Clone)");

        if (tilemapCloneTransform == null)
        {
            pathtilemap = Instantiate(frontTilemap, grid.transform);
            pathtilemap.GetComponent<TilemapRenderer>().sortingOrder = 2;
            pathtilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.litMaterial;
            pathtilemap.gameObject.tag = "Untagged";
        }
        else
        {
            pathtilemap = instantiatedRoom.transform.Find("Grid/Tilemap4_Front(Clone)").GetComponent<Tilemap>();
            pathtilemap.ClearAllTiles();
        }
    }

    private void Update()
    {
        if (instantiatedRoom == null || startPathTile == null || finishPathTile == null || grid == null || pathtilemap == null) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            ClearPath();
            SetStartPosition();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ClearPath();
            SetEndPosition();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DisplayPath();
        }
    }

    private void SetStartPosition()
    {
        if (startGridPosition == noValue)
        {
            startGridPosition = grid.WorldToCell(HelperUltilities.GetMouseWorldPosition());

            if (!IsPositionWithinBounds(startGridPosition))
            {
                startGridPosition = noValue;
                return;
            }

            pathtilemap.SetTile(startGridPosition, startPathTile);
        }
        else
        {
            pathtilemap.SetTile(startGridPosition, null);
            startGridPosition = noValue;
        }
    }

    private void SetEndPosition()
    {
        if (endGridPosition == noValue)
        {
            endGridPosition = grid.WorldToCell(HelperUltilities.GetMouseWorldPosition());

            if (!IsPositionWithinBounds(endGridPosition))
            {
                endGridPosition = noValue;
                return;
            }

            pathtilemap.SetTile(endGridPosition, finishPathTile);
        }
        else
        {
            pathtilemap.SetTile(endGridPosition, null);
            endGridPosition = noValue;
        }
    }

    private bool IsPositionWithinBounds(Vector3Int position)
    {
        if (position.x < instantiatedRoom.room.templateLowerBounds.x || position.x > instantiatedRoom.room.templateUpperBounds.x || position.y < instantiatedRoom.room.templateLowerBounds.y || position.y > instantiatedRoom.room.templateUpperBounds.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ClearPath()
    {
        if (pathStack == null)
            return; 

        foreach (Vector3 worldPosition in pathStack)
        {
            pathtilemap.SetTile(grid.WorldToCell(worldPosition), null);
        }

        pathStack = null;

        endGridPosition = noValue;
        startGridPosition = noValue;
    }

    private void DisplayPath()
    {
        if (startGridPosition == noValue || endGridPosition == noValue) return;

        pathStack = AStar.BuildPath(instantiatedRoom.room, startGridPosition, endGridPosition);

        if (pathStack == null) return;

        foreach(Vector3 worldPosition in pathStack)
        {
            pathtilemap.SetTile(grid.WorldToCell(worldPosition), startPathTile);
        }
    }
}

using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;

public class RoomNodeGraphEditor : EditorWindow
{
    private GUIStyle roomNodeStyle;
    private static RoomNodeGraphSO currentRoomNodeGraph;
    private RoomNodeTypeListSO roomNodeTypeList;

    //Node layout value
    private const float nodeHeight = 75f;
    private const float nodeWidth = 160f;
    private const int nodePadding = 25;
    private const int nodeBorder = 12;

   [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
   private static void OpenWindow()
    {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
    }

    private void OnEnable()
    {
        //Define node layout style
        roomNodeStyle = new GUIStyle();
        roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        roomNodeStyle.normal.textColor = Color.white;
        roomNodeStyle.padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding);
        roomNodeStyle.border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder);

        //Load Room Node Type List
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    /// <summary>
    /// Open Room node graph editor window if a room node graph scriptable object asset is double clicked in the inspector
    /// </summary>

    [OnOpenAsset(0)] // Need the namespace UnityEditor.Callbacks

    public static bool OnDoubleClickAssets(int instanceID, int line)
    {
        RoomNodeGraphSO roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;

        if (roomNodeGraph != null)
        {
            OpenWindow();

            currentRoomNodeGraph = roomNodeGraph;

            return true;
        }
        return false;
    }

    /// <summary>
    /// Draw Editor GUI
    /// </summary>
    private void OnGUI()
    {
        //If a scriptable object of type RoomNodeGraphSO has been selected then process
        if (currentRoomNodeGraph != null)
        {
            // Process Events
            ProcessEvents(Event.current);

            // Draw Room Nodes
            DrawRoomNodes();
        }

        if (GUI.changed)
            Repaint();
    }

    private void ProcessEvents(Event currentEvent)
    {
        ProcessRoomNodeGraphEvents(currentEvent);
    }

    /// <summary>
    /// Process Room Node Graph Event
    /// </summary>
    private void ProcessRoomNodeGraphEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            //Process mouse down event
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Process Mouse Down Event on Room Node Graph (not over the node)
    /// </summary>
    private void ProcessMouseDownEvent(Event currentEvent)
    {
        //Process Right Click mouse down on graph event (show context menu)
        if (currentEvent.button == 1)
        {
            ShowContextMenu(currentEvent.mousePosition);
        }
    }
    
    /// <summary>
    /// Show the context menu
    /// </summary>
    private void ShowContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);

        menu.ShowAsContext();
    }

    /// <summary>
    /// Create room node at mouse position
    /// </summary>
    private void CreateRoomNode(object mousePositionObject)
    {
        CreateRoomNode(mousePositionObject, roomNodeTypeList.list.Find(x => x.isNone));
    }
    
    /// <summary>
    /// Create a room node at mouse position - overload to also pass in RoomNodeType
    /// </summary>
    private void CreateRoomNode(object mousePositionObject, RoomNodeTypeSO roomNodeType)
    {
        Vector2 mousePosition = (Vector2)mousePositionObject;

        // Create room node scriptable object asset
        RoomNodeSO roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();

        // add room node to current room node graph node list
        currentRoomNodeGraph.roomNodeList.Add(roomNode);

        //set node values
        roomNode.Initialise(new Rect(mousePosition, new Vector2(nodeWidth, nodeHeight)), currentRoomNodeGraph, roomNodeType);

        //add room node to room node graph scriptable object asset database
        AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);

        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// Draw room node in the graph window
    /// </summary>
     private void DrawRoomNodes()
    {
        // Loop through all room nodes and draw them
        foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
        {
            roomNode.Draw(roomNodeStyle);
        }
        GUI.changed = true;
    }
}

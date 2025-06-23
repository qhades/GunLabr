using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    #region Header GAMEOBJECT REFERENCES
    [Space(10)]
    [Header("GAMEOBJECT REFERENCES")]
    #endregion
    [SerializeField] private GameObject pauseMenu;

    #region Header DUNGEON LEVELS

    [Space(10)]
    [Header("DUNGEON LEVELS")]

    #endregion
    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;

    [SerializeField] private int currentDungeonLevelListIndex = 0;
    private Room currentRoom;
    private Room previousRoom;
    private PlayerDetailsSO playerDetails;
    private Player player;

    [HideInInspector] public GameState gameState;
    [HideInInspector] public GameState previousGameState;
    private long gameScore;
    private int scoreMultiplier;

    protected override void Awake()
    {
        base.Awake();

        playerDetails = GameResources.Instance.currentPlayer.playerDetails;

        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        GameObject playerGameObject = Instantiate(playerDetails.playerPrefab);

        player = playerGameObject.GetComponent<Player>();

        player.Initialize(playerDetails);
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;

        StaticEventHandler.OnPointsScored += StaticEventHandler_OnPointsScored;

        StaticEventHandler.OnMultiplier += StaticEventHandler_OnMultiplier;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;

        StaticEventHandler.OnPointsScored -= StaticEventHandler_OnPointsScored;

        StaticEventHandler.OnMultiplier -= StaticEventHandler_OnMultiplier;
    }

    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }

    private void StaticEventHandler_OnPointsScored(PointsScoredArgs pointsScoredArgs)
    {
        gameScore += pointsScoredArgs.points * scoreMultiplier;

        StaticEventHandler.CallScoreChangedEvent(gameScore, scoreMultiplier);
    }

    private void StaticEventHandler_OnMultiplier(MultiplierArgs multiplierArgs)
    {
        if (multiplierArgs.multiplier)
        {
            scoreMultiplier++;
        }
        else
        {
            scoreMultiplier--;
        }

        scoreMultiplier = Mathf.Clamp(scoreMultiplier, 1, 30);

        StaticEventHandler.CallScoreChangedEvent(gameScore, scoreMultiplier);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        previousGameState = GameState.gameStarted;
        gameState = GameState.gameStarted;

        gameScore = 0;

        scoreMultiplier = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleGameState();

        // Testing
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    gameState = GameState.gameStarted;
        //}
    }

    /// <summary>
    /// Handle game state
    /// </summary>
    public void HandleGameState()
    {
        // Handle Game state
        switch (gameState)
        {
            case GameState.gameStarted:

                // Play first level
                PlayDungeonLevel(currentDungeonLevelListIndex);

                gameState = GameState.playingLevel;

                break;

            case GameState.playingLevel:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGameMenu();
                }

                break;

            case GameState.engagingEnemies:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGameMenu();
                }

                break;

            case GameState.bossStage:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGameMenu();
                }

                break;

            case GameState.engagingBoss:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGameMenu();
                }

                break;

            case GameState.gamePaused:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGameMenu();
                }

                break;
        }
    }

    public void SetCurrentRoom(Room room)
    {
        previousRoom = currentRoom;
        currentRoom = room;
    }

    public void PauseGameMenu()
    {
        if (gameState != GameState.gamePaused)
        {
            pauseMenu.SetActive(true);
            //GetPlayer().playerControl.DisablePlayer();

            previousGameState = gameState;
            gameState = GameState.gamePaused;
        }
        else if (gameState == GameState.gamePaused)
        {
            pauseMenu.SetActive(false);
            //GetPlayer().playerControl.EnablePlayer();

            gameState = previousGameState;
            previousGameState = GameState.gamePaused;
        }
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        // Build Dungeon for level
        bool dungeonBuiltSuccessfully = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSuccessfully)
        {
            Debug.LogError("Could not build dungeon from specified rooms and node graphs");
        }

        StaticEventHandler.CallRoomChangedEvent(currentRoom);

        player.gameObject.transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f, (currentRoom.lowerBounds.y + currentRoom.upperBounds.y) / 2f, 0f);

        player.gameObject.transform.position = HelperUltilities.GetSpawnPositionNearestToPlayer(player.gameObject.transform.position);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Sprite GetPlayerMinimapIcon()
    {
        return playerDetails.playerMiniMapIcon;
    }

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }

    public DungeonLevelSO GetCurrentDungeonLevel()
    {
        return dungeonLevelList[currentDungeonLevelListIndex];
    }

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUltilities.ValidateCheckNullValue(this, nameof(pauseMenu), pauseMenu);
        HelperUltilities.ValidateCheckEnumarableValues(this, nameof(dungeonLevelList), dungeonLevelList);
    }

#endif
    #endregion Validation
}

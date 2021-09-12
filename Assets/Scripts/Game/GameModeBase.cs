using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameModeBase : MonoBehaviour
{
    [Header("Game Mode Base")]
    [SerializeField] protected Timer _timer;
    [SerializeField] protected Player _player;
    [SerializeField] protected Map _map;
    [SerializeField] protected GameStats _gameStats;
    [SerializeField] protected LoadingMapGUI _loadingMap;

    [Header("Gameplay")]
    [SerializeField] protected int _startingCardsCount = 5;
    [SerializeField] protected float _playTurnTime = 20;
    [SerializeField] protected int _turnsCount = 0;
    [SerializeField] protected TurnType _currentTurnType = TurnType.Player;
    [SerializeField] protected TurnPhase _currentTurnPhase = TurnPhase.Refill;
    [SerializeField] protected GameState _gameState = GameState.Stopped;
    [SerializeField] protected int _invalidTerrainBuildingAliveToleranceTurns = 2;


    [Header("Player State")]
    [SerializeField] protected bool _gotPlayerBaseResources = false;
    [SerializeField] protected bool _hasPlayerDrawnCard = false;
    [SerializeField] protected bool _gotPlayerBuildingsResources = false;

    [Header("Map")]
    [SerializeField] protected int _mapWidth = 10;
    [SerializeField] protected int _mapHeight = 10;
    [SerializeField] protected ResourcesAmounts _passiveResourcesPerTurn;
    [SerializeField] protected ResourcesAmounts _lastTurnRessourcesPerTurn;

    private static GameModeBase instance;
    public UnityAction OnChangeTurn;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Theres is more than on instance of the Game Mode. This is not allowed!");
        }
        else
        {
            instance = this;
        }
    }

    public virtual void Update() { }

    public virtual void StartGame()
    {
        this._map.OnMapFinishedCreating += this._onStartGameMapReady;
        this._map.GenerateMap(this._mapWidth, this._mapHeight);
        _loadingMap.gameObject.SetActive(true);
    }

    public void ChangeTurn()
    {
        if (this._currentTurnType == TurnType.Player)
        {
            this._currentTurnType = TurnType.CPU;
            this.ChangePhase(TurnPhase.Main);
            this.RegisterGameStats();
        }
        else
        {
            this._currentTurnType = TurnType.Player;
            this.ChangePhase(TurnPhase.Main);
        }
        this._turnsCount++;
        OnChangeTurn?.Invoke();
    }

    public void PauseTurn()
    {
        this._timer.PauseTimer();
    }

    public void ResumeTurn()
    {
        this._timer.ResumeTimer();
    }

    public void ChangePhase(TurnPhase phase)
    {
        this._currentTurnPhase = phase;
    }

    public ResourcesAmounts GivePlayerBaseResources()
    {
        this._player.GetResource(_passiveResourcesPerTurn);
        return _passiveResourcesPerTurn;
    }

    public ResourcesAmounts GetPlayerBuildingsResources()
    {
        ResourcesAmounts buildingResources = new ResourcesAmounts();
        this._map.GetBuildings().ForEach(build =>
        {
            buildingResources += build.Card.ResourcesPerTurn;
        });
        this._player.GetResource(buildingResources);
        return buildingResources;
    }

    public void DestroyPlayerInvalidBuildings()
    {
        this._map.GetBuildings().ForEach(build =>
        {
            if (!build.CanSitOnTerrain())
            {
                if (build.Terrain.TurnsAlive >= this._invalidTerrainBuildingAliveToleranceTurns)
                {
                    build.Terrain.DestroyBuild();
                }
            }
        });
    }

    public void EndTurn()
    {
        this.ChangeTurn();
        this._timer.StartTime = 4;
        this._timer.ResetTimer();
        this._timer.StartTimer();
    }

    public void PlayerDrawNewHand()
    {
        this._player.DrawNewHand(this._startingCardsCount);
    }

    public void RegisterGameStats(){
        this._gameStats.resourcesProgression.Add(
            new GameStatsResourceProgression(){
                turn = this._turnsCount == 0 ? this._turnsCount : this._turnsCount / 2,
                food = this._player.Resources.Food,
                gold = this._player.Resources.Gold,
                military = this._player.Resources.Military,
                people = this._player.Resources.People,
                stone = this._player.Resources.Stone,
                wood = this._player.Resources.Wood
            }
        );

        double progress = (this._player.Resources.Food * 0.5) + 
                        (this._player.Resources.Gold * 1) + 
                        (this._player.Resources.Military * 2) + 
                        (this._player.Resources.People * 0.2) + 
                        (this._player.Resources.Stone * 0.6) + 
                        (this._player.Resources.Wood * 0.6);

        this._gameStats.gameProgression.Add(            
            new GameStatsFullProgression(){
                turn = this._turnsCount == 0 ? this._turnsCount : this._turnsCount / 2,
                progress = progress
            }
        );
    }

    private void _onStartGameMapReady()
    {
        this._player.DrawCard(this._startingCardsCount);
        this._currentTurnType = TurnType.Player;
        this._currentTurnPhase = TurnPhase.Main;
        this._timer.StartTime = 2;
        this._timer.ResetTimer();
        this._timer.StartTimer();
        this._gameState = GameState.Playing;
        this._map.OnMapFinishedCreating -= this._onStartGameMapReady;
    }

    public static GameModeBase Instance { get => instance; }
    public TurnType CurrentTurnType { get => _currentTurnType; }
    public TurnPhase CurrentTurnPhase { get => _currentTurnPhase; }
    public Player Player { get => _player; }
    public int MapWidth { get => _mapWidth; }
    public int MapHeight { get => _mapHeight; }
    public GameState GameState { get => _gameState; }
    public Map Map { get => _map; }
    public ResourcesAmounts LastTurnRessourcesPerTurn { get => _lastTurnRessourcesPerTurn; }
}

public enum TurnType
{
    Player,
    CPU
}

public enum TurnPhase
{
    Refill,
    Draw,
    Play,
    Disaster,
    Military,
    Destroy,
    Main
}

public enum GameState
{
    Playing,
    Paused,

    Stopped,
    Replay
}
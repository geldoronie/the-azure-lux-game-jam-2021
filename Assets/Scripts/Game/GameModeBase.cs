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

    [Header("Gameplay")]
    [SerializeField] protected int _startingCardsCount = 5;
    [SerializeField] protected float _playTurnTime = 20;
    [SerializeField] protected float _turnsCount = 0;
    [SerializeField] protected TurnType _currentTurnType = TurnType.Player;
    [SerializeField] protected TurnPhase _currentTurnPhase = TurnPhase.Refill;

    [SerializeField] protected GameState _gameState = GameState.Stopped;

    [Header("Player State")]
    [SerializeField] protected bool _gotPlayerBaseResources = false;
    [SerializeField] protected bool _hasPlayerDrawnCard = false;
    [SerializeField] protected bool _gotPlayerBuildingsResources = false;

    [Header("Map")]
    [SerializeField] protected int _mapWidth = 10;
    [SerializeField] protected int _mapHeight = 10;
    [SerializeField] protected ResourcesAmounts resourcePerTurn;

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
    }

    public void ChangeTurn()
    {
        if (this._currentTurnType == TurnType.Player)
        {
            this._currentTurnType = TurnType.CPU;
            this.ChangePhase(TurnPhase.Main);
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

    public void GivePlayerBaseResources()
    {
        this._player.GetResource(resourcePerTurn);
    }

    public void GetPlayerBuildingsResources()
    {
        this._map.GetBuildings().ForEach(build =>
        {
            this._player.GetResource(build.ResourcesPerTurn);
        });
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
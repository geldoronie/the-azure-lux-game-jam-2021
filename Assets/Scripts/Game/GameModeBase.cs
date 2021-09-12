using UnityEngine;

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
    [SerializeField] protected bool _isRunning = false;

    [Header("Map")]
    [SerializeField] protected int _mapWidth = 10;
    [SerializeField] protected int _mapHeight = 10;
    [SerializeField] protected ResourcesAmounts resourcePerTurn;

    private static GameModeBase instance;

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
        this._player.DrawCard(this._startingCardsCount);
        this._map.GenerateMap(this._mapWidth, this._mapHeight);
        this._currentTurnType = TurnType.CPU;
        this._currentTurnPhase = TurnPhase.Main;
        this._timer.StartTime = 2;
        this._timer.ResetTimer();
        this._timer.StartTimer();
        this._isRunning = true;
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
    }

    public void ChangePhase(TurnPhase phase)
    {
        this._currentTurnPhase = phase;
    }

    public void GivePlayerBaseResources()
    {
        this._player.GetResource(resourcePerTurn);
    }

    public static GameModeBase Instance { get => instance; }
    public TurnType CurrentTurnType { get => _currentTurnType; }
    public TurnPhase CurrentTurnPhase { get => _currentTurnPhase; }
    public bool IsRunning { get => _isRunning; }
    public Player Player { get => _player; }
    public int MapWidth { get => _mapWidth; }
    public int MapHeight { get => _mapHeight; }
    protected Timer Timer { get => _timer; }
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
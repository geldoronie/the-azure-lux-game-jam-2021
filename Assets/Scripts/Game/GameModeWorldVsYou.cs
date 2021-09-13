using UnityEngine;

public class GameModeWorldVsYou : GameModeBase
{
    [Header("CPU State")]
    [SerializeField] private int _cardPerTurn = 3;
    [SerializeField] private int _maxCardsOnHand = 10;
    [SerializeField] private bool _terrainDisasterExecuted = false;
    [SerializeField] private bool _changeTerrainDisasterDone = false;
    [SerializeField] private bool _invalidBuildsDestroyed = false;
    [SerializeField] private bool _vickingsRaidCheck = false;
    [SerializeField] private int firstVickingRaidMilitary = 500;
    [SerializeField] private int firstVickingRaidTurn = 10;
    [SerializeField] private float vickingsTurnsIncreasePerRaidRate = 1.25f;
    [SerializeField] private float vickingsMilitaryIncreasePerRaidRate = 1.25f;

    private int _vickingsAttackMilitaryRequired;
    private int _vickingsAttackTurnsLeft;

    protected override void Awake()
    {
        base.Awake();
        _vickingsAttackMilitaryRequired = firstVickingRaidMilitary;
        _vickingsAttackTurnsLeft = firstVickingRaidTurn;
    }

    public override void Update()
    {
        base.Update();

        if (this._gameState != GameState.Playing)
            return;

        if (this.CurrentTurnType == TurnType.Player)
        {
            this._playerTurnUpdate();
        }
        else if (this.CurrentTurnType == TurnType.CPU)
        {
            this._cpuTurnUpdate();
        }
    }

    private void _playerTurnUpdate()
    {
        if (this.CurrentTurnPhase == TurnPhase.Main)
        {
            _lastTurnRessourcesPerTurn = new ResourcesAmounts();

            this._gotPlayerBaseResources = false;
            this._hasPlayerDrawnCard = false;
            this._gotPlayerBuildingsResources = false;

            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Refill);
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
                while (this._player.Hand.Length > _maxCardsOnHand)
                {
                    this._player.RemoveCard(this._player.Hand[Random.Range(0, this._player.Hand.Length)]);
                }
            }
        }
        else if (this.CurrentTurnPhase == TurnPhase.Refill)
        {
            //got the player base resources
            if (!this._gotPlayerBaseResources)
            {
                _lastTurnRessourcesPerTurn += this.GivePlayerBaseResources();
                this._gotPlayerBaseResources = true;
            }

            //got the player buildings resources
            if (!this._gotPlayerBuildingsResources)
            {
                _lastTurnRessourcesPerTurn += this.GetPlayerBuildingsResources();
                this._gotPlayerBuildingsResources = true;
            }

            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Draw);
                this._timer.StartTime = 5;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this.CurrentTurnPhase == TurnPhase.Draw)
        {
            if (!this._hasPlayerDrawnCard)
            {
                this._player.DrawCard(_cardPerTurn);
                this._hasPlayerDrawnCard = true;
            }

            if (this._timer.Remaining <= 0)
            {
                this._timer.StartTime = this._playTurnTime;
                this._timer.ResetTimer();
                this._timer.StartTimer();
                this.ChangePhase(TurnPhase.Play);
            }
        }
        else if (this.CurrentTurnPhase == TurnPhase.Play)
        {
            if (this._timer.Remaining <= 0)
            {
                if(this._player.Resources.People <= 0){
                    this.ChangeGameState(GameState.GameOver);
                } else {
                    this.ChangeTurn();
                    this._timer.StartTime = 4;
                    this._timer.ResetTimer();
                    this._timer.StartTimer();
                }
            }
        }
    }

    private void _cpuTurnUpdate()
    {
        if (this._currentTurnPhase == TurnPhase.Main)
        {
            this._terrainDisasterExecuted = false;
            this._invalidBuildsDestroyed = false;
            this._vickingsRaidCheck = false;

            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Destroy);
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Destroy)
        {
            if (!this._invalidBuildsDestroyed)
            {
                this._executeBuildingDestroy();
                this._invalidBuildsDestroyed = true;
            }

            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Disaster);
                this._timer.StartTime = 0;
                this._timer.ResetTimer();
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Disaster)
        {
            if (!this._terrainDisasterExecuted)
            {
                this._executeChangeTerrainDisaster();
                this._terrainDisasterExecuted = true;
            }
            if (this._changeTerrainDisasterDone)
            {
                this.ChangePhase(TurnPhase.Military);
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Military)
        {
            if (!this._vickingsRaidCheck)
            {
                this.CheckVickingRaid();
                this._vickingsRaidCheck = true;
            }

            if (this._timer.Remaining <= 0)
            {
                this._timer.StartTime = 10;
                this._timer.ResetTimer();
                this._timer.StartTimer();
                this.ChangePhase(TurnPhase.Play);
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Play)
        {
            if (this._timer.Remaining <= 0)
            {
                this.ChangeTurn();
                this._timer.StartTime = 4;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
    }

    private void _executeChangeTerrainDisaster()
    {
        int x = Mathf.RoundToInt(Random.Range(0, this._mapWidth));
        int y = Mathf.RoundToInt(Random.Range(0, this._mapHeight));
        int selectedType = Mathf.RoundToInt(Random.Range(0, 5));
        this._changeTerrainDisasterDone = false;
        this._map.OnMapFinishedCellularAutomata += this._onChangeTerrainDisasterReady;

        TerrainRule terrain;

        switch (selectedType)
        {
            case 0:
                terrain = new DesertTerrainRule();
                break;
            case 1:
                terrain = new ForestTerrainRule();
                break;
            case 2:
                terrain = new GrasslandTerrainRule();
                break;
            case 3:
                terrain = new MountainTerrainRule();
                break;
            case 4:
                terrain = new RiverTerrainRule();
                break;
            case 5:
                terrain = new SwampTerrainRule();
                break;
            default:
                terrain = new GrasslandTerrainRule();
                break;
        }

        this._map.SetTerrainApplyCellularAutomata(terrain, x, y);
    }

    private void CheckVickingRaid()
    {
        _vickingsAttackTurnsLeft--;
        if (_vickingsAttackTurnsLeft == 0)
        {
            int militaryTaken = Mathf.Clamp(_vickingsAttackMilitaryRequired, 0, _player.Resources.Military);
            int peopleNeeded = _vickingsAttackMilitaryRequired - militaryTaken;

            Player.Resources -= new ResourcesAmounts(0, 0, 0, 0, peopleNeeded * 2, militaryTaken);

            _vickingsAttackTurnsLeft = Mathf.RoundToInt(firstVickingRaidTurn + Mathf.Pow(CurrentTurn / 2, vickingsTurnsIncreasePerRaidRate));
            _vickingsAttackMilitaryRequired = Mathf.RoundToInt(firstVickingRaidMilitary + Mathf.Pow(CurrentTurn / 2, vickingsTurnsIncreasePerRaidRate));
        }
    }

    public void _onChangeTerrainDisasterReady()
    {
        this._changeTerrainDisasterDone = true;
        this._map.OnMapFinishedCellularAutomata -= this._onChangeTerrainDisasterReady;
    }

    public void _executeBuildingDestroy()
    {
        this.DestroyPlayerInvalidBuildings();
    }

    public override void StartGame()
    {
        base.StartGame();
    }

    public int VickingsAttackMilitaryRequired { get => _vickingsAttackMilitaryRequired; }
    public int VickingsAttackTurnsLeft { get => _vickingsAttackTurnsLeft; }
}

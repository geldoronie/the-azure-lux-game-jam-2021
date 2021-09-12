using UnityEngine;

public class GameModeWorldVsYou : GameModeBase
{

    [Header("CPU State")]
    [SerializeField] private bool _terrainDisasterExecuted = false;
    [SerializeField] private bool _changeTerrainDisasterDone = false;

    public override void Update()
    {
        base.Update();

        if (this._gameState != GameState.Playing )
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
            this._gotPlayerBaseResources = false;
            this._hasPlayerDrawnCard = false;
            this._gotPlayerBuildingsResources = false;
            
            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Refill);
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this.CurrentTurnPhase == TurnPhase.Refill)
        {
            //got the player base resources
            if(!this._gotPlayerBaseResources){
                this.GivePlayerBaseResources();
                this._gotPlayerBaseResources = true;
            }
    
            //got the player buildings resources
            if(!this._gotPlayerBuildingsResources){
                this.GetPlayerBuildingsResources();
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
            //Player drawn the turn card
            if(!this._hasPlayerDrawnCard){
                this._player.DrawCard(1);
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
                this.ChangeTurn();
                this._timer.StartTime = 4;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
    }

    private void _cpuTurnUpdate()
    {
        if (this._currentTurnPhase == TurnPhase.Main)
        {
            this._terrainDisasterExecuted = false;

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
            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Disaster);
                this._timer.StartTime = 5;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Disaster)
        {
            if (!this._terrainDisasterExecuted){
                this._executeChangeTerrainDisaster();
                this._terrainDisasterExecuted = true;
            }
            if(_changeTerrainDisasterDone){
                if (this._timer.Remaining <= 0)
                {
                    this.ChangePhase(TurnPhase.Military);
                    this._timer.StartTime = 2;
                    this._timer.ResetTimer();
                    this._timer.StartTimer();
                }
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Military)
        {
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

    private void _executeChangeTerrainDisaster(){
        int x = Mathf.RoundToInt(Random.Range(0, this._mapWidth));
        int y = Mathf.RoundToInt(Random.Range(0, this._mapHeight));
        int selectedType =  Mathf.RoundToInt(Random.Range(0, 5));
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

        this._map.SetTerrainApplyCellularAutomata(terrain,x,y);
    }

    public void _onChangeTerrainDisasterReady(){
        this._changeTerrainDisasterDone = true;
        this._map.OnMapFinishedCellularAutomata -= this._onChangeTerrainDisasterReady;
    }

    public override void StartGame()
    {
        base.StartGame();
    }
}

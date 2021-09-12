public class GameModeWorldVsYou : GameModeBase
{
    public override void Update()
    {
        base.Update();

        if (!this.IsRunning)
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
            if (this._timer.Remaining <= 0)
            {
                this.GivePlayerBaseResources();
                this.ChangePhase(TurnPhase.Draw);
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this.CurrentTurnPhase == TurnPhase.Draw)
        {
            if (this._timer.Remaining <= 0)
            {
                this._player.DrawCard(1);
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
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
            }
        }
        else if (this._currentTurnPhase == TurnPhase.Disaster)
        {
            if (this._timer.Remaining <= 0)
            {
                this.ChangePhase(TurnPhase.Military);
                this._timer.StartTime = 2;
                this._timer.ResetTimer();
                this._timer.StartTimer();
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

    public override void StartGame()
    {
        base.StartGame();
    }
}

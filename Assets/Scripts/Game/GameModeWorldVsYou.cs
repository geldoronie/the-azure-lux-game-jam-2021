using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeWorldVsYou : GameModeBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(!this.IsRunning)
            return;

        if( this.CurrentTurnType == TurnType.Player ){
            this._playerTurnUpdate();
        } else if (this.CurrentTurnType == TurnType.CPU){
            this._cpuTurnUpdate();
        }
    }

    private void _playerTurnUpdate(){
        if(this.CurrentTurnPhase == TurnPhase.Main){
            if(this.Timer.Remaining <= 0){
                this.ChangePhase(TurnPhase.Refill);
                this.Timer.StartTime = 2;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Refill){
            if(this.Timer.Remaining <= 0){
                this.GivePlayerBaseResources();
                this.ChangePhase(TurnPhase.Draw);
                this.Timer.StartTime = 2;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Draw){
            if(this.Timer.Remaining <= 0){
                this.Player.DrawCard(1);
                this.Timer.StartTime = this.PlayTurnTime;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
                this.ChangePhase(TurnPhase.Play);
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Play){
            if(this.Timer.Remaining <= 0){
                this.ChangeTurn();
                this.Timer.StartTime = 4;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        }
    }

    private void _cpuTurnUpdate(){
        if(this.CurrentTurnPhase == TurnPhase.Main){
            if(this.Timer.Remaining <= 0){
                this.ChangePhase(TurnPhase.Destroy);
                this.Timer.StartTime = 2;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Destroy){
            if(this.Timer.Remaining <= 0){
                this.ChangePhase(TurnPhase.Disaster);
                this.Timer.StartTime = 2;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Disaster){
            if(this.Timer.Remaining <= 0){
                this.ChangePhase(TurnPhase.Military);
                this.Timer.StartTime = 2;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Military){
            if(this.Timer.Remaining <= 0){
                this.Timer.StartTime = 10;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
                this.ChangePhase(TurnPhase.Play);
            }
        } else if(this.CurrentTurnPhase == TurnPhase.Play){
            if(this.Timer.Remaining <= 0){
                this.ChangeTurn();
                this.Timer.StartTime = 4;
                this.Timer.ResetTimer();
                this.Timer.StartTimer();
            }
        }
    }

    public override void StartGame(){
        base.StartGame();
        
    }
}

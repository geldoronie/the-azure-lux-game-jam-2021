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
        if(this.Timer.Remaining <= 0){
            this.ChangeTurn();
            this.Timer.ResetTimer();
            this.Timer.StartTimer();
            
        }
    }

    private void _cpuTurnUpdate(){
        if(this.Timer.Remaining <= 0){
            this.ChangeTurn();
            this.Timer.ResetTimer();
            this.Timer.StartTimer();
            //Give PlayerBaseResources
            this.GivePlayerBaseResources();
        }
    }

    public override void StartGame(){
        base.StartGame();
        
    }
}

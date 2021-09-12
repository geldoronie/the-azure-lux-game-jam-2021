using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeBase : MonoBehaviour
{
    [SerializeField]
    public Timer Timer;

    [SerializeField]
    public Player Player;

    [SerializeField]
    public Map Map;

    [Header("Gameplay")]
    [SerializeField]
    public int StartingCardsCount = 5;

    [SerializeField]
    public float TurnTime = 20;

    [SerializeField]
    public float TurnsCount = 0;

    [SerializeField]
    public TurnType CurrentTurnType = TurnType.Player;

    [SerializeField]
    public bool IsRunning = false;

    [Header("Map")]
    [SerializeField]
    public int MapWidth = 10;

    [SerializeField]
    public int MapHeight = 10;
    public virtual void StartGame(){
        this.Player.DrawCard(this.StartingCardsCount);
        this.Map.GenerateMap(this.MapWidth,this.MapHeight);
        this.Timer.StartTime = this.TurnTime;
        this.Timer.StartTimer();
        this.IsRunning = true;
    }

    public void ChangeTurn(){
        if(this.CurrentTurnType == TurnType.Player){
            this.CurrentTurnType = TurnType.CPU;
        } else {
            this.CurrentTurnType = TurnType.Player;
        }
    }

    public void GivePlayerBaseResources(){
        this.Player.GetResource(new ResourcesAmounts(){
            food = 5,
            gold = 5,
            military = 5,
            people = 5,
            stone = 5,
            wood = 5
        });
    }

    public virtual void Update(){

    }
}

[System.Serializable]
public enum TurnType
{
    Player,
    CPU
}
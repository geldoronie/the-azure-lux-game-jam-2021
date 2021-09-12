using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeBase : MonoBehaviour
{
    [SerializeField]
    public Player Player;

    [SerializeField]
    public Map Map;

    [SerializeField]
    public int StartingCardsCount = 5;
    [Header("Map")]
    [SerializeField]
    public int MapWidth = 10;

    [SerializeField]
    public int MapHeight = 10;
    public virtual void StartGame(){
        this.Player.DrawNewHand(this.StartingCardsCount);
        this.Map.GenerateMap(this.MapWidth,this.MapHeight);
    }
}


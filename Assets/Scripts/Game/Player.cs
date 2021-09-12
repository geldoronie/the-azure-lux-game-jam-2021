using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _woodAmount = 0;
    [SerializeField] private int _stoneAmount = 0;
    [SerializeField] private int _goldAmount = 0;
    [SerializeField] private int _foodAmount = 0;
    [SerializeField] private int _peopleAmount = 0;
    [SerializeField] private int _militaryAmount = 0;
    [SerializeField] private CardsLibrary _library;
    [SerializeField] public List<Card> Hand;

    public UnityAction OnHandCardsUpdate;

    public void DrawCard(int cardCount)
    {
        this.Hand = this._library.GetCards(cardCount);
        OnHandCardsUpdate?.Invoke();
    }

    public void GetResource(ResourcesAmounts resource){
        this._foodAmount += resource.food;
        this._goldAmount += resource.gold;
        this._militaryAmount += resource.military;
        this._peopleAmount += resource.people;
        this._stoneAmount += resource.stone;
        this._woodAmount += resource.wood;
    }

    public int WoodAmount { get => _woodAmount; }
    public int StoneAmount { get => _stoneAmount; }
    public int GoldAmount { get => _goldAmount; }
    public int FoodAmount { get => _foodAmount; }
    public int PeopleAmount { get => _peopleAmount; }
    public int MilitaryAmount { get => _militaryAmount; }
}
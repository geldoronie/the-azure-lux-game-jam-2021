using System;
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
    [SerializeField] private List<Card> _hand;

    public UnityAction OnHandCardsUpdate;

    public void DrawCard(int cardCount)
    {
        this._hand.AddRange(this._library.GetCards(cardCount));
        OnHandCardsUpdate?.Invoke();
    }

    public void DrawNewHand(int cardCount)
    {
        this._hand = this._library.GetCards(cardCount);
        OnHandCardsUpdate?.Invoke();
    }

    public void GetResource(ResourcesAmounts resource)
    {
        this._foodAmount += resource.Food;
        this._goldAmount += resource.Gold;
        this._militaryAmount += resource.Military;
        this._peopleAmount += resource.People;
        this._stoneAmount += resource.Stone;
        this._woodAmount += resource.Wood;
    }

    public void RemoveCard(Card card)
    {
        _hand.Remove(card);
        OnHandCardsUpdate?.Invoke();
    }

    public int WoodAmount { get => _woodAmount; set => _woodAmount = value; }
    public int StoneAmount { get => _stoneAmount; set => _stoneAmount = value; }
    public int GoldAmount { get => _goldAmount; set => _goldAmount = value; }
    public int FoodAmount { get => _foodAmount; set => _foodAmount = value; }
    public int PeopleAmount { get => _peopleAmount; set => _peopleAmount = value; }
    public int MilitaryAmount { get => _militaryAmount; set => _militaryAmount = value; }
    public Card[] Hand { get => _hand.ToArray(); }
}
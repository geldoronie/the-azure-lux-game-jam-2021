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

    public void GetResource(ResourcesAmounts resource)
    {
        this._foodAmount += resource.Food;
        this._goldAmount += resource.Gold;
        this._militaryAmount += resource.Military;
        this._peopleAmount += resource.People;
        this._stoneAmount += resource.Stone;
        this._woodAmount += resource.Wood;
    }

    public int WoodAmount { get => _woodAmount; }
    public int StoneAmount { get => _stoneAmount; }
    public int GoldAmount { get => _goldAmount; }
    public int FoodAmount { get => _foodAmount; }
    public int PeopleAmount { get => _peopleAmount; }
    public int MilitaryAmount { get => _militaryAmount; }
    public Card[] Hand { get => _hand.ToArray(); }
}
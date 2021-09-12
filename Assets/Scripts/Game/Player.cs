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

    public void DrawNewHand(int cardCount)
    {
        this.Hand = this._library.GetCards(cardCount);
        OnHandCardsUpdate?.Invoke();
    }

    public int WoodAmount { get => _woodAmount; }
    public int StoneAmount { get => _stoneAmount; }
    public int GoldAmount { get => _goldAmount; }
    public int FoodAmount { get => _foodAmount; }
    public int PeopleAmount { get => _peopleAmount; }
    public int MilitaryAmount { get => _militaryAmount; }
}
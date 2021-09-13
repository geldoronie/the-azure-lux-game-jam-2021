using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private ResourcesAmounts _resources;
    [SerializeField] private CardsLibrary _library;
    [SerializeField] private List<Card> _hand;

    public UnityAction OnHandCardsUpdate;

    public void DrawCard(int cardCount, CardTypeToGive cardTypeToGive = CardTypeToGive.Any)
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
        _resources += resource;
    }

    public void RemoveCard(Card card)
    {
        _hand.Remove(card);
        OnHandCardsUpdate?.Invoke();
    }

    public ResourcesAmounts Resources { get => _resources; set => _resources = value; }
    public Card[] Hand { get => _hand.ToArray(); }
}
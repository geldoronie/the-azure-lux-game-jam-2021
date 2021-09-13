using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private ResourcesAmounts _resources;
    [SerializeField] private CardsLibrary _library;
    [SerializeField] private List<Card> _hand;
    [Range(1, 100)]
    [SerializeField] private int _buildingCardsChance = 75;
    [Range(1, 100)]
    [SerializeField] private int _effectsCardsChance = 25;

    public UnityAction OnHandCardsUpdate;

    public void DrawCard(int cardCount)
    {
        this._hand.AddRange(this._library.GetCards(cardCount, BuildingCardsChance, EffectsCardsChance));
        OnHandCardsUpdate?.Invoke();
    }

    public void DrawCard(int cardCount, int buildingCardsChance, int effectsCardsChance)
    {
        this._hand.AddRange(this._library.GetCards(cardCount, buildingCardsChance, effectsCardsChance));
        OnHandCardsUpdate?.Invoke();
    }

    public void DrawNewHand(int cardCount)
    {
        this._hand = this._library.GetCards(cardCount, BuildingCardsChance, EffectsCardsChance);
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
    public int EffectsCardsChance { get => _effectsCardsChance; set => _effectsCardsChance = value; }
    public int BuildingCardsChance { get => _buildingCardsChance; set => _buildingCardsChance = value; }
}
using System.Collections.Generic;
using UnityEngine;

public class CardsLibrary : MonoBehaviour
{
    [SerializeField] private CardsPlayfab _cardsDatabase;
    [Range(1, 100)]
    [SerializeField] private int _buildingCardsChance = 75;
    [Range(1, 100)]
    [SerializeField] private int _effectsCardsChance = 25;

    public List<Card> GetCards(int count)
    {
        return this._getCards(this._cardsDatabase.BuildingCards.ToArray(), this._cardsDatabase.EffectCards.ToArray(), count);
    }

    private List<Card> _getCards(BuildingCard[] buildingCards, EffectCard[] effectCards, int cardCount)
    {
        List<Card> newHand = new List<Card>();

        for (int i = 0; i < cardCount; i++)
        {
            float sortBuildings = Random.Range(0, this._buildingCardsChance);
            float sortEffects = Random.Range(0, this._effectsCardsChance);
            if (sortBuildings > sortEffects)
            {
                BuildingCard card = buildingCards[Random.Range(0, buildingCards.Length)];
                newHand.Add(card);
            }
            else
            {
                EffectCard card = effectCards[Random.Range(0, effectCards.Length)];
                newHand.Add(card);
            }
        }

        return newHand;
    }
}

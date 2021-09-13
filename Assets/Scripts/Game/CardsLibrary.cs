using System.Collections.Generic;
using UnityEngine;

public class CardsLibrary : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private int _commonCardRate = 50;
    [Range(1, 100)]
    [SerializeField] private int _uncommonCardRate = 35;
    [Range(1, 100)]
    [SerializeField] private int _rareCardRate = 14;
    [Range(1, 100)]
    [SerializeField] private int _legendaryCardRate = 1;
    [SerializeField] private CardsPlayfab _cardsDatabase;

    public List<Card> GetCards(int count, int buildingRate, int effectRate)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            cards.Add(GetCard(buildingRate, effectRate));
        }
        return cards;
    }

    private Card GetCard(int buildingRate, int effectRate)
    {
        GetRandomStuffType<bool>[] isCardBuildingRate =
        {
            new GetRandomStuffType<bool>(true, buildingRate),
            new GetRandomStuffType<bool>(false, effectRate)
        };

        bool isCardBuilding = GetRandomStuff<bool>.GetRandomThing(isCardBuildingRate);

        List<Card> cardList = new List<Card>();
        if (isCardBuilding)
        {
            switch (GetRandomRarity())
            {
                case CardRarity.Common:
                    cardList.AddRange(this._cardsDatabase.BuildingCards.Common);
                    break;
                case CardRarity.Uncommon:
                    cardList.AddRange(this._cardsDatabase.BuildingCards.Uncommon);
                    break;
                case CardRarity.Rare:
                    cardList.AddRange(this._cardsDatabase.BuildingCards.Rare);
                    break;
                case CardRarity.Legendary:
                    cardList.AddRange(this._cardsDatabase.BuildingCards.Legendary);
                    break;
            }
        }
        else
        {
            switch (GetRandomRarity())
            {
                case CardRarity.Common:
                    cardList.AddRange(this._cardsDatabase.EffectCards.Common);
                    break;
                case CardRarity.Uncommon:
                    cardList.AddRange(this._cardsDatabase.EffectCards.Uncommon);
                    break;
                case CardRarity.Rare:
                    cardList.AddRange(this._cardsDatabase.EffectCards.Rare);
                    break;
                case CardRarity.Legendary:
                    cardList.AddRange(this._cardsDatabase.EffectCards.Legendary);
                    break;
            }
        }
        return cardList[Random.Range(0, cardList.Count)];
    }


    private CardRarity GetRandomRarity()
    {
        GetRandomStuffType<CardRarity>[] rarities =
        {
            new GetRandomStuffType<CardRarity>(CardRarity.Common, _commonCardRate),
            new GetRandomStuffType<CardRarity>(CardRarity.Uncommon, _uncommonCardRate),
            new GetRandomStuffType<CardRarity>(CardRarity.Rare, _rareCardRate),
            new GetRandomStuffType<CardRarity>(CardRarity.Legendary, _legendaryCardRate)
        };

        return GetRandomStuff<CardRarity>.GetRandomThing(rarities);
    }
}

enum CardRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

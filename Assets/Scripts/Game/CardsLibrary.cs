using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardsLibrary : MonoBehaviour
{
    [SerializeField] private CardsPlayfab _cardsDatabase;
    [Range(1, 100)]
    [SerializeField] private int _buildingCardsChance = 75;
    [Range(1, 100)]
    [SerializeField] private int _effectsCardsChance = 25;

    void Awake()
    {

    }

    public List<Card> GetCards(int count)
    {
        return this._getCards(this._cardsDatabase.BuildingCards.ToArray(), this._cardsDatabase.EffectCards.ToArray(), count);
    }

    private List<Card> _getCards(BuildingCardModelPlayfab[] buildingCards, EffectCardModelPlayfab[] effectCards, int cardCount)
    {
        List<Card> newHand = new List<Card>();

        for (int i = 0; i < cardCount; i++)
        {
            float shuffle = Random.Range(0, 100);
            if (shuffle <= this._buildingCardsChance || shuffle >= this._effectsCardsChance)
            {
                BuildingCardModelPlayfab card = buildingCards[Random.Range(0, buildingCards.Length)];

                // Use Cost
                List<ResourceAmount> costs = new List<ResourceAmount>();
                card.cost.ForEach(cost =>
                {
                    costs.Add(new ResourceAmount()
                    {
                        amount = cost.amount,
                        resource = ResourceTypeToResource(cost.type)
                    });
                });

                // Resource Per Turn
                List<ResourceAmount> resourcePerTurn = new List<ResourceAmount>();
                card.resourcePerTurn.ForEach(cost =>
                {
                    resourcePerTurn.Add(new ResourceAmount()
                    {
                        amount = cost.amount,
                        resource = ResourceTypeToResource(cost.type)
                    });
                });

                // Terrain Cost
                List<CardTerrainCost> terrainCosts = new List<CardTerrainCost>();
                card.terrainCost.ForEach(cost =>
                {
                    terrainCosts.Add(TerrrainTypeToTerrainCost(cost.type));
                });

                newHand.Add(
                    new BuildingCard(
                        card.name,
                        card.description,
                        costs,
                        resourcePerTurn,
                        terrainCosts
                    )
                );
            }
            else
            {
                EffectCardModelPlayfab card = effectCards[Random.Range(0, effectCards.Length)];

                // Use Cost
                List<ResourceAmount> costs = new List<ResourceAmount>();
                card.cost.ForEach(cost =>
                {
                    costs.Add(new ResourceAmount()
                    {
                        amount = cost.amount,
                        resource = ResourceTypeToResource(cost.type)
                    });
                });

                // Effects
                EffectBase effect = new EffectBase();
                List<EffectArgument> arguments = new List<EffectArgument>();
                card.effect.arguments.ForEach(arg =>
                {
                    arguments.Add(new EffectArgument()
                    {
                        type = ArgumentTypeToEffectArgumentType(arg.type),
                        value = arg.value
                    });
                });

                //Change Terrain Effects
                if (card.effect.type == "CHANGETERRAIN")
                {
                    effect = new ChangeTerrainEffect()
                    {
                        type = card.effect.type,
                        arguments = arguments,
                        terrainToApply = TerrrainTypeToTerrainCost(card.effect.arguments.Find(a => a.type == "terrain").value),
                    };
                }

                // Get Resource Effects
                if (card.effect.type == "GETRESOURCE")
                {
                    List<ResourceAmount> resources = new List<ResourceAmount>();
                    card.effect.arguments.ForEach(arg =>
                    {
                        resources.Add(new ResourceAmount()
                        {
                            resource = ResourceTypeToResource(arg.type),
                            amount = int.Parse(arg.value)
                        });
                    });

                    effect = new GetResourceEffect()
                    {
                        type = card.effect.type,
                        arguments = arguments,
                        resourcesToGain = resources.ToArray()
                    };
                }

                newHand.Add(new EffectCard(
                        card.name,
                        card.description,
                        costs,
                        effect
                    )
                );
            }
        }

        return newHand;
    }

    private Resource ResourceTypeToResource(string type)
    {
        switch (type)
        {
            case "Wood":
                return Resource.Wood;
            case "Stone":
                return Resource.Stone;
            case "Gold":
                return Resource.Gold;
            case "Food":
                return Resource.Food;
            case "People":
                return Resource.People;
            case "Military":
                return Resource.Military;
            default:
                return Resource.Wood;
        }
    }

    private CardTerrainCost TerrrainTypeToTerrainCost(string type)
    {
        switch (type)
        {
            case "Desert":
                return CardTerrainCost.Desert;
            case "Forest":
                return CardTerrainCost.Forest;
            case "Grasslands":
                return CardTerrainCost.Grasslands;
            case "Montain":
                return CardTerrainCost.Montain;
            case "River":
                return CardTerrainCost.River;
            case "Swamp":
                return CardTerrainCost.Swamp;
            default:
                return CardTerrainCost.Desert;
        }
    }

    private EffectArgumentType ArgumentTypeToEffectArgumentType(string type)
    {
        switch (type)
        {
            case "terrain":
                return EffectArgumentType.Terrain;
            case "resource":
                return EffectArgumentType.Resource;
            default:
                Debug.Log("Effect Argument Type unknow!");
                return EffectArgumentType.Terrain;
        }
    }
}

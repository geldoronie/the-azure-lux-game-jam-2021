using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    [SerializeField]
    private CardsPlayfab _cardsDatabase;

    // Start is called before the first frame update

    [SerializeField]
    private int _playerStartHandCardsCount = 5;
    [SerializeField]
    [Range(1, 100)]
    private int _buildingCardsChance = 75;

    [SerializeField]
    [Range(1, 100)]
    private int _effectsCardsChance = 25;

    [SerializeField]
    public List<Card> PlayerHand;
    
    void Start(){
        
    }

    void Awake()
    {
        this.PlayerHand = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewHand(){
        this.PlayerHand.Clear();
        for (int i = 0; i < this._playerStartHandCardsCount; i++)
        {
            float shuffle = Random.Range(0,100);
            if(shuffle <= this._buildingCardsChance || shuffle >= this._effectsCardsChance){

                BuildingCardModelPlayfab card = this._cardsDatabase.BuildingCards[Random.Range(0,this._cardsDatabase.BuildingCards.Count)];

                // Use Cost
                List<ResourceAmount> costs = new List<ResourceAmount>();
                card.cost.ForEach( cost => {
                    costs.Add(new ResourceAmount(){
                        amount = cost.amount,
                        resource = ResourceTypeToResource(cost.type)
                    });
                });

                // Resource Per Turn
                List<ResourceAmount> resourcePerTurn = new List<ResourceAmount>();
                card.resourcePerTurn.ForEach( cost => {
                    resourcePerTurn.Add(new ResourceAmount(){
                        amount = cost.amount,
                        resource = ResourceTypeToResource(cost.type)
                    });
                });

                // Terrain Cost
                List<CardTerrainCost> terrainCosts = new List<CardTerrainCost>();
                card.terrainCost.ForEach( cost => {
                    terrainCosts.Add(TerrrainTypeToTerrainCost(cost.type));
                });

                this.PlayerHand.Add(
                    new BuildingCard(
                        card.name,
                        card.description,
                        costs,
                        resourcePerTurn,
                        terrainCosts
                    )
                );
            } else {
                EffectCardModelPlayfab card = this._cardsDatabase.EffectCards[Random.Range(0,this._cardsDatabase.EffectCards.Count)];

                // Use Cost
                List<ResourceAmount> costs = new List<ResourceAmount>();
                card.cost.ForEach( cost => {
                    costs.Add(new ResourceAmount(){
                        amount = cost.amount,
                        resource = ResourceTypeToResource(cost.type)
                    });
                });

                // Effects
                EffectBase effect = new EffectBase();
                List<EffectArgument> arguments = new List<EffectArgument>();
                card.effect.arguments.ForEach( arg => {
                    arguments.Add( new EffectArgument(){
                        type = ArgumentTypeToEffectArgumentType(arg.type),
                        value = arg.value
                    });
                });

                //Change Terrain Effects
                if (card.effect.type == "CHANGETERRAIN"){
                    effect = new ChangeTerrainEffect(){
                        type = card.effect.type,
                        arguments = arguments,
                        terrainToApply = TerrrainTypeToTerrainCost(card.effect.arguments.Find(a => a.type == "terrain").value),
                    };
                }

                // Get Resource Effects
                if (card.effect.type == "GETRESOURCE"){
                    List<ResourceAmount> resources = new List<ResourceAmount>();
                    card.effect.arguments.ForEach( arg => {
                        resources.Add(new ResourceAmount(){
                            resource = ResourceTypeToResource(arg.type),
                            amount = int.Parse(arg.value)
                        });
                    });

                    effect = new GetResourceEffect(){
                        type = card.effect.type,
                        arguments = arguments,
                        resourcesToGain = resources.ToArray()
                    };
                }

                this.PlayerHand.Add(new EffectCard(
                        card.name,
                        card.description,
                        costs,
                        effect
                    )
                );
            }
        }
    }

    Resource ResourceTypeToResource(string type){
        switch (type) {
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

    CardTerrainCost TerrrainTypeToTerrainCost(string type){
        switch (type) {
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

    EffectArgumentType ArgumentTypeToEffectArgumentType(string type){
        switch (type) {
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

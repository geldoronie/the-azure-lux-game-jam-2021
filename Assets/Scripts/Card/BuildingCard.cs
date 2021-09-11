using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Building", menuName = "GameJam/Cards/Building")]
public class BuildingCard : Card
{
    [Header("Resources Per Turn")]
    [SerializeField] public ResourceAmount[] resourcePerTurn;

    [SerializeField] public CardTerrainCost[] terrainCosts;

    public BuildingCard(string name, string description, List<ResourceAmount> costs, List<ResourceAmount> resourcePerTurn, List<CardTerrainCost> terrainCosts) 
        : base(name, description, CardType.Building, costs) 
    {
       this.resourcePerTurn = resourcePerTurn.ToArray();
       this.terrainCosts = terrainCosts.ToArray();
    }
}

public enum CardTerrainCost
{
    Desert,
    Forest,
    Grasslands,
    Montain,
    River,
    Swamp
}
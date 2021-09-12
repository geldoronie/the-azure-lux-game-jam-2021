using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
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

    public bool CanBuild(Player player, Terrain terrain)
    {
        bool check = false;
        foreach (CardTerrainCost terrainCheck in terrainCosts)
        {
            if (terrainCheck == CardTerrainCost.Desert && terrain.TerrainRule is DesertTerrainRule)
            {
                check = true;
            }
            else if (terrainCheck == CardTerrainCost.Forest && terrain.TerrainRule is ForestTerrainRule)
            {
                check = true;
            }
            else if (terrainCheck == CardTerrainCost.Grassland && terrain.TerrainRule is GrasslandTerrainRule)
            {
                check = true;
            }
            else if (terrainCheck == CardTerrainCost.Mountain && terrain.TerrainRule is MountainTerrainRule)
            {
                check = true;
            }
            else if (terrainCheck == CardTerrainCost.River && terrain.TerrainRule is RiverTerrainRule)
            {
                check = true;
            }
            else if (terrainCheck == CardTerrainCost.Swamp && terrain.TerrainRule is SwampTerrainRule)
            {
                check = true;
            }
        }

        return check && base.CostCheck(player);
    }
}

public enum CardTerrainCost
{
    Desert,
    Forest,
    Grassland,
    Mountain,
    River,
    Swamp
}
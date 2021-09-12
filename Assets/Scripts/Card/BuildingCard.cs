using UnityEngine;
using System;

[Serializable]
public class BuildingCard : Card
{
    [SerializeField] public ResourcesAmounts resourcesPerTurn;

    [SerializeField] public TerrainCost terrainCost;

    public BuildingCard(string name, string description, string imageId, string prefabId, ResourcesAmounts useCost, ResourcesAmounts resourcesPerTurn, TerrainCost terrainCost) : base(name, description, imageId, prefabId, useCost)
    {
        this.resourcesPerTurn = resourcesPerTurn;
        this.terrainCost = terrainCost;
    }

    public bool CanBuild(Player player, Terrain terrain)
    {
        bool check =
            terrainCost.Desert && terrain.TerrainRule is DesertTerrainRule ||
            terrainCost.Forest && terrain.TerrainRule is ForestTerrainRule ||
            terrainCost.Grassland && terrain.TerrainRule is GrasslandTerrainRule ||
            terrainCost.Mountain && terrain.TerrainRule is MountainTerrainRule ||
            terrainCost.River && terrain.TerrainRule is RiverTerrainRule ||
            terrainCost.Swamp && terrain.TerrainRule is SwampTerrainRule;

        return check && base.CostCheck(player);
    }
}

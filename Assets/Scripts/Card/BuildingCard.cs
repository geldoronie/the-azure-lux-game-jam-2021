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
            terrainCost.desert && terrain.TerrainRule is DesertTerrainRule ||
            terrainCost.forest && terrain.TerrainRule is ForestTerrainRule ||
            terrainCost.grassland && terrain.TerrainRule is GrasslandTerrainRule ||
            terrainCost.mountain && terrain.TerrainRule is MountainTerrainRule ||
            terrainCost.river && terrain.TerrainRule is RiverTerrainRule ||
            terrainCost.swamp && terrain.TerrainRule is SwampTerrainRule;

        return check && base.CostCheck(player);
    }
}

using UnityEngine;

[System.Serializable]
public class BuildingCard : Card
{
    [SerializeField] private ResourcesAmounts resourcesPerTurn;

    public BuildingCard(string name, string description, string imageId, string prefabId, ResourcesAmounts useCost, TerrainCost terrainCost, ResourcesAmounts resourcesPerTurn) : base(name, description, imageId, prefabId, useCost, terrainCost, CardTypeToGive.Building)
    {
        this.resourcesPerTurn = resourcesPerTurn;
    }

    public override bool CanUse(Player player, Terrain terrain)
    {
        if (terrain.Building != null) return false;
        return base.CanUse(player, terrain);
    }

    public ResourcesAmounts ResourcesPerTurn { get => resourcesPerTurn; }
}

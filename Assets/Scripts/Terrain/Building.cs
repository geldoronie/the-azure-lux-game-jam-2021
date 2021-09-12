using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingCard _card;
    private Terrain _terrain;

    public void Initialize(BuildingCard card, Terrain terrain)
    {
        _card = card;
        _terrain = terrain;

        UpdatePosition(terrain.TerrainRule);
    }

    public void UpdatePosition(TerrainRule terrain)
    {
        Vector3 newPos = transform.position;
        newPos.y = terrain.TerrainHeight;
        transform.position = newPos;

        transform.eulerAngles = new Vector3(0, 90 * Random.Range(0, 4), 0);
    }

    public bool CanSitOnTerrain()
    {
        return
            _card.TerrainCost.Desert && _terrain.TerrainRule is DesertTerrainRule ||
            _card.TerrainCost.Grassland && _terrain.TerrainRule is GrasslandTerrainRule ||
            _card.TerrainCost.Desert && _terrain.TerrainRule is DesertTerrainRule ||
            _card.TerrainCost.Forest && _terrain.TerrainRule is ForestTerrainRule ||
            _card.TerrainCost.River && _terrain.TerrainRule is RiverTerrainRule ||
            _card.TerrainCost.Swamp && _terrain.TerrainRule is SwampTerrainRule ||
            _card.TerrainCost.Mountain && _terrain.TerrainRule is MountainTerrainRule;
    }

    public BuildingCard Card { get => _card; }
    public Terrain Terrain { get => _terrain; }
}
public abstract class TerrainRule
{
    private float _terrainHeight;

    protected TerrainRule(float terrainHeight)
    {
        _terrainHeight = terrainHeight;
    }

    protected int CountTerrainType<T>(Terrain[] neighbors) where T : TerrainRule
    {
        int count = 0;

        foreach (Terrain terrain in neighbors)
        {
            if (terrain != null && terrain.TerrainRule is T) count++;
        }

        return count;
    }

    public abstract TerrainRule CheckRules(Terrain[] neighbors);
    public abstract string GetTooltip();

    public float TerrainHeight { get => _terrainHeight; }
}

public abstract class TerrainRule
{
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
}

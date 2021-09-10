public class ForestTerrain : Terrain
{
    public ForestTerrain(int x, int y) : base(x, y) { }

    public override Terrain CheckRules(VonNeumannNeighbors neighbors)
    {
        if (CountTerrainType<RiverTerrain>(neighbors) == 0)
        {
            return new GrasslandsTerrain(X, Y);
        }

        if (CountTerrainType<RiverTerrain>(neighbors) >= 2)
        {
            return new SwampTerrain(X, Y);
        }

        if (CountTerrainType<DesertTerrain>(neighbors) == 4)
        {
            return new DesertTerrain(X, Y);
        }

        return this;
    }
}
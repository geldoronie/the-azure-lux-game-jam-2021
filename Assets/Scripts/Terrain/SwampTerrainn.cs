public class SwampTerrain : Terrain
{
    public SwampTerrain(int x, int y) : base(x, y) { }

    public override Terrain CheckRules(VonNeumannNeighbors neighbors)
    {
        if (CountTerrainType<RiverTerrain>(neighbors) < 2)
        {
            return new ForestTerrain(X, Y);
        }

        if (CountTerrainType<SwampTerrain>(neighbors) == 4)
        {
            return new RiverTerrain(X, Y);
        }

        if (CountTerrainType<DesertTerrain>(neighbors) == 4)
        {
            return new DesertTerrain(X, Y);
        }

        return this;
    }
}
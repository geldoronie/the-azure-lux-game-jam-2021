public class RiverTerrain : Terrain
{
    public RiverTerrain(int x, int y) : base(x, y) { }

    public override Terrain CheckRules(VonNeumannNeighbors neighbors)
    {
        if (CountTerrainType<DesertTerrain>(neighbors) == 4)
        {
            return new DesertTerrain(X, Y);
        }

        if (CountTerrainType<RiverTerrain>(neighbors) == 0)
        {
            return new GrasslandsTerrain(X, Y);
        }

        return this;
    }
}
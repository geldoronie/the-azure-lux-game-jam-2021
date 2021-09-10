public class DesertTerrain : Terrain
{
    public DesertTerrain(int x, int y) : base(x, y) { }

    public override Terrain CheckRules(VonNeumannNeighbors neighbors)
    {
        if (CountTerrainType<RiverTerrain>(neighbors) == 4)
        {
            return new SwampTerrain(X, Y);
        }

        if (CountTerrainType<GrasslandsTerrain>(neighbors) >= 3)
        {
            return new GrasslandsTerrain(X, Y);
        }

        return this;
    }
}
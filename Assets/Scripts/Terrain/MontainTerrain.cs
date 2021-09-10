public class MontainTerrain : Terrain
{
    public MontainTerrain(int x, int y) : base(x, y) { }

    public override Terrain CheckRules(VonNeumannNeighbors neighbors)
    {
        if (CountTerrainType<DesertTerrain>(neighbors) == 4)
        {
            return new DesertTerrain(X, Y);
        }

        return this;
    }
}
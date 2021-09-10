public class GrasslandsTerrain : Terrain
{
    public GrasslandsTerrain(int x, int y) : base(x, y) { }

    public override Terrain CheckRules(VonNeumannNeighbors neighbors)
    {
        if (neighbors._northNeighbor is DesertTerrain && neighbors._southNeighbor is ForestTerrain ||
            neighbors._southNeighbor is DesertTerrain && neighbors._northNeighbor is ForestTerrain ||
            neighbors._eastNeighbor is DesertTerrain && neighbors._westNeighbor is ForestTerrain ||
            neighbors._westNeighbor is DesertTerrain && neighbors._eastNeighbor is ForestTerrain)
        {
            return new MontainTerrain(X, Y);
        }

        if (CountTerrainType<DesertTerrain>(neighbors) == 4)
        {
            return new DesertTerrain(X, Y);
        }

        if (CountTerrainType<RiverTerrain>(neighbors) >= 1 && CountTerrainType<ForestTerrain>(neighbors) >= 1)
        {
            return new ForestTerrain(X, Y);
        }

        if (CountTerrainType<RiverTerrain>(neighbors) == 4)
        {
            return new RiverTerrain(X, Y);
        }

        return this;
    }
}
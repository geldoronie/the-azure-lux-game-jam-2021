public class RiverTerrainRule : TerrainRule
{
    public override TerrainRule CheckRules(Terrain[] neighbors)
    {
        if (CountTerrainType<DesertTerrainRule>(neighbors) == 4)
        {
            return new DesertTerrainRule();
        }

        if (CountTerrainType<RiverTerrainRule>(neighbors) == 0)
        {
            return new GrasslandTerrainRule();
        }

        return this;
    }
}
public class DesertTerrainRule : TerrainRule
{
    public override TerrainRule CheckRules(Terrain[] neighbors)
    {
        if (CountTerrainType<RiverTerrainRule>(neighbors) == 4)
        {
            return new SwampTerrainRule();
        }

        if (CountTerrainType<GrasslandTerrainRule>(neighbors) >= 3)
        {
            return new GrasslandTerrainRule();
        }

        return this;
    }
}
public class SwampTerrainRule : TerrainRule
{
    public override TerrainRule CheckRules(Terrain[] neighbors)
    {
        if (CountTerrainType<RiverTerrainRule>(neighbors) < 2)
        {
            return new ForestTerrainRule();
        }

        if (CountTerrainType<SwampTerrainRule>(neighbors) == 4)
        {
            return new RiverTerrainRule();
        }

        if (CountTerrainType<DesertTerrainRule>(neighbors) == 4)
        {
            return new DesertTerrainRule();
        }

        return this;
    }

    public override string GetTooltip() { return "Swamp"; }
}
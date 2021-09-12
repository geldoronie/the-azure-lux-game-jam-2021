public class ForestTerrainRule : TerrainRule
{
    public ForestTerrainRule() : base(3.4f)
    {
    }

    public override TerrainRule CheckRules(Terrain[] neighbors)
    {
        if (CountTerrainType<RiverTerrainRule>(neighbors) == 0)
        {
            return new GrasslandTerrainRule();
        }

        if (CountTerrainType<RiverTerrainRule>(neighbors) >= 2)
        {
            return new SwampTerrainRule();
        }

        if (CountTerrainType<DesertTerrainRule>(neighbors) == 4)
        {
            return new DesertTerrainRule();
        }

        return this;
    }

    public override string GetTooltip() { return "Forest"; }
}
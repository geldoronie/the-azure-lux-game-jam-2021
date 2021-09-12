public class RiverTerrainRule : TerrainRule
{
    public RiverTerrainRule() : base(0)
    {
    }

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

    public override string GetTooltip() { return "River"; }
}
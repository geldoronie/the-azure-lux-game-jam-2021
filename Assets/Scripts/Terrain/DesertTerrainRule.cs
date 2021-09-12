public class DesertTerrainRule : TerrainRule
{
    public DesertTerrainRule() : base(3.4f)
    {
    }

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

    public override string GetTooltip() { return "Desert"; }
}
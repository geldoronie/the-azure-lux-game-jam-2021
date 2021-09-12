public class MountainTerrainRule : TerrainRule
{
    public MountainTerrainRule() : base(0.158f)
    {
    }

    public override TerrainRule CheckRules(Terrain[] neighbors)
    {
        if (CountTerrainType<DesertTerrainRule>(neighbors) == 4)
        {
            return new DesertTerrainRule();
        }

        return this;
    }

    public override string GetTooltip() { return "Mountain"; }
}
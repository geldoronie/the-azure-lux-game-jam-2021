public class GrasslandTerrainRule : TerrainRule
{
    public override TerrainRule CheckRules(Terrain[] neighbors)
    {
        if (neighbors[0] != null && neighbors[1] != null && neighbors[0].TerrainRule is DesertTerrainRule && neighbors[1].TerrainRule is ForestTerrainRule)
        {
            return new MountainTerrainRule();
        }
        if (neighbors[1] != null && neighbors[0] != null && neighbors[1].TerrainRule is DesertTerrainRule && neighbors[0].TerrainRule is ForestTerrainRule)
        {
            return new MountainTerrainRule();
        }
        if (neighbors[2] != null && neighbors[3] != null && neighbors[2].TerrainRule is DesertTerrainRule && neighbors[3].TerrainRule is ForestTerrainRule)
        {
            return new MountainTerrainRule();
        }
        if (neighbors[3] != null && neighbors[2] != null && neighbors[3].TerrainRule is DesertTerrainRule && neighbors[2].TerrainRule is ForestTerrainRule)
        {
            return new MountainTerrainRule();
        }

        if (CountTerrainType<DesertTerrainRule>(neighbors) == 4)
        {
            return new DesertTerrainRule();
        }

        if (CountTerrainType<RiverTerrainRule>(neighbors) >= 1 && CountTerrainType<ForestTerrainRule>(neighbors) >= 1)
        {
            return new ForestTerrainRule();
        }

        if (CountTerrainType<RiverTerrainRule>(neighbors) == 4)
        {
            return new RiverTerrainRule();
        }

        return this;
    }

    public override string GetTooltip() { return "Grassland"; }
}
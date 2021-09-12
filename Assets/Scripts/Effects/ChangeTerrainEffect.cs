using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Change Terrain Effect", menuName = "Game Jam/Effects/Change Terrain", order = 0)]
public class ChangeTerrainEffect : EffectBase
{
    [SerializeField] private TerrainType _terrain;

    public override void UseEffect(Player player, Terrain terrain)
    {
        GameModeBase.Instance.PauseTurn();
        switch (_terrain)
        {
            case TerrainType.Desert:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new DesertTerrainRule(), terrain.X, terrain.Y);
                break;
            case TerrainType.Forest:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new ForestTerrainRule(), terrain.X, terrain.Y);
                break;
            case TerrainType.Grassland:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new GrasslandTerrainRule(), terrain.X, terrain.Y);
                break;
            case TerrainType.Mountain:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new MountainTerrainRule(), terrain.X, terrain.Y);
                break;
            case TerrainType.River:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new RiverTerrainRule(), terrain.X, terrain.Y);
                break;
            case TerrainType.Swamp:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new SwampTerrainRule(), terrain.X, terrain.Y);
                break;
            default:
                GameModeBase.Instance.Map.SetTerrainApplyCellularAutomata(new GrasslandTerrainRule(), terrain.X, terrain.Y);
                break;

        }
        GameModeBase.Instance.Map.OnMapFinishedCellularAutomata += GameModeBase.Instance.ResumeTurn;
    }
}

public enum TerrainType
{
    Desert,
    Forest,
    Grassland,
    Mountain,
    River,
    Swamp
}
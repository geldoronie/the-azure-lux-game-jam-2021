using UnityEngine;

public class Terrain : MonoBehaviour
{

    private TerrainRule _terrainRule;
    private int _x;
    private int _y;

    public void Initialize(TerrainRule terrainRule, int x, int y)
    {
        _terrainRule = terrainRule;
        _x = x;
        _y = y;
    }

    public TerrainRule TerrainRule { get => _terrainRule; }
    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
}
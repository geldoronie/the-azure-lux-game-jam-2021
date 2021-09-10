using UnityEngine;

public abstract class Terrain
{
    [SerializeField] protected int _x;
    [SerializeField] protected int _y;

    protected Terrain(int x, int y)
    {
        _x = x;
        _y = y;
    }

    protected int CountTerrainType<T>(VonNeumannNeighbors neighbors) where T : Terrain
    {
        int count = 0;
        if (neighbors._northNeighbor != null && neighbors._northNeighbor is T) count++;
        if (neighbors._southNeighbor != null && neighbors._southNeighbor is T) count++;
        if (neighbors._eastNeighbor != null && neighbors._eastNeighbor is T) count++;
        if (neighbors._westNeighbor != null && neighbors._westNeighbor is T) count++;
        return count;
    }

    public abstract Terrain CheckRules(VonNeumannNeighbors neighbors);
    public int X { get => _x; }
    public int Y { get => _y; }
}


public struct VonNeumannNeighbors
{
    public Terrain _northNeighbor;
    public Terrain _eastNeighbor;
    public Terrain _southNeighbor;
    public Terrain _westNeighbor;

    public VonNeumannNeighbors(Terrain northNeighbor, Terrain eastNeighbor, Terrain southNeighbor, Terrain westNeighbor)
    {
        _northNeighbor = northNeighbor;
        _eastNeighbor = eastNeighbor;
        _southNeighbor = southNeighbor;
        _westNeighbor = westNeighbor;
    }
}
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private Terrain[,] _grid;

    private void Start()
    {
        CreateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyCellularAutomata();
        }
    }

    public void CreateMap()
    {
        _grid = new Terrain[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Terrain terrain = null;

                int random = Random.Range(0, 6);
                if (random == 0)
                {
                    terrain = new DesertTerrain(x, y);
                    Debug.Log("DesertTerrain");
                }
                else if (random == 1)
                {
                    terrain = new ForestTerrain(x, y);
                    Debug.Log("ForestTerrain");
                }
                else if (random == 2)
                {
                    terrain = new GrasslandsTerrain(x, y);
                    Debug.Log("GrasslandsTerrain");
                }
                else if (random == 3)
                {
                    terrain = new MontainTerrain(x, y);
                    Debug.Log("MontainTerrain");
                }
                else if (random == 4)
                {
                    terrain = new RiverTerrain(x, y);
                    Debug.Log("RiverTerrain");
                }
                else if (random == 5)
                {
                    terrain = new SwampTerrain(x, y);
                    Debug.Log("SwampTerrain");
                }

                _grid[x, y] = terrain;
            }
        }
    }

    public void ApplyCellularAutomata()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                VonNeumannNeighbors neighbors = GetVonNeumannNeighbors(x, y);
                _grid[x, y] = _grid[x, y].CheckRules(neighbors);
            }
        }
    }

    private VonNeumannNeighbors GetVonNeumannNeighbors(int x, int y)
    {
        return new VonNeumannNeighbors(
            GetTerrainOnGrid(x, y + 1),
            GetTerrainOnGrid(x, y - 1),
            GetTerrainOnGrid(x - 1, y),
            GetTerrainOnGrid(x + 1, y)
        );
    }

    private Terrain GetTerrainOnGrid(int x, int y)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            return _grid[x, y];
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_grid != null)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_grid[x, y] != null)
                    {
                        if (_grid[x, y] is DesertTerrain)
                        {
                            Gizmos.color = Color.yellow;
                        }
                        else if (_grid[x, y] is ForestTerrain)
                        {
                            Gizmos.color = new Color(23, 97, 33);
                        }
                        else if (_grid[x, y] is GrasslandsTerrain)
                        {
                            Gizmos.color = Color.green;
                        }
                        else if (_grid[x, y] is MontainTerrain)
                        {
                            Gizmos.color = Color.white;
                        }
                        else if (_grid[x, y] is RiverTerrain)
                        {
                            Gizmos.color = Color.blue;
                        }
                        else if (_grid[x, y] is SwampTerrain)
                        {
                            Gizmos.color = new Color(134, 51, 212);
                        }

                        Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one);
                    }
                }
            }
        }
    }
}
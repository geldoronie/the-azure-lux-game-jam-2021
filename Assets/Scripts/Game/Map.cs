using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int gambisX;
    [SerializeField] private int gambisY;
    [Range(1, 100)]
    [SerializeField] private int desertIncidence = 1;
    [Range(1, 100)]
    [SerializeField] private int forestIncidence = 1;
    [Range(1, 100)]
    [SerializeField] private int grasslandIncidence = 1;
    [Range(1, 100)]
    [SerializeField] private int mountainIncidence = 1;
    [Range(1, 100)]
    [SerializeField] private int riverIncidence = 1;
    [Range(1, 100)]
    [SerializeField] private int swampIncidence = 1;

    private Terrain[,] _grid;

    private void Start()
    {
        StartCoroutine(CreateMap());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ApplyCellularAutomata());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(SwampTerrain(new DesertTerrain(gambisX, gambisY)));
        }
    }

    public IEnumerator CreateMap()
    {
        _grid = new Terrain[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _grid[x, y] = null;
            }
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _grid[x, y] = GetRandomTerrain(x, y);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public IEnumerator SwampTerrain(Terrain terrain)
    {
        if (GetTerrainOnGrid(terrain.X, terrain.Y) != null)
        {
            _grid[terrain.X, terrain.Y] = terrain;
            VonNeumannNeighbors neighbors = GetVonNeumannNeighbors(terrain.X, terrain.Y);
            List<Terrain> toVerify = new List<Terrain>();
            if (neighbors._northNeighbor != null) toVerify.Add(neighbors._northNeighbor);
            if (neighbors._eastNeighbor != null) toVerify.Add(neighbors._eastNeighbor);
            if (neighbors._southNeighbor != null) toVerify.Add(neighbors._southNeighbor);
            if (neighbors._westNeighbor != null) toVerify.Add(neighbors._westNeighbor);

            List<TerrainCoordinates> verified = new List<TerrainCoordinates>();
            verified.Add(new TerrainCoordinates(terrain.X, terrain.Y));

            while (toVerify.Count > 0)
            {
                Terrain terrainToVerify = toVerify[0];
                VonNeumannNeighbors neighborsToVerify = GetVonNeumannNeighbors(terrainToVerify.X, terrainToVerify.Y);
                _grid[terrainToVerify.X, terrainToVerify.Y] = terrainToVerify.CheckRules(neighborsToVerify);

                if (neighborsToVerify._northNeighbor != null)
                {
                    TerrainCoordinates northCoord = new TerrainCoordinates(neighborsToVerify._northNeighbor.X, neighborsToVerify._northNeighbor.Y);
                    if (!verified.Contains(northCoord))
                    {
                        verified.Add(northCoord);
                        toVerify.Add(neighborsToVerify._northNeighbor);
                    }
                }
                if (neighborsToVerify._eastNeighbor != null)
                {
                    TerrainCoordinates eastCoord = new TerrainCoordinates(neighborsToVerify._eastNeighbor.X, neighborsToVerify._eastNeighbor.Y);
                    if (!verified.Contains(eastCoord))
                    {
                        verified.Add(eastCoord);
                        toVerify.Add(neighborsToVerify._eastNeighbor);
                    }
                }
                if (neighborsToVerify._southNeighbor != null)
                {
                    TerrainCoordinates southCoord = new TerrainCoordinates(neighborsToVerify._southNeighbor.X, neighborsToVerify._southNeighbor.Y);
                    if (!verified.Contains(southCoord))
                    {
                        verified.Add(southCoord);
                        toVerify.Add(neighborsToVerify._southNeighbor);
                    }
                }
                if (neighborsToVerify._westNeighbor != null)
                {
                    TerrainCoordinates westCoord = new TerrainCoordinates(neighborsToVerify._westNeighbor.X, neighborsToVerify._westNeighbor.Y);
                    if (!verified.Contains(westCoord))
                    {
                        verified.Add(westCoord);
                        toVerify.Add(neighborsToVerify._westNeighbor);
                    }
                }

                verified.Add(new TerrainCoordinates(terrainToVerify.X, terrainToVerify.Y));
                toVerify.Remove(terrainToVerify);

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public IEnumerator ApplyCellularAutomata()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                VonNeumannNeighbors neighbors = GetVonNeumannNeighbors(x, y);
                _grid[x, y] = _grid[x, y].CheckRules(neighbors);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private Terrain GetRandomTerrain(int x, int y)
    {
        int totalIncidence = desertIncidence + forestIncidence + grasslandIncidence + mountainIncidence + riverIncidence + swampIncidence;

        int randomIncidence = Random.Range(0, totalIncidence);
        int currentIncidence = desertIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new DesertTerrain(x, y);
        }
        currentIncidence += forestIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new ForestTerrain(x, y);
        }
        currentIncidence += grasslandIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new GrasslandsTerrain(x, y);
        }
        currentIncidence += mountainIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new MontainTerrain(x, y);
        }
        currentIncidence += riverIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new RiverTerrain(x, y);
        }
        currentIncidence += swampIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new SwampTerrain(x, y);
        }
        return null;
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
                            Gizmos.color = new Color(23f / 255f, 97f / 255f, 33f / 255f);
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
                            Gizmos.color = new Color(134f / 255f, 51f / 255f, 212f / 255f);
                        }

                    }
                    else
                    {
                        Gizmos.color = Color.black;
                    }
                    Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one);
                }
            }
        }
    }
}

public struct TerrainCoordinates
{
    public int x;
    public int y;

    public TerrainCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Map Size")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [Header("Incidences")]
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

    [Header("Prefabs")]
    [SerializeField] private Terrain blankPrefab;
    [SerializeField] private Terrain desertPrefab;
    [SerializeField] private Terrain forestPrefab;
    [SerializeField] private Terrain grasslandPrefab;
    [SerializeField] private Terrain mountainPrefab;
    [SerializeField] private Terrain riverPrefab;
    [SerializeField] private Terrain swampPrefab;

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
    }

    public IEnumerator CreateMap()
    {
        _grid = new Terrain[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _grid[x, y] = null;
                Instantiate(blankPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
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

    public IEnumerator SetTerrainApplyCellularAutomata(TerrainRule newRule, int x, int y)
    {
        if (GetTerrainOnGrid(x, y) != null)
        {
            _grid[x, y] = _grid[x, y];
            Terrain[] neighbors = GetVonNeumannNeighbors(x, y);
            List<Terrain> toVerify = new List<Terrain>();
            foreach (Terrain neighbor in neighbors)
            {
                if (neighbor != null) toVerify.Add(neighbor);
            }

            List<TerrainCoordinates> verified = new List<TerrainCoordinates>();
            verified.Add(new TerrainCoordinates(x, y));

            while (toVerify.Count > 0)
            {
                Terrain terrainToVerify = toVerify[0];
                Terrain[] neighborsToVerify = GetVonNeumannNeighbors(terrainToVerify.X, terrainToVerify.Y);
                SwapTerrainApplyingItsRules(terrainToVerify);

                foreach (Terrain neighborTerrain in neighborsToVerify)
                {
                    if (neighborTerrain != null)
                    {
                        TerrainCoordinates northCoord = new TerrainCoordinates(neighborTerrain.X, neighborTerrain.Y);
                        if (!verified.Contains(northCoord))
                        {
                            verified.Add(northCoord);
                            toVerify.Add(neighborTerrain);
                        }
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
                SwapTerrainApplyingItsRules(_grid[x, y]);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void SwapTerrainApplyingItsRules(Terrain terrain)
    {
        int x = terrain.X;
        int y = terrain.Y;
        Terrain[] neighbors = GetVonNeumannNeighbors(terrain.X, terrain.Y);
        TerrainRule newRule = terrain.TerrainRule.CheckRules(neighbors);

        Destroy(_grid[x, y].gameObject);
        Terrain terrainObject = Instantiate<Terrain>(FindPrefab(newRule), GetWorldCoordinates(x, y), Quaternion.identity);
        terrainObject.Initialize(newRule, x, y);
        _grid[x, y] = terrainObject;
    }

    private Terrain FindPrefab(TerrainRule newRule)
    {
        if (newRule is DesertTerrainRule)
        {
            return desertPrefab;
        }
        else if (newRule is ForestTerrainRule)
        {
            return forestPrefab;
        }
        else if (newRule is GrasslandTerrainRule)
        {
            return grasslandPrefab;
        }
        else if (newRule is MountainTerrainRule)
        {
            return mountainPrefab;
        }
        else if (newRule is RiverTerrainRule)
        {
            return riverPrefab;
        }
        else if (newRule is SwampTerrainRule)
        {
            return swampPrefab;
        }
        return blankPrefab;
    }

    private Vector3 GetWorldCoordinates(int x, int y)
    {
        return new Vector3(x - _width / 2f, 0, y - _height / 2f);
    }

    private Terrain GetRandomTerrain(int x, int y)
    {
        int totalIncidence = desertIncidence + forestIncidence + grasslandIncidence + mountainIncidence + riverIncidence + swampIncidence;

        int randomIncidence = Random.Range(0, totalIncidence);
        int currentIncidence = desertIncidence;
        if (randomIncidence < currentIncidence)
        {
            Terrain terrainObject = Instantiate<Terrain>(desertPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
            terrainObject.Initialize(new DesertTerrainRule(), x, y);
            return terrainObject;
        }
        currentIncidence += forestIncidence;
        if (randomIncidence < currentIncidence)
        {
            Terrain terrainObject = Instantiate<Terrain>(forestPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
            terrainObject.Initialize(new ForestTerrainRule(), x, y);
            return terrainObject;
        }
        currentIncidence += grasslandIncidence;
        if (randomIncidence < currentIncidence)
        {
            Terrain terrainObject = Instantiate<Terrain>(grasslandPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
            terrainObject.Initialize(new GrasslandTerrainRule(), x, y);
            return terrainObject;
        }
        currentIncidence += mountainIncidence;
        if (randomIncidence < currentIncidence)
        {
            Terrain terrainObject = Instantiate<Terrain>(mountainPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
            terrainObject.Initialize(new MountainTerrainRule(), x, y);
            return terrainObject;
        }
        currentIncidence += riverIncidence;
        if (randomIncidence < currentIncidence)
        {
            Terrain terrainObject = Instantiate<Terrain>(riverPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
            terrainObject.Initialize(new RiverTerrainRule(), x, y);
            return terrainObject;
        }

        currentIncidence += swampIncidence;
        if (randomIncidence < currentIncidence)
        {
            Terrain terrainObject = Instantiate<Terrain>(swampPrefab, GetWorldCoordinates(x, y), Quaternion.identity);
            terrainObject.Initialize(new SwampTerrainRule(), x, y);
            return terrainObject;
        }
        return null;
    }

    private Terrain[] GetVonNeumannNeighbors(int x, int y)
    {
        Terrain[] neighbors = new Terrain[4];
        neighbors[0] = GetTerrainOnGrid(x, y + 1);
        neighbors[1] = GetTerrainOnGrid(x, y - 1);
        neighbors[2] = GetTerrainOnGrid(x - 1, y);
        neighbors[3] = GetTerrainOnGrid(x + 1, y);
        return neighbors;
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
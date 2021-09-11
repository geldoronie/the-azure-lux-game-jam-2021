using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Map Size")]
    [SerializeField] private int _width = 15;
    [SerializeField] private int _height = 15;
    [SerializeField] private float _rotationSpeed = 60f;

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
    [SerializeField] private Transform castleTransform;
    [SerializeField] private GameObject cornerTowerPrefab;
    [SerializeField] private GameObject middleWallPrefab;
    [SerializeField] private Terrain blankPrefab;
    [SerializeField] private Terrain desertPrefab;
    [SerializeField] private Terrain forestPrefab;
    [SerializeField] private Terrain grasslandPrefab;
    [SerializeField] private Terrain mountainPrefab;
    [SerializeField] private Terrain riverPrefab;
    [SerializeField] private Terrain swampPrefab;

    private Terrain[,] _grid;
    private int maxIterations;
    private int iterations;

    private void Start()
    {
        StartCoroutine(CreateMap());
    }

    public IEnumerator CreateMap()
    {
        _grid = new Terrain[_width, _height];
        maxIterations = _width * _height + (int)(_width / 3) * _width * _height;
        iterations = 0;
        Debug.Log("Começou: " + iterations + "/" + maxIterations);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Terrain newTerrain = Instantiate<Terrain>(blankPrefab, GetWorldCoordinates(x, y), Quaternion.identity, transform);
                newTerrain.Initialize(null, x, y);
                _grid[x, y] = newTerrain;

                if (x == 0)
                {
                    Instantiate(middleWallPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 180, 0), castleTransform);

                    if (y == 0)
                    {
                        Instantiate(cornerTowerPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 0, 0), castleTransform);
                    }

                    if (y == _height - 1)
                    {
                        Instantiate(cornerTowerPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 90, 0), castleTransform);
                    }
                }

                if (y == 0)
                {
                    Instantiate(middleWallPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 90, 0), castleTransform);
                }

                if (x == _width - 1)
                {
                    Instantiate(middleWallPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 0, 0), castleTransform);

                    if (y == 0)
                    {
                        Instantiate(cornerTowerPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 270, 0), castleTransform);
                    }

                    if (y == _height - 1)
                    {
                        Instantiate(cornerTowerPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 180, 0), castleTransform);
                    }
                }

                if (y == _height - 1)
                {
                    Instantiate(middleWallPrefab, GetWorldCoordinates(x, y), Quaternion.Euler(0, 270, 0), castleTransform);
                }

            }
        }

        Coroutine c = null;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                StartCoroutine(SwapTerrain(GetRandomTerrainRule(), x, y));
                iterations++;
                yield return new WaitForEndOfFrame();
            }

            if (x > 2 && x % 3 == 0)
            {
                c = StartCoroutine(ApplyCellularAutomata(() => { iterations++; }));
            }
        }

        yield return c;

        Debug.Log("Acabou: " + iterations + "/" + maxIterations);

        maxIterations = 0;
        iterations = 0;
    }

    private Coroutine applyingCellularAutomataCoroutine;

    public void SetTerrainApplyCellularAutomata(TerrainRule newRule, int x, int y)
    {
        if (applyingCellularAutomataCoroutine == null)
        {
            applyingCellularAutomataCoroutine = StartCoroutine(SetTerrainApplyCellularAutomataCoroutine(newRule, x, y));
        }
        else
        {
            Debug.LogError("Another SetTerrainApplyCellularAutomata is running");
        }
    }

    private IEnumerator SetTerrainApplyCellularAutomataCoroutine(TerrainRule newRule, int x, int y)
    {
        if (GetTerrainOnGrid(x, y) != null)
        {
            _grid[x, y] = _grid[x, y];
            Terrain[] neighbors = GetVonNeumannNeighbors(x, y);
            List<Terrain> toVerify = new List<Terrain>();
            yield return StartCoroutine(SwapTerrain(newRule, x, y));
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
                yield return StartCoroutine(SwapTerrain(terrainToVerify));

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

            applyingCellularAutomataCoroutine = null;
        }
    }

    public delegate void AddIteration();

    public IEnumerator ApplyCellularAutomata(AddIteration AddIteration = null)
    {
        Coroutine c = null;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                c = StartCoroutine(SwapTerrain(_grid[x, y]));
                AddIteration?.Invoke();
                yield return new WaitForEndOfFrame();
            }
        }
        yield return c;
    }

    private IEnumerator SwapTerrain(Terrain terrain)
    {
        if (terrain.TerrainRule != null)
        {
            int x = terrain.X;
            int y = terrain.Y;
            Terrain[] neighbors = GetVonNeumannNeighbors(terrain.X, terrain.Y);
            TerrainRule newRule = terrain.TerrainRule.CheckRules(neighbors);

            yield return StartCoroutine(SwapTerrain(newRule, x, y));
        }
    }

    private IEnumerator SwapTerrain(TerrainRule newRule, int x, int y)
    {
        if (newRule.GetType().ToString() != _grid[x, y].TerrainRule?.GetType().ToString())
        {
            Terrain terrainObject = Instantiate<Terrain>(FindPrefab(newRule), GetWorldCoordinates(x, y), Quaternion.Euler(-180, 0, 0), transform);
            terrainObject.Initialize(newRule, x, y);

            float timeElapsed = -180;
            while (terrainObject.transform.eulerAngles.x != 0)
            {
                timeElapsed -= Time.deltaTime * _rotationSpeed;
                terrainObject.transform.eulerAngles = new Vector3(timeElapsed, 0, 0);
                _grid[x, y].gameObject.transform.eulerAngles = new Vector3(timeElapsed + 180, 0, 0);
                if (timeElapsed < -360)
                {
                    terrainObject.transform.eulerAngles = Vector3.zero;
                }
                yield return new WaitForEndOfFrame();
            }

            Destroy(_grid[x, y].gameObject);
            _grid[x, y] = terrainObject;
        }

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

    private TerrainRule GetRandomTerrainRule()
    {
        int totalIncidence = desertIncidence + forestIncidence + grasslandIncidence + mountainIncidence + riverIncidence + swampIncidence;

        int randomIncidence = Random.Range(0, totalIncidence);
        int currentIncidence = desertIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new DesertTerrainRule();
        }
        currentIncidence += forestIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new ForestTerrainRule();
        }
        currentIncidence += grasslandIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new GrasslandTerrainRule();
        }
        currentIncidence += mountainIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new MountainTerrainRule();
        }
        currentIncidence += riverIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new RiverTerrainRule();
        }

        currentIncidence += swampIncidence;
        if (randomIncidence < currentIncidence)
        {
            return new SwampTerrainRule();
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
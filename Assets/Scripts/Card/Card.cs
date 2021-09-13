using System;
using UnityEngine;

[Serializable]
public class Card
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string imageId;
    [SerializeField] private string prefabId;
    [SerializeField] private ResourcesAmounts useCost;
    [SerializeField] private TerrainCost terrainCost;
    [SerializeField] private GameObject _prefab;

    public Card(string name, string description, string imageId, string prefabId, ResourcesAmounts useCost, TerrainCost terrainCost)
    {
        this.name = name;
        this.description = description;
        this.imageId = imageId;
        this.prefabId = prefabId;
        this.useCost = useCost;
        this.terrainCost = terrainCost;
    }

    public virtual bool CanUse(Player player, Terrain terrain)
    {
        bool check =
            terrainCost.Desert && terrain.TerrainRule is DesertTerrainRule ||
            terrainCost.Forest && terrain.TerrainRule is ForestTerrainRule ||
            terrainCost.Grassland && terrain.TerrainRule is GrasslandTerrainRule ||
            terrainCost.Mountain && terrain.TerrainRule is MountainTerrainRule ||
            terrainCost.River && terrain.TerrainRule is RiverTerrainRule ||
            terrainCost.Swamp && terrain.TerrainRule is SwampTerrainRule;
        if (!check) return false;

        return
            player.Resources.Wood >= UseCost.Wood &&
            player.Resources.Stone >= UseCost.Stone &&
            player.Resources.Gold >= UseCost.Gold &&
            player.Resources.Food >= UseCost.Food &&
            player.Resources.People >= UseCost.People &&
            player.Resources.Military >= UseCost.Military;
    }

    public string Name { get => name; }
    public string Description { get => description; }
    public string ImageId { get => imageId; }
    public string PrefabId { get => prefabId; }
    public ResourcesAmounts UseCost { get => useCost; }
    public TerrainCost TerrainCost { get => terrainCost; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}

[System.Serializable]
public class ResourcesAmounts
{
    [SerializeField] private int wood;
    [SerializeField] private int stone;
    [SerializeField] private int gold;
    [SerializeField] private int food;
    [SerializeField] private int people;
    [SerializeField] private int military;

    public ResourcesAmounts(int wood, int stone, int gold, int food, int people, int military)
    {
        this.wood = wood;
        this.stone = stone;
        this.gold = gold;
        this.food = food;
        this.people = people;
        this.military = military;
    }

    public ResourcesAmounts()
    {
        wood = 0;
        stone = 0;
        gold = 0;
        food = 0;
        people = 0;
        military = 0;
    }

    public static ResourcesAmounts operator +(ResourcesAmounts a, ResourcesAmounts b)
            => new ResourcesAmounts(
                a.Wood + b.Wood,
                a.Stone + b.Stone,
                a.Gold + b.Gold,
                a.Food + b.Food,
                a.People + b.People,
                a.Military + b.Military
            );

    public static ResourcesAmounts operator -(ResourcesAmounts a, ResourcesAmounts b)
            => new ResourcesAmounts(
                a.Wood - b.Wood,
                a.Stone - b.Stone,
                a.Gold - b.Gold,
                a.Food - b.Food,
                a.People - b.People,
                a.Military - b.Military
            );

    public int Wood { get => wood; }
    public int Stone { get => stone; }
    public int Gold { get => gold; }
    public int Food { get => food; }
    public int People { get => people; }
    public int Military { get => military; }
}

[System.Serializable]
public class TerrainCost
{
    [SerializeField] private bool grassland;
    [SerializeField] private bool desert;
    [SerializeField] private bool forest;
    [SerializeField] private bool river;
    [SerializeField] private bool swamp;
    [SerializeField] private bool mountain;

    public TerrainCost(bool grassland, bool desert, bool forest, bool river, bool swamp, bool mountain)
    {
        this.grassland = grassland;
        this.desert = desert;
        this.forest = forest;
        this.river = river;
        this.swamp = swamp;
        this.mountain = mountain;
    }

    public bool Grassland { get => grassland; }
    public bool Desert { get => desert; }
    public bool Forest { get => forest; }
    public bool River { get => river; }
    public bool Swamp { get => swamp; }
    public bool Mountain { get => mountain; }
}

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

    public Card(string name, string description, string imageId, string prefabId, ResourcesAmounts useCost)
    {
        this.name = name;
        this.description = description;
        this.imageId = imageId;
        this.prefabId = prefabId;
        this.useCost = useCost;
    }

    public string Name { get => name; }
    public string Description { get => description; }
    public string ImageId { get => imageId; }
    public string PrefabId { get => prefabId; }
    public ResourcesAmounts UseCost { get => useCost; }

    public bool CostCheck(Player player)
    {
        return
            player.WoodAmount >= UseCost.wood &&
            player.StoneAmount >= UseCost.stone &&
            player.GoldAmount >= UseCost.gold &&
            player.FoodAmount >= UseCost.food &&
            player.PeopleAmount >= UseCost.people &&
            player.MilitaryAmount >= UseCost.military;

    }
}

[System.Serializable]
public class ResourcesAmounts
{
    public int wood;
    public int stone;
    public int gold;
    public int food;
    public int people;
    public int military;
}

[System.Serializable]
public class TerrainCost
{
    public bool grassland;
    public bool desert;
    public bool forest;
    public bool river;
    public bool swamp;
    public bool mountain;
}

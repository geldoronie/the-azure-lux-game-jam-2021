using UnityEngine;

public class Card : ScriptableObject
{
    [Header("Generic")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [Header("Costs")]
    [SerializeField] private int woodCost = 0;
    [SerializeField] private int stoneCost = 0;
    [SerializeField] private int goldCost = 0;
    [SerializeField] private int foodCost = 0;
    [SerializeField] private int peopleCost = 0;
    [SerializeField] private int militaryCost = 0;

    public string Name { get => _name; }
    public string Description { get => _description; }
    public int WoodCost { get => woodCost; }
    public int StoneCost { get => stoneCost; }
    public int GoldCost { get => goldCost; }
    public int FoodCost { get => foodCost; }
    public int PeopleCost { get => peopleCost; }
    public int MilitaryCost { get => militaryCost; }
}

public enum Resource
{
    Wood,
    Stone,
    Gold,
    Food,
    People,
    Military
}

[System.Serializable]
public class ResourceAmount
{
    public Resource resource;
    public int amount;
}
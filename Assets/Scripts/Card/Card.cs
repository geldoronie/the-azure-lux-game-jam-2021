using UnityEngine;

public class Card : ScriptableObject
{
    [Header("Generic")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [Header("Costs")]
    [SerializeField] private ResourceAmount[] cost;
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
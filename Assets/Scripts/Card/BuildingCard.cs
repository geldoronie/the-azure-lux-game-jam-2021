using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "GameJam/Cards/Building")]
public class BuildingCard : Card
{
    [Header("Resources Per Turn")]
    [SerializeField] private ResourceAmount[] resoucePerTurn;

    [SerializeField] private CardTerrainCost[] terrainCost;
}

public enum CardTerrainCost
{
    Desert,
    Forest,
    Grasslands,
    Montain,
    River,
    Swamp
}
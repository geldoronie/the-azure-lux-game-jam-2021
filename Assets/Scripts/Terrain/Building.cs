using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingCard _card;
    private Terrain _terrain;

    public void Initialize(BuildingCard card, Terrain terrain)
    {
        _card = card;
        _terrain = terrain;
    }

    public BuildingCard Card { get => _card; }
    public Terrain Terrain { get => _terrain; }
}
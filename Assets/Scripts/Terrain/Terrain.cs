using UnityEngine;

public class Terrain : MonoBehaviour
{
    private int _x;
    private int _y;
    private TerrainRule _terrainRule;
    private Building _building;

    public void Initialize(TerrainRule terrainRule, int x, int y, Building building)
    {
        _x = x;
        _y = y;
        _terrainRule = terrainRule;
        _building = building;
    }

    public void ConstructBuilding(BuildingCard buildingCard)
    {
        if (_building != null)
        {
            Debug.LogError("Error! A building cannot be constructed on top of another building");
            return;
        }
        else
        {
            Building constructedBuilding = Instantiate<Building>(buildingCard.Prefab, transform.position, Quaternion.identity);
            constructedBuilding.Initialize(buildingCard, this);
            _building = constructedBuilding;
            GameModeBase.Instance.Player.WoodAmount -= buildingCard.UseCost.Wood;
            GameModeBase.Instance.Player.StoneAmount -= buildingCard.UseCost.Stone;
            GameModeBase.Instance.Player.GoldAmount -= buildingCard.UseCost.Gold;
            GameModeBase.Instance.Player.FoodAmount -= buildingCard.UseCost.Food;
            GameModeBase.Instance.Player.PeopleAmount -= buildingCard.UseCost.People;
            GameModeBase.Instance.Player.MilitaryAmount -= buildingCard.UseCost.Military;
        }
    }

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public TerrainRule TerrainRule { get => _terrainRule; }
    public Building Building { get => _building; }
}
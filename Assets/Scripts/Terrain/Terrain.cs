using UnityEngine;

public class Terrain : MonoBehaviour
{
    private int _x;
    private int _y;
    private TerrainRule _terrainRule;
    private Building _building;
    private int _turnsAlive;

    public void Initialize(TerrainRule terrainRule, int x, int y, Building building)
    {
        _x = x;
        _y = y;
        _terrainRule = terrainRule;
        _building = building;
        if (building != null)
        {
            building.UpdatePosition(terrainRule);
        }
        _turnsAlive = 0;
        GameModeBase.Instance.OnChangeTurn += NextTurn;
    }

    private void OnDestroy()
    {
        GameModeBase.Instance.OnChangeTurn -= NextTurn;
    }

    public void NextTurn()
    {
        _turnsAlive++;
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
            Building buildingPrefab = buildingCard.Prefab.GetComponent<Building>();
            Building constructedBuilding = Instantiate<Building>(buildingPrefab, transform.position, Quaternion.identity);
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

    public ToolTipInformation GetTooltip()
    {
        string content = "Building(s): None";
        if (_building != null)
        {
            content = _building.Card.Name + " (" + TurnsAlive + ")\n";
            content += "Resources per turn:\n";
            content += " - Food: " + _building.Card.ResourcesPerTurn.Food + "\n";
            content += " - Wood: " + _building.Card.ResourcesPerTurn.Wood + "\n";
            content += " - Gold: " + _building.Card.ResourcesPerTurn.Gold + "\n";
            content += " - Stone: " + _building.Card.ResourcesPerTurn.Stone + "\n";
            content += " - People: " + _building.Card.ResourcesPerTurn.People + "\n";
            content += " - Military: " + _building.Card.ResourcesPerTurn.Military;
        }

        Terrain[] neighbors = GameModeBase.Instance.Map.GetVonNeumannNeighbors(X, Y);
        TerrainRule newRule = TerrainRule.CheckRules(neighbors);
        if (newRule.GetType().ToString() != this.GetType().ToString())
        {
            content += "\n\nThis terrain should change to: " + newRule.GetTooltip();
        }

        return new ToolTipInformation(
            TerrainRule?.GetTooltip(),
            content
        );
    }

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public TerrainRule TerrainRule { get => _terrainRule; }
    public Building Building { get => _building; }
    public int TurnsAlive { get => _turnsAlive / 2; }
}
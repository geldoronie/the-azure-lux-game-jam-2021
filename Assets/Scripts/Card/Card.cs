using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Card
{
    [Header("Generic")]
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected CardType _type;

    [Header("Costs")]
    [SerializeField] protected int _woodCost = 0;
    [SerializeField] protected int _stoneCost = 0;
    [SerializeField] protected int _goldCost = 0;
    [SerializeField] protected int _foodCost = 0;
    [SerializeField] protected int _peopleCost = 0;
    [SerializeField] protected int _militaryCost = 0;

    public string Name { get => this._name; }
    public string Description { get => this._description; }
    public int WoodCost { get => this._woodCost; }
    public int StoneCost { get => this._stoneCost; }
    public int GoldCost { get => this._goldCost; }
    public int FoodCost { get => this._foodCost; }
    public int PeopleCost { get => this._peopleCost; }
    public int MilitaryCost { get => this._militaryCost; }

    public Card(string name, string description, CardType type, List<ResourceAmount> costs)
    {
        this._name = name;
        this._description = description;
        this._type = type;

        costs.ForEach( cost => {
            switch (cost.resource)
            {
                case Resource.Food:
                    this._foodCost = cost.amount;
                    break;
                case Resource.Gold:
                    this._goldCost = cost.amount;
                    break;
                case Resource.Military:
                    this._militaryCost = cost.amount;
                    break;
                case Resource.People:
                    this._peopleCost = cost.amount;
                    break;
                case Resource.Stone:
                    this._stoneCost = cost.amount;
                    break;
                case Resource.Wood:
                    this._woodCost = cost.amount;
                    break;
                default:
                    Debug.Log("Card Cost unknow!");
                    break;
            }
        });
    }
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

public enum CardType {
    Building,
    Effect
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BuildingCardModelPlayfab
{
    public string name;

    public string description;

    public string imageId;
    public List<CostModelPlayfab> cost;

    public List<CostModelPlayfab> resourcePerTurn;

    public List<TerrainCostModelPlayfab> terrainCost;
}
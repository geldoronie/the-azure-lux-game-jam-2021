using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EffectCardModelPlayfab
{
    public string name;

    public string description;

    public string imageId;
    public List<CostModelPlayfab> cost;

    public EffectModelPlayfab effect;
}
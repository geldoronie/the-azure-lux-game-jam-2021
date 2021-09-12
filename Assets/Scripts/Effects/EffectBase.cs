using System.Collections.Generic;
using UnityEngine;


public class EffectBase
{
    [SerializeField] public string type;
    [SerializeField] public List<EffectArgument> arguments;
}

public class EffectArgument
{
    [SerializeField] public EffectArgumentType type;
    [SerializeField] public string value;
}

public enum EffectArgumentType
{
    Terrain,
    Resource
}
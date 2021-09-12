using UnityEngine;
using System;

[Serializable]
public class EffectCard : Card
{
    [SerializeField] private string effectId;
    [SerializeField] private EffectBase _effect;

    public EffectCard(string name, string description, string imageId, string prefabId, ResourcesAmounts useCost, TerrainCost terrainCost, string effectId) : base(name, description, imageId, prefabId, useCost, terrainCost)
    {
        this.effectId = effectId;
    }

    public string EffectId { get => effectId; }
    public EffectBase Effect { get => _effect; set => _effect = value; }
}
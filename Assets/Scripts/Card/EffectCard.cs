using UnityEngine;
using System;

[Serializable]
public class EffectCard : Card
{
    [SerializeField] public EffectBase effect;

    public EffectCard(string name, string description, string imageId, string prefabId, ResourcesAmounts useCost, EffectBase effect) : base(name, description, imageId, prefabId, useCost)
    {
        this.effect = effect;
    }
}
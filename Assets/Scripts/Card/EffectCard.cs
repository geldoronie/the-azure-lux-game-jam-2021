using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EffectCard : Card { 

    [SerializeField] private EffectBase effect;

    public EffectCard(string name, string description, List<ResourceAmount> costs, EffectBase effect)
        : base(name, description, CardType.Effect, costs)
    {
        this.effect = effect;
    }
}
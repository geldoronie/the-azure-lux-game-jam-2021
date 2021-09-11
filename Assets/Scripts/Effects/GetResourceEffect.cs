using UnityEngine;

[CreateAssetMenu(fileName = "Get Resource", menuName = "GameJam/Cards/Effect Cards/Get Resource")]
public class GetResourceEffect : EffectBase
{
    [SerializeField] public ResourceAmount[] resourcesToGain;
}
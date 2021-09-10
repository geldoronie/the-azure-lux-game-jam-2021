using UnityEngine;

[CreateAssetMenu(fileName = "Get Resource", menuName = "GameJam/Cards/Effect Cards/Get Resource")]
public class GetResourceEffectCard : EffectCard
{
    [SerializeField] private ResourceAmount[] resourceToGain;
}
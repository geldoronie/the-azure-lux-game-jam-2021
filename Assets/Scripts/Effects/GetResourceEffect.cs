using UnityEngine;

[CreateAssetMenu(fileName = "Get Resource Effect", menuName = "Game Jam/Effects/Get Resource")]
public class GetResourceEffect : EffectBase
{
    [SerializeField] private ResourcesAmounts _resourcesToGive;

    public override void UseEffect(Player player, Terrain terrain)
    {
        player.Resources += _resourcesToGive;
    }

    public ResourcesAmounts ResourcesToGive { get => _resourcesToGive; }
}
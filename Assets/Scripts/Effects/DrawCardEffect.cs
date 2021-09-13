using UnityEngine;


[CreateAssetMenu(fileName = "Draw Card Effect", menuName = "Game Jam/Effects/Draw Card", order = 0)]
public class DrawCardEffect : EffectBase
{
    [SerializeField] private int _cardToGive;
    [SerializeField] private CardTypeToGive _cardTypeToGive;

    public override void UseEffect(Player player, Terrain terrain)
    {
        switch (_cardTypeToGive)
        {
            case CardTypeToGive.Building:
                player.DrawCard(_cardToGive, 1, 0); break;
            case CardTypeToGive.Effect:
                player.DrawCard(_cardToGive, 0, 1); break;
            default:
                player.DrawCard(_cardToGive); break;
        }

    }

    public CardTypeToGive CardTypeToGive { get => _cardTypeToGive; }
    public int CardToGive { get => _cardToGive; }
}

public enum CardTypeToGive
{
    Any,
    Building,
    Effect
}
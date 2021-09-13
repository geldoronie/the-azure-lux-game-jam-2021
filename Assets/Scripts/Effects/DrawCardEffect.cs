using UnityEngine;


[CreateAssetMenu(fileName = "Draw Card Effect", menuName = "Game Jam/Effects/Draw Card", order = 0)]
public class DrawCardEffect : EffectBase
{
    [SerializeField] private int _cardToGive;
    [SerializeField] private CardTypeToGive _cardTypeToGive;

    public override void UseEffect(Player player, Terrain terrain)
    {
        player.DrawCard(_cardToGive, _cardTypeToGive);
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
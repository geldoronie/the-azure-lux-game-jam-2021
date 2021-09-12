using UnityEngine;
using TMPro;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _cardNameText;
    [SerializeField] private TMP_Text _cardDescriptionText;
    [SerializeField] private TMP_Text _woodCostText;
    [SerializeField] private TMP_Text _stoneCostText;
    [SerializeField] private TMP_Text _goldCostText;
    [SerializeField] private TMP_Text _foodCostText;
    [SerializeField] private TMP_Text _peopleCostText;
    [SerializeField] private TMP_Text _militaryCostText;

    private Card card;

    public void Initialize(Card card)
    {
        this.card = card;
        _cardNameText.SetText(card.Name);
        _cardDescriptionText.SetText(card.Description);
        _woodCostText.SetText(card.WoodCost.ToString("00"));
        _stoneCostText.SetText(card.StoneCost.ToString("00"));
        _goldCostText.SetText(card.GoldCost.ToString("00"));
        _foodCostText.SetText(card.FoodCost.ToString("00"));
        _peopleCostText.SetText(card.PeopleCost.ToString("00"));
        _militaryCostText.SetText(card.MilitaryCost.ToString("00"));
    }
    public Card Card { get => card; }
}

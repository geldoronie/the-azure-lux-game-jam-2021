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
        _woodCostText.SetText(card.UseCost.Wood.ToString("00"));
        _stoneCostText.SetText(card.UseCost.Stone.ToString("00"));
        _goldCostText.SetText(card.UseCost.Gold.ToString("00"));
        _foodCostText.SetText(card.UseCost.Food.ToString("00"));
        _peopleCostText.SetText(card.UseCost.People.ToString("00"));
        _militaryCostText.SetText(card.UseCost.Military.ToString("00"));
    }
    public Card Card { get => card; }
}

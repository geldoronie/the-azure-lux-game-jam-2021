using UnityEngine;
using TMPro;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;
    [SerializeField] private TMP_Text woodCostText;
    [SerializeField] private TMP_Text stoneCostText;
    [SerializeField] private TMP_Text goldCostText;
    [SerializeField] private TMP_Text foodCostText;
    [SerializeField] private TMP_Text peopleCostText;
    [SerializeField] private TMP_Text militaryCostText;

    private Card card;

    public void Initialize(Card card)
    {
        cardNameText.SetText(card.Name);
        cardDescriptionText.SetText(card.Description);
        woodCostText.SetText(card.WoodCost.ToString("00"));
        stoneCostText.SetText(card.StoneCost.ToString("00"));
        goldCostText.SetText(card.GoldCost.ToString("00"));
        foodCostText.SetText(card.FoodCost.ToString("00"));
        peopleCostText.SetText(card.PeopleCost.ToString("00"));
        militaryCostText.SetText(card.MilitaryCost.ToString("00"));
    }
    public Card Card { get => card; }
}

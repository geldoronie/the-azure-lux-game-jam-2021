using UnityEngine;

[ExecuteInEditMode]
public class CardOrganizerGUI : MonoBehaviour
{
    [SerializeField] private int cardWidth = 100;
    [SerializeField] private float cardHeighGap = 9.3f;
    [SerializeField] private float cardSelectedGap = 80;
    [SerializeField] private float sizePerCard = 9.3f;

    private RectTransform rectTransform;
    private Canvas canvas;
    private int selectedCardId;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<Canvas>();
    }

    private void Update()
    {
        UpdateGUI();
        CheckSelectionCard();
    }

    private void CheckSelectionCard()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 panelPosition = rectTransform.anchoredPosition * canvas.scaleFactor;
        Vector3 panelSize = panelPosition + (Vector3)rectTransform.sizeDelta * canvas.scaleFactor;

        if (mousePosition.x >= panelPosition.x && mousePosition.x < panelSize.x &&
            mousePosition.y >= panelPosition.y && mousePosition.y < panelSize.y)
        {
            selectedCardId = Mathf.RoundToInt((mousePosition.x / panelSize.x) * transform.childCount) - 1;
        }
        else
        {
            selectedCardId = -1;
        }
    }

    private void UpdateGUI()
    {
        rectTransform.sizeDelta = new Vector2(transform.childCount * sizePerCard, rectTransform.sizeDelta.y);
        float gap = (rectTransform.sizeDelta.x - cardWidth * transform.childCount) / transform.childCount;
        float angle = ((2 * Mathf.PI * rectTransform.sizeDelta.x) / 360) / transform.childCount;
        int oddOrEven = 1 - transform.childCount % 2;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();
            float shiftedI = (i - transform.childCount / 2);
            float selectedI = i - selectedCardId;

            // X position
            float gapOddOrEven = (cardWidth / 2f + gap / 2f) * oddOrEven;
            float gapSelected = selectedCardId != -1 ? selectedI * cardSelectedGap : 0;
            float gapSelectedCompensation = selectedI != 0 ? Mathf.Sin(1 / Mathf.Abs(selectedI)) : 0;
            float xPos = gapOddOrEven + gapSelected * gapSelectedCompensation + gap * shiftedI + cardWidth * shiftedI;

            // Y position
            float yOddOrEven = ((shiftedI < 0 ? 1 : 0) * cardHeighGap) * oddOrEven;
            float ySelected = selectedCardId == i ? cardHeighGap * 3 : 0;
            float yPos = yOddOrEven + ySelected - Mathf.Abs(shiftedI) * cardHeighGap;

            // Set position
            childRectTransform.anchoredPosition = new Vector2(xPos, yPos);

            // Rotation
            float angleOddOrEven = (-angle / 2f) * oddOrEven;
            childRectTransform.rotation = Quaternion.Euler(0, 0, angleOddOrEven - angle * shiftedI);
        }

    }
}

using UnityEngine;
using UnityEngine.Events;

public class CardOrganizerGUI : MonoBehaviour
{
    [SerializeField] private CardDisplayer _selectedCardHolder;
    [SerializeField] private CardDisplayer _cardPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private int _cardWidth = 100;
    [SerializeField] private float _cardHeighGap = 9.3f;
    [SerializeField] private float _cardSelectedGap = 20;
    [SerializeField] private float _sizePerCard = 82.36f;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private int _hoveredCardId;
    private CardDisplayer _hoveredCard;
    private CardDisplayer _selectedCard;

    public UnityAction<Card> OnSelectCard;
    public UnityAction OnDeselectCard;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = transform.parent.GetComponent<Canvas>();
        _player.OnHandCardsUpdate += OnHandCardsUpdate;
    }

    private void OnDestroy()
    {
        _player.OnHandCardsUpdate -= OnHandCardsUpdate;
    }

    private void Update()
    {
        UpdateGUI();
        CheckSelectionCard();

        if (_hoveredCard != null && Input.GetMouseButtonDown(0))
        {
            UnselectCard();

            _selectedCard = _hoveredCard;
            _selectedCard.gameObject.SetActive(false);
            _selectedCardHolder.Initialize(_hoveredCard.Card);
            _selectedCardHolder.gameObject.SetActive(true);
            _hoveredCard = null;

            OnSelectCard?.Invoke(_selectedCard.Card);
        }
        if (Input.GetMouseButtonDown(1)) UnselectCard();
    }

    public void UnselectCard()
    {
        if (_selectedCard != null)
        {
            _selectedCardHolder.gameObject.SetActive(false);
            _selectedCard.gameObject.SetActive(true);
            _selectedCard = null;

            OnDeselectCard?.Invoke();
        }
    }

    private void CheckSelectionCard()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 panelPosition = _rectTransform.anchoredPosition * _canvas.scaleFactor;
        Vector3 panelSize = panelPosition + (Vector3)_rectTransform.sizeDelta * _canvas.scaleFactor;

        if (mousePosition.x >= panelPosition.x && mousePosition.x < panelSize.x &&
            mousePosition.y >= panelPosition.y && mousePosition.y < panelSize.y)
        {
            int hoverIdWithActiveChildren = Mathf.RoundToInt((mousePosition.x / panelSize.x) * GetActiveChildren()) - 1;

            int normalHoverId = -1;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    normalHoverId++;
                }
                if (normalHoverId == hoverIdWithActiveChildren)
                {
                    _hoveredCard = transform.GetChild(i).gameObject.GetComponent<CardDisplayer>();
                    _hoveredCardId = hoverIdWithActiveChildren;
                    break;
                }
            }
        }
        else
        {
            _hoveredCard = null;
            _hoveredCardId = -1;
        }
    }

    private int GetActiveChildren()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                childCount--;
            }
        }
        return childCount;
    }

    private void UpdateGUI()
    {
        int childCount = GetActiveChildren();
        _rectTransform.sizeDelta = new Vector2(childCount * _sizePerCard, _rectTransform.sizeDelta.y);
        float gap = (_rectTransform.sizeDelta.x - _cardWidth * childCount) / childCount;
        float angle = ((2 * Mathf.PI * _rectTransform.sizeDelta.x) / 360) / childCount;
        int oddOrEven = 1 - childCount % 2;
        int actualI = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();
                float shiftedI = (actualI - childCount / 2);
                float hoverI = actualI - _hoveredCardId;

                // X position
                float gapOddOrEven = (_cardWidth / 2f + gap / 2f) * oddOrEven;
                float gapHovered = _hoveredCard != null ? hoverI * _cardSelectedGap : 0;
                float gapHoveredCompensation = hoverI != 0 ? Mathf.Sin(1 / Mathf.Abs(hoverI)) : 0;
                float xPos = gapOddOrEven + gapHovered * gapHoveredCompensation + gap * shiftedI + _cardWidth * shiftedI;

                // Y position
                float yOddOrEven = ((shiftedI < 0 ? 1 : 0) * _cardHeighGap) * oddOrEven;
                float yHovered = _hoveredCardId == actualI ? _cardHeighGap * 3 : 0;
                float yPos = yOddOrEven + yHovered - Mathf.Abs(shiftedI) * _cardHeighGap;

                // Set position
                childRectTransform.anchoredPosition = new Vector2(xPos, yPos);

                // Rotation
                float angleOddOrEven = (-angle / 2f) * oddOrEven;
                childRectTransform.rotation = Quaternion.Euler(0, 0, angleOddOrEven - angle * shiftedI);
                actualI++;
            }
        }
    }

    private void _addHandCard(Card card)
    {
        CardDisplayer newCard = Instantiate<CardDisplayer>(_cardPrefab, transform);
        newCard.Initialize(card);
    }

    private void _clearHandCards()
    {
        UnselectCard();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void OnHandCardsUpdate()
    {
        this._clearHandCards();
        this._player.Hand.ForEach(card =>
        {
            this._addHandCard(card);
        });
    }

    public CardDisplayer SelectedCard { get => _selectedCard; }
}

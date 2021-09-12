using System.Collections.Generic;
using UnityEngine;

public class CardOrganizerGUI : MonoBehaviour
{
    [SerializeField] private CardsLibrary _library;
    [SerializeField] private CardDisplayer _cardPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private int _cardWidth = 100;
    [SerializeField] private float _cardHeighGap = 9.3f;
    [SerializeField] private float _cardSelectedGap = 20;
    [SerializeField] private float _sizePerCard = 82.36f;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private int _selectedId = -1;
    private CardDisplayer _selectedCard;
    private int _hoverCardId = -1;

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

        if (_hoverCardId != -1 && Input.GetMouseButtonDown(0))
        {
            UnselectCard();

            Destroy(transform.GetChild(_hoverCardId).gameObject);

            Vector3 panelPosition = _rectTransform.anchoredPosition;
            Vector3 panelSize = panelPosition + (Vector3)_rectTransform.sizeDelta;

            _selectedId = _hoverCardId;
            Vector2 cardPos = new Vector2(panelPosition.x, panelSize.y + _cardHeighGap * _cardHeighGap);
            CardDisplayer oldCard = transform.GetChild(_hoverCardId).GetComponent<CardDisplayer>();

            _selectedCard = Instantiate<CardDisplayer>(_cardPrefab, cardPos, Quaternion.identity, _canvas.transform);
            _selectedCard.Initialize(oldCard.Card);

            RectTransform newCardRectTransform = _selectedCard.GetComponent<RectTransform>();

            newCardRectTransform.anchorMin = Vector2.zero;
            newCardRectTransform.anchorMax = Vector2.zero;
            newCardRectTransform.pivot = Vector2.zero;
            newCardRectTransform.anchoredPosition = cardPos;
        }
        if (Input.GetMouseButtonDown(1)) UnselectCard();
    }

    private void UnselectCard()
    {
        if (_selectedCard != null)
        {
            CardDisplayer oldCard = Instantiate<CardDisplayer>(_cardPrefab, transform);
            oldCard.Initialize(_selectedCard.Card);
            oldCard.transform.SetSiblingIndex(_selectedId);
            Destroy(_selectedCard.gameObject);

            _selectedCard = null;
            _selectedId = -1;
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
            _hoverCardId = Mathf.RoundToInt((mousePosition.x / panelSize.x) * transform.childCount) - 1;
        }
        else
        {
            _hoverCardId = -1;
        }
    }

    private void UpdateGUI()
    {
        _rectTransform.sizeDelta = new Vector2(transform.childCount * _sizePerCard, _rectTransform.sizeDelta.y);
        float gap = (_rectTransform.sizeDelta.x - _cardWidth * transform.childCount) / transform.childCount;
        float angle = ((2 * Mathf.PI * _rectTransform.sizeDelta.x) / 360) / transform.childCount;
        int oddOrEven = 1 - transform.childCount % 2;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();
            float shiftedI = (i - transform.childCount / 2);
            float hoverI = i - _hoverCardId;

            // X position
            float gapOddOrEven = (_cardWidth / 2f + gap / 2f) * oddOrEven;
            float gapHovered = _hoverCardId != -1 ? hoverI * _cardSelectedGap : 0;
            float gapHoveredCompensation = hoverI != 0 ? Mathf.Sin(1 / Mathf.Abs(hoverI)) : 0;
            float xPos = gapOddOrEven + gapHovered * gapHoveredCompensation + gap * shiftedI + _cardWidth * shiftedI;

            // Y position
            float yOddOrEven = ((shiftedI < 0 ? 1 : 0) * _cardHeighGap) * oddOrEven;
            float yHovered = _hoverCardId == i ? _cardHeighGap * 3 : 0;
            float yPos = yOddOrEven + yHovered - Mathf.Abs(shiftedI) * _cardHeighGap;

            // Set position
            childRectTransform.anchoredPosition = new Vector2(xPos, yPos);

            // Rotation
            float angleOddOrEven = (-angle / 2f) * oddOrEven;
            childRectTransform.rotation = Quaternion.Euler(0, 0, angleOddOrEven - angle * shiftedI);
        }
    }

    private void _addHandCard(Card card)
    {
        CardDisplayer newCard = Instantiate<CardDisplayer>(_cardPrefab, transform);
        newCard.Initialize(card);
    }

    private void _clearHandCards()
    {
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
}

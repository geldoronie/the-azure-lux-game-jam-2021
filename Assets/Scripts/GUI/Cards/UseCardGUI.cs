using UnityEngine;

[RequireComponent(typeof(CardOrganizerGUI))]
public class UseCardGUI : MonoBehaviour
{
    [SerializeField] private Material _enableMaterial;
    [SerializeField] private Material _disableMaterial;

    private CardOrganizerGUI _cardOrganizerGUI;
    private GameObject _objectToCreate;

    private void Awake()
    {
        _cardOrganizerGUI = GetComponent<CardOrganizerGUI>();
    }

    private void Start()
    {
        _cardOrganizerGUI.OnSelectCard += OnSelectCard;
        _cardOrganizerGUI.OnDeselectCard += OnDeselectCard;
    }

    private void OnDestroy()
    {
        _cardOrganizerGUI.OnSelectCard -= OnSelectCard;
        _cardOrganizerGUI.OnDeselectCard -= OnDeselectCard;
    }

    private void Update()
    {
        if (_objectToCreate != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Terrain terrain = hit.collider.GetComponent<Terrain>();
                if (terrain != null)
                {
                    _objectToCreate.SetActive(true);
                    _objectToCreate.transform.position = terrain.transform.position;
                    BuildingCard card = (BuildingCard)_cardOrganizerGUI.SelectedCard.Card;
                    if (card.CanBuild(GameModeBase.Instance.Player, terrain))
                    {
                        _objectToCreate.GetComponent<MeshRenderer>().material = _enableMaterial;
                    }
                    else
                    {
                        _objectToCreate.GetComponent<MeshRenderer>().material = _disableMaterial;
                    }
                }
                else
                {
                    _objectToCreate.SetActive(false);
                }
            }
            else
            {
                _objectToCreate.SetActive(false);
            }
        }
    }

    private void OnSelectCard(Card card)
    {
        if (card is BuildingCard)
        {
            _objectToCreate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _objectToCreate.GetComponent<Collider>().enabled = false;
            _objectToCreate.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.5f);
            _objectToCreate.transform.localScale = Vector3.one * 0.65f;
            _objectToCreate.SetActive(false);
        }
    }

    private void OnDeselectCard()
    {
        if (_objectToCreate != null)
        {
            Destroy(_objectToCreate);
            _objectToCreate = null;
        }
    }
}
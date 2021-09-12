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
                    Card card = _cardOrganizerGUI.SelectedCard.Card;
                    if (card.CanUse(GameModeBase.Instance.Player, terrain))
                    {
                        SetObjectToCreateMaterial(_enableMaterial);
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (card is BuildingCard)
                            {
                                terrain.ConstructBuilding((BuildingCard)card);
                            }
                            else if (card is EffectCard)
                            {
                                ((EffectCard)card).Effect.UseEffect(GameModeBase.Instance.Player, terrain);
                            }
                            GameModeBase.Instance.Player.RemoveCard(card);
                        }
                    }
                    else
                    {
                        SetObjectToCreateMaterial(_disableMaterial);
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
        _objectToCreate = Instantiate(card.Prefab);
        _objectToCreate.SetActive(false);
        SetObjectToCreateMaterial(_disableMaterial);
    }

    private void SetObjectToCreateMaterial(Material material)
    {
        MeshRenderer[] meshRenderers = _objectToCreate.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer rend in meshRenderers)
        {
            Material[] sharedMaterials = new Material[rend.sharedMaterials.Length];
            for (int i = 0; i < rend.sharedMaterials.Length; i++)
            {
                foreach (Material mat in rend.sharedMaterials)
                {
                    sharedMaterials[i] = material;
                }
            }
            rend.sharedMaterials = sharedMaterials;
            Material[] materials = new Material[rend.materials.Length];
            for (int i = 0; i < rend.materials.Length; i++)
            {
                foreach (Material mat in rend.materials)
                {
                    materials[i] = material;
                }
            }
            rend.materials = materials;
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
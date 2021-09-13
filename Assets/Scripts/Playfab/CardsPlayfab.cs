using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class CardsPlayfab : MonoBehaviour
{
    [SerializeField] private AuthenticationPlayfab Authentication;
    [SerializeField] private GameObject primitiveBuildingCardPrefab;
    [SerializeField] private EffectBase primitiveEffect;

    public BuildingCardList BuildingCards;
    public EffectCardList EffectCards;
    public UnityAction<BuildingCard[], EffectCard[]> onGetCards;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.Authentication = GameObject.Find("AuthenticationPlayfab").GetComponent<AuthenticationPlayfab>();
        GetTitleDataRequest request = new GetTitleDataRequest
        {
            Keys = new List<string> {
                "cards_effects",
                "cards_buildings"
            },
            AuthenticationContext = this.Authentication.AuthenticationContext
        };

        PlayFabClientAPI.GetTitleData(request, this._onGetCardsSuccess, this._onGetCardsFailure);
    }

    private void _onGetCardsSuccess(GetTitleDataResult result)
    {
        if (!result.Data.ContainsKey("cards_buildings") || !result.Data.ContainsKey("cards_effects"))
        {
            Debug.Log("There's no 'cards_buildings' key at DataTitle in Playfab Configuration");
            return;
        }

        this.BuildingCards = JsonUtility.FromJson<BuildingCardList>(result.Data["cards_buildings"]);
        foreach (BuildingCard card in this.BuildingCards.Common) { LoadBuildingCard(card); }
        foreach (BuildingCard card in this.BuildingCards.Uncommon) { LoadBuildingCard(card); }
        foreach (BuildingCard card in this.BuildingCards.Rare) { LoadBuildingCard(card); }
        foreach (BuildingCard card in this.BuildingCards.Legendary) { LoadBuildingCard(card); }

        this.EffectCards = JsonUtility.FromJson<EffectCardList>(result.Data["cards_effects"]);
        foreach (EffectCard card in this.EffectCards.Common) { LoadEffectCard(card); }
        foreach (EffectCard card in this.EffectCards.Uncommon) { LoadEffectCard(card); }
        foreach (EffectCard card in this.EffectCards.Rare) { LoadEffectCard(card); }
        foreach (EffectCard card in this.EffectCards.Legendary) { LoadEffectCard(card); }
    }

    private void LoadBuildingCard(BuildingCard card)
    {
        if (!string.IsNullOrEmpty(card.PrefabId))
        {
            card.Prefab = Resources.Load("Buildings/" + card.PrefabId) as GameObject;
        }
        if (card.Prefab == null)
        {
            card.Prefab = primitiveBuildingCardPrefab;
        }
    }

    private void LoadEffectCard(EffectCard card)
    {
        card.Prefab = primitiveBuildingCardPrefab;
        if (!string.IsNullOrEmpty(card.EffectId))
        {
            card.Effect = Resources.Load("Effects/" + card.EffectId) as EffectBase;
        }
        if (card.Effect == null)
        {
            card.Effect = primitiveEffect;
        }
    }

    private void _onGetCardsFailure(PlayFabError error)
    {
        Debug.LogError(error);
    }
}

[System.Serializable]
public class BuildingCardList
{
    [SerializeField] private List<BuildingCard> common;
    [SerializeField] private List<BuildingCard> uncommon;
    [SerializeField] private List<BuildingCard> rare;
    [SerializeField] private List<BuildingCard> legendary;

    public BuildingCardList(List<BuildingCard> common, List<BuildingCard> uncommon, List<BuildingCard> rare, List<BuildingCard> legendary)
    {
        this.common = common;
        this.uncommon = uncommon;
        this.rare = rare;
        this.legendary = legendary;
    }

    public List<BuildingCard> Common { get => common; }
    public List<BuildingCard> Uncommon { get => uncommon; }
    public List<BuildingCard> Rare { get => rare; }
    public List<BuildingCard> Legendary { get => legendary; }
}

[System.Serializable]
public class EffectCardList
{
    [SerializeField] private List<EffectCard> common;
    [SerializeField] private List<EffectCard> uncommon;
    [SerializeField] private List<EffectCard> rare;
    [SerializeField] private List<EffectCard> legendary;

    public EffectCardList(List<EffectCard> common, List<EffectCard> uncommon, List<EffectCard> rare, List<EffectCard> legendary)
    {
        this.common = common;
        this.uncommon = uncommon;
        this.rare = rare;
        this.legendary = legendary;
    }

    public List<EffectCard> Common { get => common; }
    public List<EffectCard> Uncommon { get => uncommon; }
    public List<EffectCard> Rare { get => rare; }
    public List<EffectCard> Legendary { get => legendary; }
}
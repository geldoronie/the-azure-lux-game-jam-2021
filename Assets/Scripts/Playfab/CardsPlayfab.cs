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

    public List<BuildingCard> BuildingCards;
    public List<EffectCard> EffectCards;
    public UnityAction<BuildingCard[], EffectCard[]> onGetCards;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.BuildingCards = new List<BuildingCard>();
        this.EffectCards = new List<EffectCard>();
        this.Authentication = GameObject.Find("AuthenticationPlayfab").GetComponent<AuthenticationPlayfab>();
        this._getCards();
    }

    private void _getCards()
    {
        var request = new GetTitleDataRequest
        {
            Keys = new List<string> {
                "cards_effects",
                "cards_buildings"
            },
            AuthenticationContext = this.Authentication.AuthenticationContext
        };

        PlayFabClientAPI.GetTitleData(request, this._onGetCardsSuccess, this._onGetCardsFailure);
    }

    struct BuildingCardList
    {
        public List<BuildingCard> common;
        public List<BuildingCard> uncommon;
        public List<BuildingCard> rare;
        public List<BuildingCard> legendary;
    }

    struct EffectCardList
    {
        public List<EffectCard> common;
        public List<EffectCard> uncommon;
        public List<EffectCard> rare;
        public List<EffectCard> legendary;
    }

    private void _onGetCardsSuccess(GetTitleDataResult result)
    {
        if (!result.Data.ContainsKey("cards_buildings") || !result.Data.ContainsKey("cards_effects"))
        {
            Debug.Log("There's no 'cards_buildings' key at DataTitle in Playfab Configuration");
            return;
        }

        BuildingCardList buildingCardsList = JsonUtility.FromJson<BuildingCardList>(result.Data["cards_buildings"]);
        this.BuildingCards = new List<BuildingCard>();
        this.BuildingCards.AddRange(buildingCardsList.common);
        this.BuildingCards.AddRange(buildingCardsList.uncommon);
        this.BuildingCards.AddRange(buildingCardsList.rare);
        this.BuildingCards.AddRange(buildingCardsList.legendary);

        foreach (BuildingCard card in this.BuildingCards)
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

        EffectCardList effectCardsList = JsonUtility.FromJson<EffectCardList>(result.Data["cards_effects"]);
        this.EffectCards = new List<EffectCard>();
        this.EffectCards.AddRange(effectCardsList.common);
        this.EffectCards.AddRange(effectCardsList.uncommon);
        this.EffectCards.AddRange(effectCardsList.rare);
        this.EffectCards.AddRange(effectCardsList.legendary);

        foreach (EffectCard card in this.EffectCards)
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
    }

    private void _onGetCardsFailure(PlayFabError error)
    {
        Debug.LogError(error);
    }
}

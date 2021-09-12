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
        public List<BuildingCard> cards;
    }

    struct EffectCardList
    {
        public List<EffectCard> cards;
    }

    private void _onGetCardsSuccess(GetTitleDataResult result)
    {
        if (!result.Data.ContainsKey("cards_buildings") || !result.Data.ContainsKey("cards_effects"))
        {
            Debug.Log("There's no 'cards_buildings' key at DataTitle in Playfab Configuration");
            return;
        }

        this.BuildingCards = JsonUtility.FromJson<BuildingCardList>(result.Data["cards_buildings"]).cards;

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

        this.EffectCards = JsonUtility.FromJson<EffectCardList>(result.Data["cards_effects"]).cards;
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

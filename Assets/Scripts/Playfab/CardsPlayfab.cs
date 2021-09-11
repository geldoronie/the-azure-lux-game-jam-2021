using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class CardsPlayfab : MonoBehaviour
{
    [SerializeField] private AuthenticationPlayfab Authentication;
    private List<BuildingCardModelPlayfab> _buildingCards;
    private List<EffectCardModelPlayfab> _effectCards;
    public UnityAction<BuildingCardModelPlayfab[], EffectCardModelPlayfab[]> onGetCards;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this._buildingCards = new List<BuildingCardModelPlayfab>();
        this._effectCards = new List<EffectCardModelPlayfab>();
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

    private void _onGetCardsSuccess(GetTitleDataResult result)
    {
        if (!result.Data.ContainsKey("cards_buildings") || !result.Data.ContainsKey("cards_effects"))
        {
            Debug.Log("There's no 'cards_buildings' key at DataTitle in Playfab Configuration");
            return;
        }
        this._buildingCards = JsonUtility.FromJson<BuildingCardTitleDataPlayfab>(result.Data["cards_buildings"]).cards;
        this._effectCards = JsonUtility.FromJson<EffectCardTitleDataPlayfab>(result.Data["cards_effects"]).cards;

        onGetCards?.Invoke(this._buildingCards.ToArray(), this._effectCards.ToArray());
    }

    private void _onGetCardsFailure(PlayFabError error)
    {
        Debug.Log(error);
    }
}

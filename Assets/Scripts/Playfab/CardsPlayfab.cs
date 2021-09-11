using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class CardsPlayfab : MonoBehaviour
{
    [SerializeField]
    private AuthenticationPlayfab Authentication; 
    // Start is called before the first frame update
    public List<BuildingCardModelPlayfab> BuildingCards;
    public List<EffectCardModelPlayfab> EffectCards;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.BuildingCards = new List<BuildingCardModelPlayfab>();
        this.EffectCards = new List<EffectCardModelPlayfab>();
        this.Authentication = GameObject.Find("AuthenticationPlayfab").GetComponent<AuthenticationPlayfab>();
        this._getCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _getCards(){
        var request = new GetTitleDataRequest {
            Keys = new List<string> {
                "cards_effects",
                "cards_buildings"
            },
            AuthenticationContext = this.Authentication.AuthenticationContext 
        };

        PlayFabClientAPI.GetTitleData(request,this._onGetCardsSuccess,this._onGetCardsFailure);
    }

    private void _onGetCardsSuccess(GetTitleDataResult result){

        if(!result.Data.ContainsKey("cards_buildings") || !result.Data.ContainsKey("cards_effects")){
            Debug.Log("There's no 'cards_buildings' key at DataTitle in Playfab Configuration");
            return;
        }
        this.BuildingCards = JsonUtility.FromJson<BuildingCardTitleDataPlayfab>(result.Data["cards_buildings"]).cards;
        this.EffectCards = JsonUtility.FromJson<EffectCardTitleDataPlayfab>(result.Data["cards_effects"]).cards;
        Debug.Log(result);
    }

    private void _onGetCardsFailure(PlayFabError error){
        Debug.Log(error);
    }
}

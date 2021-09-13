using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

[System.Serializable]
public class GameMode {
    public string name;
    public int startingCardsCount;
    public int playTurnTime;
    public int invalidTerrainBuildingAliveToleranceTurns;
    public int mapWidth;
    public int mapHeight;
    public ResourcesAmounts passiveResourcesPerTurn; 
    public int cardPerTurn;
    public int maxCardsOnHand; 
    public int firstVickingRaidMilitary; 
    public int firstVickingRaidTurn;
    public float vickingsTurnsIncreasePerRaidRate;
    public float vickingsMilitaryIncreasePerRaidRate;
    public ResourcesAmounts playerStartResourses;
    public int buildingCardsChance;
    public int effectsCardsChance;
}

struct GameModeTitleData
{
    public List<GameMode> gameModes;
}

public class GameModePlayfab : MonoBehaviour
{
    [SerializeField] private AuthenticationPlayfab Authentication;
    public List<GameMode> GameModes;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.Authentication = GameObject.Find("AuthenticationPlayfab").GetComponent<AuthenticationPlayfab>();
        this.Authentication.OnAuthenticationSuccess += LoadGameModes;
    }

    public void LoadGameModes(){
         GetTitleDataRequest request = new GetTitleDataRequest
        {
            Keys = new List<string> {
                "game_modes"
            },
            AuthenticationContext = this.Authentication.AuthenticationContext
        };

        PlayFabClientAPI.GetTitleData(request, this._onGetGameModeSuccess, this._onGetGameModeFailure);
    }

    private void _onGetGameModeSuccess(GetTitleDataResult result)
    {
        if (!result.Data.ContainsKey("game_modes"))
        {
            Debug.Log("There's no 'game_modes' key at DataTitle in Playfab Configuration");
            return;
        }
    
        this.GameModes = JsonUtility.FromJson<GameModeTitleData>(result.Data["game_modes"]).gameModes;
        this.Authentication.OnAuthenticationSuccess -= LoadGameModes;
    }

    private void _onGetGameModeFailure(PlayFabError error)
    {
        this.Authentication.OnAuthenticationSuccess -= LoadGameModes;
        Debug.LogError(error);
    }
}

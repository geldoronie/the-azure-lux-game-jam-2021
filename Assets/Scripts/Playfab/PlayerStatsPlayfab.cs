using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public struct Matches
{
    public List<GameStats> games;
}

public class PlayerStatsPlayfab : MonoBehaviour
{
    [SerializeField] private AuthenticationPlayfab Authentication;

    [SerializeField] private List<GameStats> Matches;

    [SerializeField] private GameStats LastMatchStats;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.Authentication = GameObject.Find("AuthenticationPlayfab").GetComponent<AuthenticationPlayfab>();
        this.Authentication.OnAuthenticationSuccess += LoadPlayerStats;
    }

    public void SetLastMapStatistics(GameStats gameStats){
        this.LastMatchStats = gameStats;
    }

    public void SaveLastMapStatistics(GameStats gameStats){
        this.LastMatchStats = gameStats;
        this.Matches.Add(gameStats);
        var matches = new Matches(){
            games = this.Matches
        };
        this._setMatchesStatistics(matches);
    }

    public void LoadPlayerStats(){
        this.Authentication.OnAuthenticationSuccess -= LoadPlayerStats;
        this._getMatchesStatistics();
    }

    private void _setMatchesStatistics(Matches matches)
    {
        Dictionary<string, string> matchesData = new Dictionary<string, string>();
        matchesData.Add(
            "matches",
            JsonUtility.ToJson(matches)
        );
        var request = new UpdateUserDataRequest
        {
            Data = matchesData,
            AuthenticationContext = this.Authentication.AuthenticationContext
        };

        PlayFabClientAPI.UpdateUserData(request, this._onSetMatchesStatisticsSuccess, this._onSetMatchesStatisticsFailure);
    }

    private void _getMatchesStatistics()
    {
        var request = new GetUserDataRequest
        {
            Keys = new List<string> {
                "matches"
            },
            AuthenticationContext = this.Authentication.AuthenticationContext
        };

        PlayFabClientAPI.GetUserData(request, this._onGetMatchesStatisticsSuccess, this._onGetMatchesStatisticsFailure);
    }

    private void _onGetMatchesStatisticsSuccess(GetUserDataResult result)
    {
        if (!result.Data.ContainsKey("matches"))
        {
            Debug.Log("There's no 'matches statistics' key at PlayerDataTitle in Playfab Configuration");
            return;
        }

        this.Matches = JsonUtility.FromJson<Matches>(result.Data["matches"].Value).games;

        Debug.Log("Match Statistics Loaded!");
    }

    private void _onGetMatchesStatisticsFailure(PlayFabError error)
    {
        Debug.LogError(error);
    }
    
    private void _onSetMatchesStatisticsSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Match Statistics Saved!");
    }

    private void _onSetMatchesStatisticsFailure(PlayFabError error)
    {
        Debug.LogError(error);
    }
}

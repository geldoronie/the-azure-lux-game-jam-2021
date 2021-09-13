using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class PlayerProfilePlayfab : MonoBehaviour
{
    [SerializeField] private AuthenticationPlayfab Authentication;

    [SerializeField] public PlayerProfile Profile;

    public UnityAction OnProfileLoadedSuccess;
    public UnityAction OnProfileLoadedFailure;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.Authentication = GameObject.Find("AuthenticationPlayfab").GetComponent<AuthenticationPlayfab>();
        this.Authentication.OnAuthenticationSuccess += LoadPlayerProfile;
    }
    public void LoadPlayerProfile(){
        this.Authentication.OnAuthenticationSuccess -= LoadPlayerProfile;
        this._getPlayerProfile();
    }

    private void _getPlayerProfile()
    {
        var request = new GetPlayerProfileRequest
        {
            AuthenticationContext = this.Authentication.AuthenticationContext
        };

        PlayFabClientAPI.GetPlayerProfile(request, this._onPlayerProfileSuccess, this._onGetPlayerProfileFailure);
    }

    private void _onPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        this.Profile = new PlayerProfile();
        this.Profile.displayName = result.PlayerProfile.DisplayName;
        this.Profile.avatarUrl = result.PlayerProfile.AvatarUrl;
        this.OnProfileLoadedSuccess?.Invoke();
        Debug.Log("Player Profile Loaded!");
    }

    private void _onGetPlayerProfileFailure(PlayFabError error)
    {
        this.OnProfileLoadedFailure?.Invoke();
        Debug.LogError(error);
    }
}

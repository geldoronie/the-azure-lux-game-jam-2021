using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class AuthenticationPlayfab : MonoBehaviour
{

    [SerializeField]
    public string Username { get { return this._username; } }
    [SerializeField]
    public string Email { get { return this._email; } }
    [SerializeField]
    public string PlayFabId { get { return this._playFabId; } }
    [SerializeField]
    public PlayFabAuthenticationContext AuthenticationContext { get { return this._authenticationContext; } }

    [SerializeField]
    public bool Logged { get { return this._logged; } }
    [SerializeField]
    public bool Registered { get { return this._registered; } }
    [SerializeField]
    public string Ticket { get { return this._ticket; } }
    [SerializeField]
    public bool Loading { get { return this._loading; } }

    #region Secrets     
    const string TITLE_ID = "32052";

    const string SECRET_KEY = "1MGQR6R3A87N7K6G5WWOBG5FY1BRPXG7GWONOC7179OIC5A7N7";

    #endregion

    private string _username;
    private string _email;

    private string _playFabId;

    private string _ticket;

    private bool _logged = false;

    private bool _registered = false;

    private bool _loading = false;

    private PlayFabAuthenticationContext _authenticationContext;

    public UnityAction OnAuthenticationSuccess;
    public UnityAction<string> OnAuthenticationFailure;

    public UnityAction<string> OnRegisterAuthenticationFailure;

    // Start is called before the first frame update
    void Start()
    {
        PlayFab.PlayFabSettings.TitleId = TITLE_ID;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Login(string username, string password){
        var request = new LoginWithPlayFabRequest { 
            Username = username,
            Password = password,
            TitleId = TITLE_ID
        };

        this._username = username;
        this._loading = true;
        this._logged = false;

        PlayFabClientAPI.LoginWithPlayFab(request, this.OnLoginSuccess, this.OnLoginFailure);
    }

    public void Register(string displayName, string username, string email, string password){
        var request = new RegisterPlayFabUserRequest { 
            Username = username,
            Password = password,
            TitleId = TITLE_ID,
            Email = email,
            DisplayName = displayName
        };

        this._loading = true;
        this._registered = false;

        PlayFabClientAPI.RegisterPlayFabUser(request, this.OnRegisterSuccess, this.OnRegisterFailure);
    }

    private void OnLoginSuccess(LoginResult result){
        this._email = "";
        this._playFabId = result.PlayFabId;
        this._ticket = result.SessionTicket;
        this._authenticationContext = result.AuthenticationContext;
        this._logged = true;
        this._loading = false;
        this.OnAuthenticationSuccess?.Invoke();
        Debug.Log("Logged with successful!");
    }

    private void OnLoginFailure(PlayFabError error){
        this._logged = false;
        this._username = "";
        this._loading = false;
        // foreach (var er in error.)
        // {
        //     stringError += er.Value;
        // }
        this.OnAuthenticationFailure?.Invoke(error.ToString());
        Debug.Log("Failed to loggin!");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result){
        this._registered = true;
        this._loading = false;
        Debug.Log("Registered with successful!");
    }

    private void OnRegisterFailure(PlayFabError error){
        this._logged = false;
        this._registered = false;
        this._username = "";
        this._loading = false;
        // string stringError = "";
        // foreach (var er in error.ErrorDetails)
        // {
        //     stringError += er.Value;
        // }
        this.OnRegisterAuthenticationFailure?.Invoke(error.ToString());
        Debug.Log("Failed to register!");
        Debug.Log(error);
    }
}

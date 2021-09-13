using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthenticationGUI : MonoBehaviour
{

    [SerializeField]
    private AuthenticationPlayfab Authentication;
    // Start is called before the first frame update

    [Header("Forms")]
    [SerializeField]
    private Image LoginPanel;

    [SerializeField]
    private Image RegisterPanel;

    [SerializeField]
    private Image StartupPanel;

    [SerializeField]
    private Image LoadingPanel;

    [Header("Login")]
    [SerializeField]
    private TMP_InputField UsernameField;

    [SerializeField]
    private TMP_InputField PasswordField;

    [SerializeField]
    private Button LoginButton;


    [SerializeField]
    private TMP_Text LoginErrorText;

    [Header("Register")]
    [SerializeField]
    private TMP_InputField UsernameRegisterField;

    [SerializeField]
    private TMP_InputField PasswordRegisterField;

    [SerializeField]
    private TMP_InputField EmailRegisterField;

    [SerializeField]
    private TMP_InputField DisplayNameRegisterField;

    [SerializeField]
    private TMP_Text RegisterErrorText;

    [SerializeField]
    private Button RegisterButton;

    [Header("Startup")]
    [SerializeField]
    private Button RegisterScreenButton;

    [SerializeField]
    private Button LoginScreenButton;

    private bool _waitForAuthenticationResponse = false;

    void Awake()
    {
        this.LoginButton.onClick.AddListener(this.OnLoginButtonClick);  
        this.RegisterButton.onClick.AddListener(this.OnRegisterButtonClick);
        this.RegisterScreenButton.onClick.AddListener(this.OnRegisterScreenButtonClick);
        this.LoginScreenButton.onClick.AddListener(this.OnLoginScreenButtonClick);
        this.LoadingPanel.gameObject.SetActive(false);
        this.OnBackToStartupScreenButtonClick();
        this.Authentication.OnAuthenticationFailure += this.OnLoginError;
        this.Authentication.OnRegisterAuthenticationFailure += this.OnRegisterError;
    }

    void Update(){
        if(!this._waitForAuthenticationResponse)
            return;

        if(!Authentication.Loading){
            this.HideLoadingScreen();
            this._waitForAuthenticationResponse = false;
            if(Authentication.Registered || Authentication.Logged){
                this.ClearInputs();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
    }

    public void OnLoginButtonClick(){
        Authentication.Login(this.UsernameField.text, this.PasswordField.text);
        this.ShowLoadingScreen();
        this.LoginErrorText.text = "";
        this._waitForAuthenticationResponse = true;
    }

    public void OnRegisterButtonClick(){
        Authentication.Register(this.DisplayNameRegisterField.text, this.UsernameRegisterField.text, this.EmailRegisterField.text, this.PasswordRegisterField.text);
        this.ShowLoadingScreen();
        this.RegisterErrorText.text = "";
        this._waitForAuthenticationResponse = true;
    }

    public void OnLoginScreenButtonClick(){
        this.LoginPanel.gameObject.SetActive(true);
        this.RegisterPanel.gameObject.SetActive(false);
        this.StartupPanel.gameObject.SetActive(false);
    }

    public void OnRegisterScreenButtonClick(){
        this.LoginPanel.gameObject.SetActive(false);
        this.RegisterPanel.gameObject.SetActive(true);
        this.StartupPanel.gameObject.SetActive(false);
    }

    public void OnBackToStartupScreenButtonClick(){
        this.LoginPanel.gameObject.SetActive(false);
        this.RegisterPanel.gameObject.SetActive(false);
        this.StartupPanel.gameObject.SetActive(true);
    }

    public void ShowLoadingScreen(){
        this.LoadingPanel.gameObject.SetActive(true);
    }

    public void HideLoadingScreen(){
        this.LoadingPanel.gameObject.SetActive(false);
    }

    public void ClearInputs(){
        this.UsernameField.text = "";
        this.PasswordField.text = "";
        this.DisplayNameRegisterField.text = "";
        this.UsernameRegisterField.text = "";
        this.EmailRegisterField.text = "";
        this.PasswordRegisterField.text = "";
    }

    private void OnLoginError(string error){
        this.LoginErrorText.text = error;
    }

    private void OnRegisterError(string error){
        this.RegisterErrorText.text = error;
    }
}

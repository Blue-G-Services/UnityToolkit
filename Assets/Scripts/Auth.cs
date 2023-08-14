using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Google;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;
using models;
using models.dto;
using models.inputs;
using Newtonsoft.Json;

public class Auth : MonoBehaviour
{
    [SerializeField]
    private string googlewebapi = "1070416155038-ulgq8kfkm42f7ov9v5jqf5e5hdm4gj0a.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    private string token ;

    [SerializeField]
     TMP_InputField LoginNameText;

    [SerializeField]
     TMP_InputField LoginEmailText;

    [SerializeField]
     TMP_InputField LoginPasswordText;

    [SerializeField]
     TMP_InputField RegisterNameText;

    [SerializeField]
     TMP_InputField RegisterEmailText;

    [SerializeField]
     TMP_InputField RegisterPasswordText;

    [SerializeField]
    public TMP_Text tokentext;

    [SerializeField]
    public Button LoginBtn;

    [SerializeField]
    Button googlebtn;
    [SerializeField]
    Button GuestBtn;

    [SerializeField]
    public Button RegisterBtn;
    [SerializeField]
    public GameObject msgbox;
    // Start is called before the first frame update
    /*
    var body = await reader.ReadToEndAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<LoginResponse>(body);
    */
    private void Awake()
    {
        LoginBtn.onClick.AddListener(LoginHandle);
        RegisterBtn.onClick.AddListener(RegisterHandle);
        googlebtn.onClick.AddListener(GoogleSignInClick);
        GuestBtn.onClick.AddListener(LoginGuestHandler);
        configuration = new GoogleSignInConfiguration { WebClientId = googlewebapi, RequestEmail = true, RequestIdToken = true };

    }
    private void Start()
    {
        initFireBase();
    }
    private void initFireBase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    private void GoogleSignInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthFinished);

    }
     void OnGoogleAuthFinished(Task<GoogleSignInUser> task) {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            AddToInformation("Google ID Token = " + task.Result.IdToken);
            AddToInformation("Email = " + task.Result.Email);
            GoogleAuthHandler(task.Result.IdToken);


        }


    }

    async void LoginHandle()
    {
        try
        {
            var input = new LoginWithEmailParams
            {
                email = LoginEmailText.text,
                password = LoginPasswordText.text,


            };


            await DynamicPixels.Authentication.LoginWithEmail(input);

            SceneManager.LoadScene("MainMenu");
        }

        catch (DynamicPixelsException e)
        {
            Debug.LogException(e);
        }
    }
    async void LoginGuestHandler()
    {
        try
        {

           var login= await DynamicPixels.Authentication.LoginAsGuest(new LoginAsGuestParams { });
            SceneManager.LoadScene("MainMenu");
            
        }

        catch (DynamicPixelsException e)
        {
            Debug.LogException(e);
        }
    }

    async void RegisterHandle()
    {
        try
        {
            var input = new RegisterWithEmailParams
            {
                Name = RegisterNameText.text,
                Email = RegisterEmailText.text,
                Password = RegisterPasswordText.text
            };

            var result = await DynamicPixels.Authentication.RegisterWithEmail(input);
            Debug.Log("Register is Done");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        catch (DynamicPixelsException e)
        {
            
            Debug.LogException(e);
        }
    }
    async void GoogleAuthHandler(string TokenId)
    {
        try
        {
            var input = new LoginWithGoogleParams
            {
                AccessToken = TokenId



            };

            var result = await DynamicPixels.Authentication.LoginWithGoogle(input);
            Debug.Log("GoogleAuth");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        catch (DynamicPixelsException e)
        {

            Debug.LogException(e);
        }
    }
    private void AddToInformation(string str) { Debug.Log( "\n" + str); }

}

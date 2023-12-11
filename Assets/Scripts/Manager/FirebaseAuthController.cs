using Firebase;
using Firebase.Auth;
using Google;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
//using Facebook.MiniJSON;
using UnityEngine;

public class FirebaseAuthController : MonoBehaviour
{
    //[SerializeField]
    //private LoginReferences userInputs;
    //[SerializeField]
    //private FirestoreController firestoreController;
    //[SerializeField]
    //private List<string> facebookParams;
    //[SerializeField]
    //private PopupHelper popupPrefab;
    //[SerializeField]
    //private string googleWebApi = "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com";
    private string googleWebApi = "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com";
    //[SerializeField]
    //private LoginManager manager;


    public HomePanel HomePanel;
    //public DependencyStatus dependencyStatus;
    //public FirebaseAuth auth;
    //public FirebaseUser user;

    private string Token
    {
        get
        {
            return PlayerPrefs.GetString("Token", string.Empty);
        }
        set
        {
            PlayerPrefs.SetString("Token", value);
        }
    }


    //public  string googleAccessToken = string.Empty;
    private GoogleSignInConfiguration configuration;
    void Start()
    {

        //if (!Token.Equals(string.Empty))
        //{
        //    //OnSignInSilently();
        //}
    }

    public void GoogleSIgnIn()
    {
        OnSignIn();

    }





    //[DllImport("__Internal")]
    //private static extern void OpenOAuthInExternalTab(string url, string callbackFunctionName);

    //public void OnApplicationQuit()
    //{
    //    OnSignOut();
    //    OnDisconnect();
    //}

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            WebClientId = googleWebApi,
            RequestIdToken = true,
            UseGameSignIn = false
        };
        AddStatusText("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);
    }

    public void OnSignOut()
    {
        AddStatusText("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddStatusText("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    AddStatusText("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddStatusText("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddStatusText("Canceled");
        }
        else
        {
            AddStatusText("Welcome: " + task.Result.DisplayName + "!");
            Token = task.Result.IdToken;
            //AddStatusText("Access Token =" +  Token);
            //HomePanel.OnPlayAsGuest();

        }
    }
                                                                                                                                    
    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddStatusText("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently()
              .ContinueWith(OnAuthenticationFinished);
    }


    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddStatusText("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);
    }

    private List<string> messages = new List<string>();
    void AddStatusText(string text)
    {
        if (messages.Count == 5)
        {
            messages.RemoveAt(0);
        }
        messages.Add(text);
        string txt = "";
        foreach (string s in messages)
        {
            txt += "" + s;
        }
        Debug.Log("Google Status = " + txt);
    }
}





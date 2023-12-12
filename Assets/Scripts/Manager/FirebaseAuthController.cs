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
    private string googleWebApi = "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com";

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

    private GoogleSignInConfiguration configuration;

    public void GoogleSIgnIn()
    {
        OnSignIn();
    }
    /// <summary>  Use This client_id For Google Web Client id Form Google Services Json
    ///  "services": {
    //    "appinvite_service": {
    //      "other_platform_oauth_client": [
    //        {
    //          "client_id": "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com",
    //          "client_type": 3
    //        }
    //      ]
    //    }
    //  }
    //}
    /// </summary>
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
            AddStatusText("Enail: " + task.Result.Email + "!");
            Token = task.Result.IdToken;
            UIManager.Instance.homePanel.OnGoogleSignUp();
            UIManager.Instance.gamePlayPanel.setPlayerData(task.Result.Email,task.Result.DisplayName,task.Result.ImageUrl);
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





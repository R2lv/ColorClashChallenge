using System.Net;
using System.Threading.Tasks;
using CandyCoded.HapticFeedback;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using UnityEngine;

public class Auth : MonoBehaviour
{
    private string web_client_id = "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com";
    private FirebaseAuth _auth;
    private FirebaseUser _user;

    private GoogleSignInConfiguration configuration;
    private DependencyStatus dependencyStatus;
    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = web_client_id,
            RequestIdToken = true
        };
    }
    private void Start()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _auth = FirebaseAuth.DefaultInstance;
                //  firestoreController.InitializeFirestore();
            }
            else
            {
                Debug.Log(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void SigninWithGoogle()
    {
        HapticFeedback.MediumFeedback();
        SoundManager.Instance.ButtonClickSound();
        GoogleLogin();
    }

    private void GoogleLogin()
    {
        GoogleSignIn.Configuration = configuration;
        configuration.WebClientId = web_client_id;
        configuration.UseGameSignIn = false;
        configuration.RequestIdToken = true;
        configuration.RequestEmail = true;
        configuration.RequestProfile = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread((authTask) =>
        {
            OnGoogleAuthenticatedFinished(authTask);
        });
    }

    private void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> obj)
    {
        if (obj.IsFaulted)
        {
            Debug.LogError("Google login Fault ==== " + obj.Result.ToString());
            return;
        }
        else if (obj.IsCanceled)
        {
            Debug.Log("Google login cancelled");
            return;
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(obj.Result.IdToken, null);
            GoogleFirebaseLogin(credential);
        }
    }

    private void GoogleFirebaseLogin(Credential credential)
    {
        _auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Signin Cancelled");
                return;
            }
            else if (task.IsCanceled)
            {
                Debug.Log("Signing Cancelled");
                return;
            }
            _user = _auth.CurrentUser;

            Debug.Log("Sucsess   " + _user.UserId + _user.Email + _user.DisplayName);
            UIManager.Instance.homePanel.OnGoogleSignUp();
            UIManager.Instance.playerProfilePanel.setPlayerData(_user.Email, _user.DisplayName, _user.PhotoUrl);
        });
    }
    public void LogOut()
    {
        _auth.SignOut();
        UIManager.Instance.homePanel.gameObject.SetActive(true);
        UIManager.Instance.playerProfilePanel.setPlayerData(null, null, null);
        UIManager.Instance.playerProfilePanel.gameObject.SetActive(false);
    }
}

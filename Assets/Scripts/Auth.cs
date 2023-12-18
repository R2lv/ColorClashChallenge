using System.Net;
using System.Threading.Tasks;
using CandyCoded.HapticFeedback;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using UnityEngine;


[System.Serializable]
public class PlayerScoreData
{
    public string playerName;
    public int score;
}
public class Auth : MonoBehaviour
{
    private string web_client_id = "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com";
    private FirebaseAuth _auth;
    public FirebaseUser _user;

    private GoogleSignInConfiguration configuration;
    private DependencyStatus dependencyStatus;
    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = web_client_id,
            RequestIdToken = true
        };
        InitializeFirebase();
    }
    private void Start()
    {
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
            if (task.IsCompleted)
            {
                if (IdToken != string.Empty)
                {
                    Debug.Log("IsToken == " + IdToken);
                    //GoogleLogin();
                    Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(IdToken, null);
                    GoogleFirebaseLogin(credential);
                }

                ScoreManager.Instance.OnStart();
            }
        });
    }
    public string IdToken
    {
        set
        {
            PlayerPrefs.SetString("IdToken", value);
        }
        get
        {
            return PlayerPrefs.GetString("IdToken", null);
        }
    }
    public void SigninWithGoogle()
    {
        HapticFeedback.MediumFeedback();
        SoundManager.Instance.ButtonClickSound();
        if (IdToken != string.Empty)
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(IdToken, null);
            GoogleFirebaseLogin(credential);
        }
        else
        {
            GoogleLogin();
        }
    }

    private void GoogleLogin()
    {
        Debug.Log("Google Login BtnCLick");
        GoogleSignIn.Configuration = configuration;
        configuration.WebClientId = web_client_id;
        configuration.UseGameSignIn = false;
        configuration.RequestIdToken = true;
        configuration.RequestEmail = true;
        configuration.RequestProfile = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread((authTask) =>
        {
            Debug.Log("Auth Task" + authTask.ToString());
            OnGoogleAuthenticatedFinished(authTask);
        });
    }

    private void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> obj)
    {
        Debug.Log("Object == " + obj.Result.Email);
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
            Debug.Log("Google Login WithTOken");
            if (IdToken == string.Empty)
                IdToken = obj.Result.IdToken;
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(IdToken, null);
            GoogleFirebaseLogin(credential);
            Debug.Log("Result IdToken ======= " + IdToken);
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
        IdToken = null;
        _auth.SignOut();
        UIManager.Instance.homePanel.gameObject.SetActive(true);
        UIManager.Instance.playerProfilePanel.setPlayerData(null, null, null);
        UIManager.Instance.playerProfilePanel.gameObject.SetActive(false);
    }
    public void SetPlayerScore()
    {
        Debug.Log("USer Id = " + _user.UserId);
        Debug.Log("_count = " + GameManager.Instance._count);
        Debug.Log("_count = " + ScoreManager.Instance.gameObject.name);
        //ScoreManager.Instance.SetScoreData(_user.UserId, GameManager.Instance._count);
    }

}

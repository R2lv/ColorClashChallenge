using Firebase.Auth;
using Firebase.Extensions;
//using Facebook.MiniJSON;
using Google;
using System;
using System.Threading;
using System.Threading.Tasks;
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
    [SerializeField]
    private string googleWebApi = "1044675717648-o4hkgl70ls5psna1o13mlana8f870lji.apps.googleusercontent.com";
    //[SerializeField]
    //private LoginManager manager;
    //[SerializeField]
    //private AvatarSelector avatarSelector;

    private FirebaseUser user;
    private FirebaseAuth firebaseAuth;
    private Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    private CancellationToken CancelledMethod;
    private GoogleSignInConfiguration configuration;

    #region Init

    private void Awake()
    {
        if (dependencyStatus != Firebase.DependencyStatus.Available)
            InitializeFirebase();
        else
            firebaseAuth = FirebaseAuth.DefaultInstance;

        //InitializeFacebook();

        InitializeGoogle();

        //StartCoroutine(CheckForLoggedInUser());

    }

    private void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                firebaseAuth = FirebaseAuth.DefaultInstance;
                Debug.Log("FireBAse Autu");
                //firestoreController.InitializeFirestore();
            }
            else
            {
                Debug.Log(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }



    #endregion

    #region public methods


    public void LoginWithGoogle()
    {
        //popupPrefab.PopupMessage("Login", "Signing in Please Wait");
        GoogleLogin();
    }

    #endregion

    #region firebaseAPI

    //private Task CreateUserWithEmailAsync(string email, string password)
    //{
    //    return firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password)
    //      .ContinueWithOnMainThread((task) =>
    //      {
    //          if (LogTaskCompletion(task, "User Creation"))
    //          {
    //              SendVerificationMail();
    //          }
    //          return task;
    //      }).Unwrap();
    //}

    //private void SendVerificationMail()
    //{
    //    user = firebaseAuth.CurrentUser;

    //    user.SendEmailVerificationAsync().ContinueWithOnMainThread((authTask) =>
    //    {
    //        if (LogTaskCompletion(authTask, "Send Password Reset Email"))
    //        {

    //            Debug.Log("Account Created" + "Confirmation mail Sent. Please check spams folder too!!");
    //            //CreateFirestoreUser();
    //        }
    //    });
    //}


    //private void HandleSignInWithSignInResult(Task<SignInResult> task)
    //{
    //    if (LogTaskCompletion(task, "Sign-in"))
    //    {
    //        //if (userInputs.rememberCreds.isOn)
    //        //{
    //        //    PlayerPrefs.SetString("email", userInputs.loginEmail.text);
    //        //    PlayerPrefs.SetString("password", userInputs.loginPassword.text);
    //        //}
    //        //else
    //        //{
    //        //    PlayerPrefs.DeleteKey("email");
    //        //    PlayerPrefs.DeleteKey("password");
    //        //}

    //        user = firebaseAuth.CurrentUser;

    //        if (user.IsEmailVerified)
    //        {
    //            firestoreController.GetUser(user.UserId, null);
    //        }
    //        else
    //        {
    //            FirebaseAuth.DefaultInstance.SignOut();
    //            Debug.Log("User Not Verified" + "Please confirm your email id by clicking on the link sent to your mail");
    //        }
    //    }
    //}

    private void SendPasswordResetEmail(string email)
    {
        firebaseAuth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread((authTask) =>
        {
            if (LogTaskCompletion(authTask, "Send Password Reset Email"))
                Debug.Log("Password Reset" + "Password reset email sent to " + email + ". Please check spam folder");
        });
    }

    private bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;

        if (task.IsCanceled)
            Debug.Log("Password Reset" + operation + " canceled.");

        else if (task.IsFaulted)
        {
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string authErrorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ",
                      ((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
                }
                string[] error = exception.ToString().Split(":");
                Debug.Log("Error" + error[1]);
            }
        }
        else if (task.IsCompleted)
        {
            complete = true;
        }
        return complete;
    }

    #endregion


    #region Google API

    private void InitializeGoogle()
    {
        Debug.Log("Google Web Token  - " + googleWebApi);
        configuration = new GoogleSignInConfiguration
        {

            WebClientId = googleWebApi,
            RequestIdToken = true
        };
    }

    private void GoogleLogin()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;
        GoogleSignIn.Configuration.RequestProfile = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread((authTask) =>
        {
            OnGoogleAuthenticatedFinished(authTask);
        });
    }

    private void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> obj)
    {
        if (obj.IsFaulted)
        {
            Debug.LogError("Google login Fault === ");
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
        firebaseAuth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
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

            user = firebaseAuth.CurrentUser;

            Debug.Log("U=================  " + user.UserId + "E ================== " + user.Email + "D ====================== " + user.DisplayName);
        });
    }

    #endregion
}

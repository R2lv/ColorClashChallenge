using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Google;
using UnityEngine;

public class Auth : MonoBehaviour
{
    public string web_client_id = "785353175115-clvb09rfitgjmf8qvuft0l5r189n5oq0.apps.googleusercontent.com";
    private FirebaseAuth _auth;
    void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
        if (_auth.CurrentUser == null)
        {
            LogInAnonymously();
        }
    }

    void LogInAnonymously()
    {
        _auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled) {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            var result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    
    public void SignInWithGoogle()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration {
            RequestIdToken = true,
            WebClientId = web_client_id
        };
        
        
        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn ();

        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser> ();
        signIn.ContinueWith (task => {
            if (task.IsCanceled) {
                signInCompleted.SetCanceled ();
            } else if (task.IsFaulted) {
                signInCompleted.SetException (task.Exception);
            } else {

                Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential (((Task<GoogleSignInUser>)task).Result.IdToken, null);
                _auth.CurrentUser.LinkWithCredentialAsync(credential).ContinueWith (authTask => {
                    if (authTask.IsCanceled) {
                        signInCompleted.SetCanceled();
                    } else if (authTask.IsFaulted) {
                        signInCompleted.SetException(authTask.Exception);
                    } else {
                        Debug.Log("Logged in");
                        signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);
                    }
                });
            }
        });
    }
}

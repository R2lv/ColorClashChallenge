using Firebase.Auth;
using Google;
using UnityEngine;

public class GoogleManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    private void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignInWithGoogle()
    {
        Firebase.Auth.Credential credential =
            Firebase.Auth.GoogleAuthProvider.GetCredential("YOUR_GOOGLE_ID_TOKEN", null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.Log("User signed in successfully: " + newUser.DisplayName);
        });
    }
}

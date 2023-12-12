using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void OnPlayAsGuest()
    { 
        GameManager.Instance.gameplay.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    public void OnGoogleSignUp()
    {
        Debug.Log("GoogleSignUp");
        GameManager.Instance.gameplay.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

}

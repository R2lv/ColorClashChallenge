using CandyCoded.HapticFeedback;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void OnPlayAsGuest()
    {
        UIManager.Instance.mode = Mode.guest;
        HapticFeedback.MediumFeedback();
        SoundManager.Instance.ButtonClickSound();
        GameManager.Instance.gameplay.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    public void OnGoogleSignUp()
    {
        Debug.Log("GoogleSignUp");
        UIManager.Instance.playerProfilePanel.gameObject.SetActive(true);
        UIManager.Instance.mode = Mode.google;
        //GameManager.Instance.gameplay.gameObject.SetActive(true);
        //GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
 

    public void OnAppleSignUpBtnClick()
    {
        UIManager.Instance.mode = Mode.apple;
        HapticFeedback.HeavyFeedback();
        SoundManager.Instance.ButtonClickSound();
    }
}

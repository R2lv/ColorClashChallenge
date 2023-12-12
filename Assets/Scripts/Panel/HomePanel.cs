using CandyCoded.HapticFeedback;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void OnPlayAsGuest()
    {
        HapticFeedback.LightFeedback();
        SoundManager.Instance.ButtonClickSound();
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
    public void OnLeaderBoardBtnClick()
    {
        HapticFeedback.HeavyFeedback();
        SoundManager.Instance.ButtonClickSound();
    }

    public void OnAppleSignUpBtnClick()
    {
        HapticFeedback.HeavyFeedback();
        SoundManager.Instance.ButtonClickSound();
    }
}

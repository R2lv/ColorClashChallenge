using CandyCoded.HapticFeedback;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerProfilePanel : MonoBehaviour
{
    [Header("PlayerProfile")]
    public RawImage profileImage;
    public TextMeshProUGUI userNameText;
    public Text emailText;

    public void setPlayerData(string email, string username, Uri imageUrl)
    {
        profileImage.gameObject.SetActive(true);
        userNameText.gameObject.SetActive(true);
        //emailText.gameObject.SetActive(true);
        emailText.text = email;
        userNameText.text = username;
        //profileImage.sprite = imageUrl;
        if (imageUrl != null)
            StartCoroutine(GetImageProfilePic(imageUrl));
        else
        {
            profileImage.texture = null;
        }
    }
    public IEnumerator GetImageProfilePic(Uri imageUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D tex = new Texture2D(2, 2);
                tex = DownloadHandlerTexture.GetContent(uwr);
                profileImage.texture = tex;
                UIManager.Instance.gamePlayPanel.PlayerDataSet(userNameText.text, emailText.text, tex);
            }
        }
    }
    public void OnLeaderBoardBtnClick()
    {
        HapticFeedback.HeavyFeedback();
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.leaderBoardPanel.gameObject.SetActive(true);
    }

    public void OnPlayBtnClick()
    {
        HapticFeedback.MediumFeedback();
        SoundManager.Instance.ButtonClickSound();
        GameManager.Instance.gameplay.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    public void OnLogOutBtnClick()
    {
        HapticFeedback.MediumFeedback();
        SoundManager.Instance.ButtonClickSound();
        GameManager.Instance.googleAuth.LogOut();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [Header("PlayerProfile")]
    public RawImage profileImage;
    public Text userNameText;
    public Text emailText;


    public void setPlayerData(string email, string username, Uri imageUrl)
    {
        profileImage.gameObject.SetActive(true);
        userNameText.gameObject.SetActive(true);
        emailText.gameObject.SetActive(true);
        emailText.text = email;
        userNameText.text = username;
        //profileImage.sprite = imageUrl;
        StartCoroutine(GetImageProfilePic(imageUrl));
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
            }
        }

    }
}

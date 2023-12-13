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



    public void PlayerDataSet(string playerName,string email,Texture2D tex)
    { 
        userNameText.text = playerName;
        emailText.text = email;
        profileImage.texture = tex;
    }
}

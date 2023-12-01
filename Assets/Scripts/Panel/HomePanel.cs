using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public void OnPlayAsGuest()
    { 
        GameManager.instance.gameplay.gameObject.SetActive(true);
        GameManager.instance.StartGame();
        gameObject.SetActive(false);
    }
}

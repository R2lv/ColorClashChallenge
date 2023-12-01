using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("Text templates")]
    [TextArea]
    public string gameOverTitleText;
    [TextArea]
    public string gameOverTextTemplate;
    [TextArea]
    public string betterthanText;

    [Header("Text objects")]
    public Text gameOverTitle;
    public Text gameOverText;
    public Text betterthan;

    public void Display(int colors)
    {
        gameObject.SetActive(true);
        gameOverTitle.text = gameOverTitleText;
        gameOverText.text = string.Format(gameOverTextTemplate, colors);
        int per = colors * 10 / 7;
        betterthan.text = "You did Better than <size=18><color=yellow>" + per + "% </color></size>of the Playerbase";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("UIScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}

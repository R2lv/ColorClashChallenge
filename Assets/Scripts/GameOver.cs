using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Text templates")]
    [TextArea]
    public string gameOverTitleText;
    [TextArea]
    public string gameOverTextTemplate;

    [Header("Text objects")]
    public TextMeshProUGUI gameOverTitle;
    public TextMeshProUGUI gameOverText;

    public void Display(int colors)
    {
        gameObject.SetActive(true);
        gameOverTitle.text = gameOverTitleText;
        gameOverText.text = string.Format(gameOverTextTemplate, colors);
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

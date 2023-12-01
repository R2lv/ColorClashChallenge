using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Image mainImage;
    public Color32[] colorList;
    public int noOfColor;


    [Header("Game settings")]
    public float stageTime;
    [Range(0, 1)]
    public float timeAcceleration;
    public int misses;

    [Header("Slider options")]
    public Slider slider;

    [Header("UI")]
    public TextMeshProUGUI missesText;
    public TextMeshProUGUI correctText;

    [Header("Text templates")]
    public string missesTextTemplate;
    public string correctTextTemplate;

    [Header("Game over UI")]
    public GameOver gameOver;

    [Header("Debug")]
    public bool isPlaying = false;

    public  int _missesLeft;
    private float _time;
    private int _count;
    private float _currentStageTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }



    private void Start()
    {
        //noOfColor = RandomeNumber();
        StartGame();
        GameReset();
    }

    private void StartGame()
    {
        isPlaying = true;
        _missesLeft = misses;
        _count = 0;
        _currentStageTime = stageTime;
        gameOver.Hide();
        UpdateUI();
        NextColor();
    }

    private void NextColor()
    {
        _time = Time.time;
        GameReset();
    }

    public  void OnAnswer(bool isCorrect)
    {
        if (!isCorrect)
        {
            Fail();
        }
        else
        {
            Correct();
        }
        UpdateUI();
        _currentStageTime -= _currentStageTime * timeAcceleration;
    }
    private void Fail()
    {
        if (_missesLeft > 1)
        {
            _missesLeft--;
            NextColor();
        }
        else
        {
            _missesLeft--;
            GameOver();
        }
    }
    private void Correct()
    {
        _count++;
        NextColor();
    }

    private void UpdateUI()
    {
        Debug.Log(_count.ToString());
        missesText.text = string.Format(missesTextTemplate, misses - _missesLeft, misses);
        correctText.text = string.Format(correctTextTemplate, _count);
    }

    private void GameOver()
    {
        gameOver.Display(_count);
        isPlaying = false;
    }

    private void UpdateTimer()
    {
        slider.value = (Time.time - _time) / _currentStageTime;
        //sliderFillImage.color = Color.Lerp(sliderStartColor, sliderEndColor, slider.value);
        if (slider.value >= 1)
        {
            OnAnswer(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isPlaying) return;
        UpdateTimer();
    }
    public void GameReset()
    {
        noOfColor = RandomeNumber();
        mainImage.color = colorList[noOfColor];
    }

    public int RandomeNumber()
    {
        int no = Random.Range(0, 7);
        return no;
    }


}


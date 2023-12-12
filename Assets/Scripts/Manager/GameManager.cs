using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WheelColor
{
    public Color color;
    public bool ispickup;
}

public class GameManager : Singleton<GameManager>
{
    public GameObject gameplay;
    public KnobController knobcontroller;
    public Image mainImage;
    //public WheelColor[] ColorList;
    public WheelPart[] PartList;
    public int noOfColor;

    public Color knobSelectColor;
    public bool isselectColor;

    [Header("Game settings")]
    public float stageTime;
    [Range(0, 1)]
    public float timeAcceleration;
    public int misses;

    [Header("Slider options")]
    public Slider slider;

    [Header("UI")]
    //public TextMeshProUGUI missesText;
    public Text correctText;

    [Header("Game over UI")]
    public GameOver gameOver;

    [Header("Debug")]
    public bool isPlaying = false;

    [Header("Misses")]
    public List<Image> heartImage = new List<Image>();
    public Sprite colseSprite;

    public int _missesLeft;
    private float _time;
    private int _count;
    private float _currentStageTime;

    private void Start()
    {
        GameReset();
        RandomColorAdd();
        //PartPickUpColor();
    }
    private void Update()
    {
        if (!isPlaying) return;
        //UpdateTimer();
    }
    public void StartGame()
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

    public void OnAnswer(bool isCorrect)
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
        ColorManager.instance.GameReset();
        RandomColorAdd();
    }
    private void Correct()
    {
        _count++;
        NextColor();
        ColorManager.instance.GameReset();
        RandomColorAdd();
    }

    private void UpdateUI()
    {
        Debug.Log(_count.ToString());
        int nomisses = misses - _missesLeft;
        for (int i = 0; i < nomisses; i++)
        {
            heartImage[i].sprite = colseSprite;
        }
        correctText.text = _count + " correct";
        isselectColor = false;
    }

    private void GameOver()
    {
        gameOver.Display(_count);
        isPlaying = false;
        gameplay.SetActive(false);
    }

    private void UpdateTimer()
    {
        slider.value = (Time.time - _time) / _currentStageTime;
        if (slider.value >= 1)
        {
            OnAnswer(false);
            //ColorManager.instance.GameReset();
        }
    }
    public IEnumerator CheckColor()
    {
        yield return new WaitForSeconds(0.4f);
        if (mainImage.color == knobSelectColor)
        {
            OnAnswer(true);
        }
        else
        {
            OnAnswer(false);
        }

    }
    public void GameReset()
    {
        noOfColor = RandomeNumber();
        //mainImage.color = ColorManager.instance.color_V[noOfColor];
    }
    public int RandomeNumber()
    {
        int no = UnityEngine.Random.Range(0, 8);
        return no;
    }

    public void RandomColorAdd()
    {
        //for (int i = 0; i < 8; i++)
        //{
        //Color RandomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        //if (RandomColor == ColorList[i].color)
        //{
        //    return;
        //}
        //ColorList[i].color = RandomColor;
        //ColorList[i].color = ColorManager.instance.color_P[ColorManager.instance.colornumber[i]].colors;
        //}
        //for (int i = 0; i < PartList.Length; i++)
        //{
        //    ColorList[i].ispickup = false;
        //}
        //if()
        PartPickUpColor(ColorManager.instance.color_C);
    }

    public void PartPickUpColor(color[] colors)
    {
        for (int i = 0; i < PartList.Length; i++)
        {
            PartList[i].partImage.color = colors[randomNumberForColor(colors)].colors;
        }
        mainImage.color = colors[noOfColor].colors;
    }
    public int randomNumberForColor(color[] colors)
    {
        int no = UnityEngine.Random.Range(0, colors.Length);
        if (colors[no].isPickup == true)
        {
            return randomNumberForColor(colors);
        }
        colors[no].isPickup = true;
        return no;
    }
}


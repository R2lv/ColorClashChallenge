using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelPart : MonoBehaviour
{
    public Image partImage;
    public int colornumber;
    public GameObject border;
    private void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("knob"))
        {
            if (GameManager.instance.isselectColor == false)
            {
                GameManager.instance.isselectColor = true;
                GameManager.instance.knobSelectColor = partImage.color;
                border.SetActive(true);
                StartCoroutine(GameManager.instance.CheckColor());
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("knob"))
        { 
            border.SetActive(false);
        }
    }
}

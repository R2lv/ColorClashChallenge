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
            if (GameManager.Instance.isselectColor == false)
            {
                GameManager.Instance.isselectColor = true;
                GameManager.Instance.knobSelectColor = partImage.color;
                GameManager.Instance.knobcontroller.knob.color = partImage.color;
                border.SetActive(true);
                StartCoroutine(GameManager.Instance.CheckColor());
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

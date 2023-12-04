using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelPart : MonoBehaviour
{
    public Image partImage;
    public int colornumber;
    private void Start()
    {
        colornumber = GameManager.instance.pickupColor(colornumber);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("knob"))
        {
            Debug.Log("KonbEnter ========================================");
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class KnobController : MonoBehaviour
{
    public Image pointer;
    public Transform handle;
    public Image[] wheelButtons;
    public Image knob;
    public KnobArea area;

    private bool _isCursorInsideKnob = true;
    public bool _isMouseDown = false;

    public bool _isKnobColorPicker = false;

    public Color32[] colorList;
    // Start is called before the first frame update
    private void Start()
    {
        pointer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isMouseDown)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<CircleCollider2D>().OverlapPoint(pos))
            {
                //knob.transform.position = new Vector3(0, pos.y, 0);
                _isMouseDown = true;
                _isCursorInsideKnob = true;
                pointer.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonUp(0) && _isMouseDown)
        {
            AnglePicked(handle.rotation.eulerAngles.z);
            knob.transform.position = this.gameObject.transform.position;
            _isKnobColorPicker = false;
            _isMouseDown = false;
            pointer.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0) && _isMouseDown)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (area.isKnobArea)
            {
                Debug.Log("Pos === " + pos);
                //if (pos.x <= 0.5f)
                    knob.transform.position = new Vector2(pos.x, pos.y);
            }
            else
            {
                knob.transform.position = this.gameObject.transform.position;
                _isKnobColorPicker = false;
            }
            _isCursorInsideKnob = GetComponent<CircleCollider2D>().OverlapPoint(pos);

            if (_isCursorInsideKnob)
            {
                pointer.gameObject.SetActive(false);
            }
            else
            {
                pointer.gameObject.SetActive(true);
                RotateKnob();
            }
        }
    }

    private void AnglePicked(float angle)
    {
        var index = Math.Floor(angle / 45);
        Debug.Log("inde ==== " + index);
        //CheckColor(index);
    }

    private void RotateKnob()
    {
        var mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = AngleBetweenTwoPoints(handle.position, mouseOnScreen);
        handle.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }

    //public void CheckColor(double colornumber)
    //{
    //    //if()

    //    if (colornumber == GameManager.instance.noOfColor)
    //    {
    //        //GameManager.instance.OnAnswer(true);
    //    }
    //    else
    //    {
    //        //GameManager.instance.OnAnswer(false);
    //    }
    //}
}

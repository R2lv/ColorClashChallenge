using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobArea : MonoBehaviour
{
    public bool isKnobArea;

    public void OnMouseDown()
    {
        isKnobArea = true;
    }
    public void OnMouseUp()
    {
        isKnobArea = false;
    }
    public void OnMouseEnter()
    {
        isKnobArea= true;
    }
    public void OnMouseExit()
    {
        isKnobArea = false;
    }
}


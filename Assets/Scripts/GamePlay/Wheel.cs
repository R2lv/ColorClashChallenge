using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float rotationSpeed;
    public int number;

    public void Start()
    {
        //number = GetNumber();
        InvokeRepeating("GetNumber", 10f, 15f);
    }

    public int GetNumber()
    {
        int no = Random.Range(0, 2);
        number = no;
        return no;
    }

    public void LateUpdate()
    {
        if (number == 0)
        {
            RotateImageClockwise();
        }
        else
        {
            RotateImageAntiClockwise();
        }
    }
    void RotateImageClockwise()
    {
        //Debug.Log("ColockWIse ================== " + this.transform.rotation.z);
        // Rotate the image clockwise
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void RotateImageAntiClockwise()
    {
        //Debug.Log("AntiColockWIse");
        // Rotate the image anticlockwise
        transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
    }
}

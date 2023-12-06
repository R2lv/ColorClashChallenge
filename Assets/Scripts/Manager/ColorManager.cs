using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class color
{
    public Color colors;
    public bool isPickup;
}

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public color[] color_P;
    public color[] color_V;
    public color[] color_R;
    public color[] color_N;
    public color[] color_C;

    //public List<int> colornumber = new List<int>();
    public void Awake()
    {
        if (instance == null)
            instance = this;
        GameReset();
    }
    public void GameReset()
    {
        //colornumber.Clear();
        for (int i = 0; i < color_P.Length; i++)
        {
            color_P[i].isPickup = false;
            color_V[i].isPickup = false;
            color_R[i].isPickup = false;
            color_N[i].isPickup = false;
            color_C[i].isPickup = false;
        }
        
    }

    //public int RandomNumber()
    //{
    //    int no = UnityEngine.Random.Range(0, 8);
    //    if (no == colornumber[])
    //    {
    //        return RandomNumber();
    //    }
    //    return no;
    //}


}

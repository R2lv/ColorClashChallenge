using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public void SetColor(Color32 color)
    {
        GetComponent<Image>().color = color;
    }
}
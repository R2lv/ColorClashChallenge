using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TileManager : MonoBehaviour
{
    public Image mainImage;
    public TextMeshProUGUI mainImageText;
    public GameObject tile;
    public Color32[] colorList;
    public Vector2Int initialGrid;

    private Vector2Int _grid;
    private GridLayoutGroup _layout;
    private Rect _rect;
    private Color32 _currentColor;
    
    public delegate void OnAnswer(bool answer);

    private OnAnswer _onAnswer;
    
    void Start()
    {
        _layout = GetComponent<GridLayoutGroup>();
        _rect = GetComponent<RectTransform>().rect;
        _grid = initialGrid;
        UpdateGrid();
    }

    public void SetOnAnswer(OnAnswer onAnswer)
    {
        _onAnswer = onAnswer;
    }

    public void UpdateGrid(Vector2Int grid)
    {
        _grid = grid;
        UpdateGrid();
    }

    private void UpdateGrid()
    {
        var gridSize = CalcGridSize(_grid.x, _grid.y);
        _layout.constraintCount = _grid.x;
        _layout.cellSize = new Vector3(gridSize, gridSize);
    }
    
    private void Answer(Graphic image)
    {
        _onAnswer?.Invoke(CheckAnswer(image.color));
    }

    private bool CheckAnswer(Color32 color)
    {
        return _currentColor.Equals(color);
    }

    public void NextColor()
    { 
        mainImage.color = _currentColor = colorList[(new Random().Next(colorList.Length))];
        mainImageText.color = ContrastColor(mainImage.color);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var cList = colorList.ToList();
        for (var i = 0; i < _grid.x * _grid.y; i++)
        {
            var colorIndex = (new Random()).Next(0, cList.Count-1);
            var tileObj = Instantiate(tile, transform);
            tileObj.GetComponent<Button>().onClick.AddListener(delegate
            {
                Answer(tileObj.GetComponent<Image>());
            });
            var t = tileObj.GetComponent<Tile>();
            t.SetColor(cList[colorIndex]);
            cList.RemoveAt(colorIndex);
        }
    }

    private float CalcGridSize(int x, int y)
    {
        var spacing = _layout.spacing;
        var padding = _layout.padding;
        var spacingX = spacing.x * (x - 1);
        var spacingY = spacing.y * (y - 1);
        var width = _rect.width - spacingX - padding.left - padding.right;
        var height = _rect.height - spacingY - padding.top - padding.bottom;
        return Math.Min(width / x, height / y);
    }

    private Color32 ContrastColor(Color32 color)
    {
        var luminance = (0.299 * color.r + 0.587 * color.g + 0.114 * color.b)/255;
        var d = luminance > 0.5 ? 0 : 255;
        return  new Color(d,d,d);
    }
}

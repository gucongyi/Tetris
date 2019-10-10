using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid:MonoBehaviour
{
    //储存数组
    public static int w = 10;
    public static int h = 50;
    static float cellSize = 72;
    static float AdptercellSize = 72*Screen.width/720f;
    public static Transform[,] grid = new Transform[w, h];
    public static Camera uiCamera;

    public static Button ButtonTransLeft;
    public static Button ButtonTransRight;
    public static Button ButtonDown;
    public static Button ButtonChange;
    public static Text TextScore;
    public static int ScoreCount = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //取整数
    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round((v.x- AdptercellSize / 2) / AdptercellSize), Mathf.Round((v.y - AdptercellSize / 2)/ AdptercellSize));
    }

    public static Vector2 worldPointToScreen(Vector2 worldPoint)
    {
        var ScreenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, worldPoint);
        return ScreenPoint;
    }

    //检查边界
    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x <w && (int)pos.y >=0);
    }

    //删除第y行
    public static void DeleteRow(int y)
    {
        Grid.ScoreCount++;
        Grid.TextScore.text = $"Score:{Grid.ScoreCount}";
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //上一行往下走
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].localPosition += new Vector3(0, -1* cellSize, 0);
            }
        }
    }

    //整体往下移动
    public static void DecreaseRowAbove(int y)
    {
        for (int i = y; i < h; i++)
        {
            DecreaseRow(i);
        }
    }

    //判断某一行是否被填满
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    //删除填满的一行
    public static void DeleteFullRows()
    {
        for (int y = 0; y < h; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
                --y;
            }
        }
    }
}

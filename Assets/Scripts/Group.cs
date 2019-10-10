using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    float lastFall = 0;
    const int cellSize= 72;
    // Use this for initialization
    void Start()
    {
        Grid.ButtonTransLeft.onClick.RemoveAllListeners();
        Grid.ButtonTransLeft.onClick.AddListener(TransLeft);
        Grid.ButtonTransRight.onClick.RemoveAllListeners();
        Grid.ButtonTransRight.onClick.AddListener(TransRight);
        Grid.ButtonChange.onClick.RemoveAllListeners();
        Grid.ButtonChange.onClick.AddListener(ChangeDir);
        Grid.ButtonDown.onClick.RemoveAllListeners();
        Grid.ButtonDown.onClick.AddListener(Down);
        if (!IsValidGridPos())
        {
            Debug.Log("game over");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TransLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TransRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeDir();
        }
        else if (Time.time - lastFall >= 1)
        {
            Down();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
            Down();
            Down();
        }
    }

    private void ChangeDir()
    {
        transform.Rotate(0, 0, -90);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.Rotate(0, 0, 90);
        }
    }

    private void TransRight()
    {
        transform.localPosition += new Vector3(1 * cellSize, 0, 0);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.localPosition += new Vector3(-1 * cellSize, 0, 0);
        }
    }

    private void TransLeft()
    {
        transform.localPosition += new Vector3(-1 * cellSize, 0, 0);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.localPosition += new Vector3(1 * cellSize, 0, 0);
        }
    }

    private void Down()
    {
        transform.localPosition += new Vector3(0, -1 * cellSize, 0);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.localPosition += new Vector3(0, 1 * cellSize, 0);
            //要提出来，否则最后对每个格子处理有朝向问题
            int allChildCount = transform.childCount;
            for (int i = 0; i < allChildCount; i++)
            {
                transform.GetChild(0).parent = transform.parent;
            }
            Grid.DeleteFullRows();
            FindObjectOfType<Spawner>().spawnNext();
            enabled = false;
        }
        lastFall = Time.time;
    }

    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(Grid.worldPointToScreen(child.position));
            if (!Grid.InsideBorder(v))
            {
                return false;
            }
            if (Grid.grid[(int)v.x,(int)v.y]!=null && Grid.grid[(int)v.x,(int)v.y].parent!=transform)
            {
                return false;
            }
        }
        return true;
    }

    //更新方块的位置
    private void UpdateGrid()
    {
        //清空之前的
        for (int y = 0; y < Grid.h; y++)
        {
            for (int x = 0; x < Grid.w; x++)
            {
                if (Grid.grid[x,y] != null)
                {
                    if (Grid.grid[x,y].parent == transform)
                    {
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }
        //更新现在的
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(Grid.worldPointToScreen(child.position));
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public GameObject Cubie;

    public static Transform[,] Grid_Left = new Transform[7, 20];
    public static Transform[,] Grid_Right = new Transform[7, 20];
    public static Transform[,] Grid_Up = new Transform[7, 20];
    public static Transform[,] Grid_Down = new Transform[7, 20];

    public static int width = 7;

    enum Direction { Left, Right, Up, Down};
    Direction SpawnDirection;
    Transform parent;


    float time;

    public void Set_Direction(int direction_Num, Transform parent)
    {
        if (direction_Num == 0) SpawnDirection = Direction.Left;
        if (direction_Num == 1) SpawnDirection = Direction.Right;
        if (direction_Num == 2) SpawnDirection = Direction.Up;
        if (direction_Num == 3) SpawnDirection = Direction.Down;

        this.parent = parent;

        
        if (!ValidMove())
        {
            ValidWidth();
        }

        
    }

    void Start()
    {
        time = 0.8f;

        Cubie = GameObject.Find("Cubie");
    }

    void Update()
    {
        Block_Move();
    }

    void Block_Move()
    {
        if(time <= 0)
        {
            transform.localPosition += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.localPosition += new Vector3(0, 1, 0);
                AddToGrid();
                enabled = false;
                SpawnManager.instance.SpawnBlock();
            }

            time = 0.8f;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            Vector2 vec = parent.InverseTransformPoint(children.position);
            int x = Mathf.RoundToInt(vec.x);
            int y = Mathf.RoundToInt(vec.y);

            if (SpawnDirection == Direction.Left) Grid_Left[x, y] = children;
            if (SpawnDirection == Direction.Right) Grid_Right[x, y] = children;
            if (SpawnDirection == Direction.Up) Grid_Up[x, y] = children;
            if (SpawnDirection == Direction.Down) Grid_Down[x, y] = children;

            transform.parent = Cubie.transform;
        }
    }

    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            Vector2 vec = parent.InverseTransformPoint(children.position);
            int x = Mathf.RoundToInt(vec.x);
            int y = Mathf.RoundToInt(vec.y);

            if (x < 0 || x >= width || y < 0) return false;

            if (SpawnDirection == Direction.Left && Grid_Left[x, y] != null) return false;
            if (SpawnDirection == Direction.Right && Grid_Right[x, y] != null) return false;
            if (SpawnDirection == Direction.Up && Grid_Up[x, y] != null) return false;
            if (SpawnDirection == Direction.Down && Grid_Down[x, y] != null) return false;
        }
        return true;
    }

    void ValidWidth()
    {
        foreach (Transform children in transform)
        {
            Vector2 vec = parent.InverseTransformPoint(children.position);
            int x = Mathf.RoundToInt(vec.x);

            if (x < 0)
            {
                transform.localPosition += new Vector3(1, 0, 0);
                break;
            }
            if(x >= width)
            {
                transform.localPosition -= new Vector3(1, 0, 0);
                break;
            }
        }
    }

    public static void Grid_Rotation(string direction)
    {
        if(direction == "Left")
        {
            Transform[,] temp = Grid_Left;
            Grid_Left = Grid_Up;
            Grid_Up = Grid_Right;
            Grid_Right = Grid_Down;
            Grid_Down = temp;
            
        }
        else
        {
            Transform[,] temp = Grid_Left;
            Grid_Left = Grid_Down;
            Grid_Down = Grid_Right;
            Grid_Right = Grid_Up;
            Grid_Up = temp;
        }

    }
}

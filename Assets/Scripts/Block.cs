using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static GameObject[,] Left_Grid = new GameObject[20, 5];
    public static GameObject[,] Right_Grid = new GameObject[20, 5];
    public static GameObject[,] Up_Grid = new GameObject[5, 20];
    public static GameObject[,] Down_Grid = new GameObject[5, 20];

    enum Block_Direction{Left, Right, Up,Down};
    Block_Direction spawnDirection;
    Vector3 direction;

    float delayTime;
    public static float speed;
    bool Arrival;

    public static void BlockSpawn(GameObject[] blocks)
    {
        int n = Random.Range(0, blocks.Length);
        int random_Pos = Random.Range(0, 4);
        GameObject block = Instantiate(blocks[n]);
        Block blockScript = block.GetComponent<Block>();
        if (random_Pos == 0)
        {
            block.transform.position = new Vector2(-14, 0);
            blockScript.spawnDirection = Block_Direction.Left;
            blockScript.direction = new Vector2(1,0);
        }
        else if (random_Pos == 1)
        {
            block.transform.position = new Vector2(14, 0);
            blockScript.spawnDirection = Block_Direction.Right;
            blockScript.direction = new Vector2(-1, 0);
        }
        else if (random_Pos == 2)
        {
            block.transform.position = new Vector2(0, 14);
            blockScript.spawnDirection = Block_Direction.Up;
            blockScript.direction = new Vector2(0, -1);
        }
        else
        {
            block.transform.position = new Vector2(0, -14);
            blockScript.spawnDirection = Block_Direction.Down;
            blockScript.direction = new Vector2(0, 1);
        }

    }

    void Start()
    {
        Arrival = false;
        delayTime = 1;
        speed = 1;

        
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.RotateAround(transform.TransformPoint(Vector3.zero), new Vector3(0, 0, 1), 90);
            if(!ValidMove()) transform.RotateAround(transform.TransformPoint(Vector3.zero), new Vector3(0, 0, 1), -90);
        }
    }

    void Move()
    {
        if (ValidMove())
        {
            if (delayTime <= 0)
            {
                transform.position += direction * speed;
                delayTime = 1;


            }
            else delayTime -= Time.deltaTime;
        }
        else
        {
            AddToGrid();
        }
        
    }

    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            if (spawnDirection == Block_Direction.Left) Left_Grid[Mathf.Abs((int)children.position.x + 3),(int)children.position.y+1] = children.gameObject;
            if (spawnDirection == Block_Direction.Right) Right_Grid[(int)children.position.x - 3, (int)children.position.y+1] = children.gameObject;
            if (spawnDirection == Block_Direction.Up) Up_Grid[(int)children.position.x+1, (int)children.position.y-3] = children.gameObject;
            if (spawnDirection == Block_Direction.Down) Down_Grid[(int)children.position.x+1, Mathf.Abs((int)children.position.y+3)] = children.gameObject;
        }
    }
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int x = (int)Mathf.Abs(children.position.x);
            int y = (int)Mathf.Abs(children.position.y);

            if (spawnDirection == Block_Direction.Up || spawnDirection == Block_Direction.Down) if (y <= 5) return false;
            if (spawnDirection == Block_Direction.Left || spawnDirection == Block_Direction.Right) if (x <= 5) return false;
            print("["+(x-3));
            if (spawnDirection == Block_Direction.Left && Left_Grid[x - 3, y+1] != null) return false;
            if (spawnDirection == Block_Direction.Right && Right_Grid[x - 3, y+1] != null) return false;
            if (spawnDirection == Block_Direction.Up && Up_Grid[x+1, y - 3] != null) return false;
            if (spawnDirection == Block_Direction.Down && Down_Grid[x+1, y - 3] != null) return false;
        }
        return true;
    }
}

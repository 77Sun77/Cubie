using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static int[,] Blocks = new int[24, 5]; // 1이면 블록 있는거 / 0이면 블록 없는거

    GameObject go;
    Transform blocks_Parent;
    public void Block_Setting(Vector3 spawnPoint, int point)
    {
        transform.localPosition = spawnPoint;
        RotationManager.instance.Set_Block(transform.GetChild(0));
        blocks_Parent = transform.GetChild(0);
        this.point = point;
    }

    int point;


    void Start()
    {
        
    }

    void Update()
    {
        Block_Move();
    }

    void Block_Move()
    {
        if(ValidMove()) transform.Translate(Vector3.down * 1 * Time.deltaTime);
        else
        {
            Vector3 vec = transform.localPosition;
            transform.localPosition = new Vector3(vec.x, Mathf.RoundToInt(vec.y));
            AddToGrid();
            SpawnManager.instance.SpawnBlock();
            enabled = false;
        }
    }
    /*
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Cubie") && Block_Kind == Kinds.Block_Parent && isActive)
        {
            Grid_Block();
            
        }
        if(Block_Kind == Kinds.Piece && !col.CompareTag("Cubie"))
        {
            go = col.gameObject;
            if (parentBlock.isActive) enabled = false;
        }
    }
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Cubie") && Block_Kind == Kinds.Block_Parent)
        {
            transform.position += transform.up;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Cubie") && Block_Kind == Kinds.Block_Parent && isActive)
        {
            SpawnManager.instance.SpawnBlock();
            enabled = false;
            isActive = false;
            Destroy(gameObject);
        }
    }*/

    void Grid_Block()
    {
        foreach(Transform block in blocks_Parent)
        {
            GameObject go = block.GetComponent<Block>().go;
            Color color = go.GetComponent<SpriteRenderer>().color;
            color.a = 1;
            go.GetComponent<SpriteRenderer>().color = color;
            go.tag = "Cubie";
            block.GetComponent<Block>().enabled = false;
        }
        
    }
    void AddToGrid()
    {
        foreach (Transform block in blocks_Parent)
        {
            Vector3 vec = transform.parent.InverseTransformPoint(block.transform.position);
            int x = Mathf.CeilToInt(vec.x);
            int y = Mathf.CeilToInt(vec.y);

            Blocks[X_Calculation(x), y] = 1;
            print(X_Calculation(x) + "," + y);
        }
    }
    bool ValidMove()
    {
        foreach(Transform block in blocks_Parent)
        {
            Vector3 vec = transform.parent.InverseTransformPoint(block.transform.position);
            int x = Mathf.CeilToInt(vec.x);
            int y = Mathf.CeilToInt(vec.y);

            if (y <= 0) return false;

            if (!(y >= 5))
            {
                if (Blocks[X_Calculation(x), y] != 0) return false;
            }
            
        }
        return true;
    }

    int X_Calculation(int x)
    {
        if(point == 0)
        {
            if (x == -1) return 23;
        }
        if(point == 23)
        {
            if (x == 1) return 1;
        }
        return point + x;
    }
}

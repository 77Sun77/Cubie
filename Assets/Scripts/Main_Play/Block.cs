using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static int[,] Blocks = new int[24, 5]; // 1이면 블록 있는거 / 0이면 블록 없는거

    GameObject go;
    Transform blocks_Parent;
    public void Block_Setting(Vector3 spawnPoint, int point, BlockState state)
    {
        transform.localPosition = spawnPoint;
        RotationManager.instance.Set_Block(transform.GetChild(0));
        blocks_Parent = transform.GetChild(0);
        this.point = point;
        block = state;
    }

    int point;
    public enum BlockState { Original, Clone, Piece };
    public BlockState block;

    void Start()
    {
        
    }

    void Update()
    {
        Block_Move();

        if(block == BlockState.Clone)
        {
            if(blocks.Count == blocks_Parent.childCount)
            {
                foreach (GameObject block in blocks)
                {
                    Color color = block.GetComponent<SpriteRenderer>().color;
                    color.a = 1;
                    block.GetComponent<SpriteRenderer>().color = color;
                    
                }
                blocks.RemoveRange(0, blocks.Count);
                Destroy(gameObject);
            }
            
        }
    }

    void Block_Move()
    {
        if(block == BlockState.Original)
        {
            if (ValidMove())
            {
                transform.Translate(Vector3.down * 1 * Time.deltaTime);
            }
            else
            {
                Vector3 vec = transform.localPosition;
                int y = 0;
                if (Mathf.RoundToInt(vec.y) != 0) y = Mathf.CeilToInt(vec.y);
                transform.localPosition = new Vector3(vec.x, y);
                AddToGrid();
                SpawnManager.instance.SpawnBlock();
                Clone_Block();
                Destroy(gameObject);
            }
        }
        
    }
    void Clone_Block()
    {
        Block go = Instantiate(gameObject,transform.parent).GetComponent<Block>();
        go.Block_Setting(transform.localPosition, point, BlockState.Clone);
        foreach(Transform children in go.blocks_Parent)
        {
            children.GetComponent<Block>().enabled = true;
            children.GetComponent<Block>().block = BlockState.Piece;
        }
    }

    public static List<GameObject> blocks = new List<GameObject>();
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (block == BlockState.Piece)
        {
            blocks.Add(col.gameObject);
        }
    }
    /*
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
    }
    */
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
            int x = Mathf.RoundToInt(vec.x);
            int y = Mathf.RoundToInt(vec.y);

            Blocks[X_Calculation(x), y] = 1;
        }
    }
    bool ValidMove()
    {
        foreach(Transform block in blocks_Parent)
        {
            Vector3 vec = transform.parent.InverseTransformPoint(block.transform.position);
            int x = Mathf.FloorToInt(block.localPosition.x);
            int y = Mathf.FloorToInt(vec.y);

            if (transform.localPosition.y <= 0) return false;

            if (y < 5)
            {
                print(X_Calculation(x));
                if (Blocks[X_Calculation(x), y] == 1)
                {
                    return false;
                }
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

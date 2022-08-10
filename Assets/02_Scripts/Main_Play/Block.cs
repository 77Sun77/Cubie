using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockSpeed;

    public enum Kinds { Block_Parent, Piece };
    public Kinds Block_Kind;
    GameObject go;
    Transform blocks_Parent;
    public void Block_Setting(Vector3 rotation, Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        transform.RotateAround(Vector3.zero, new Vector3(0, 0, -1), rotation.z);
        RotationManager.instance.Set_Block(transform.GetChild(0));
        blocks_Parent = transform.GetChild(0);
        isActive = true;
    }

    public bool isActive;

    Block parentBlock;

    void Start()
    {
        if (Block_Kind == Kinds.Piece) parentBlock = transform.parent.parent.GetComponent<Block>();
    }

    void Update()
    {
        Block_Move();
    }

    void Block_Move()
    {
        if (isActive) transform.Translate(Vector3.down * 1 * blockSpeed * Time.deltaTime);
    }
}

   /* public void OnTriggerEnter2D(Collider2D col)
    {

        if (Block_Kind == Kinds.Piece && !col.CompareTag("Cubie"))
        {
            //go = col.gameObject;
            //if (parentBlock.isActive) enabled = false;
           // Debug.Log("몰루");
        }
        if (col.CompareTag("Cubie") && Block_Kind == Kinds.Block_Parent && isActive)
        {
           
            GridManager.instance.FillGrid(col);
            Grid_Block();
           //Debug.Log("아루");

        }

    }




    //최상위 실행
    void Grid_Block()
    {
        isActive = false;
        foreach (Transform block in blocks_Parent)
        {
            //GameObject go = block.GetComponent<Block>().go;
            //Color color = go.GetComponent<SpriteRenderer>().color;
            //color.a = 1;
            //go.GetComponent<SpriteRenderer>().color = color;
            //go.tag = "Cubie";
            block.GetComponent<Block>().enabled = false;
        }
        //SpawnManager.instance.SpawnBlock();
        this.enabled = false;
        Destroy(gameObject);
    }


}
   */
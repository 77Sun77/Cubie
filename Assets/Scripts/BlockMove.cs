using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public GameObject Cubie;
    Transform parent;
    int col_Count;
    Vector3 rotation;
    CubieRotation cubieRotation;

    public static Transform[,] Blocks = new Transform[40, 40];
    Vector3 pos, pos_Temp;

    public void Set_Direction(Transform parent)
    {
        this.parent = parent;
    }

    bool isMove, isArrival;
    void Start()
    {
        Cubie = GameObject.Find("Cubie");
        cubieRotation = GameObject.Find("RotationController").GetComponent<CubieRotation>();

        isMove = true;
        isArrival = false;

        rotation = transform.eulerAngles;
    }

    void Update()
    {
        Block_Move();

        if(isArrival && transform.localPosition != pos_Temp)
        {
            transform.localPosition = pos_Temp;
            print(pos_Temp);
        }
       
    }

    void Block_Move()
    {
        if(isMove) transform.Translate(new Vector3(0, -1, 0) * 2 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isMove = false;
        col_Count++;
        if (col_Count == 1)
        {
            First:
            if (!Valid())
            {
                transform.localPosition += new Vector3(0, 1, 0);
                goto First;
            }
            else AddToGrid();
            isArrival = true;
            Transform clone = BlockClone();
            transform.parent = Cubie.transform;
            transform.localPosition = Cubie.transform.InverseTransformPoint(pos);
            transform.localRotation = clone.localRotation;
            pos_Temp = Cubie.transform.InverseTransformPoint(clone.position);
            print(Cubie.transform.InverseTransformPoint(pos));
            foreach (Transform children in transform) children.GetComponent<BoxCollider2D>().enabled = false;
            SpawnManager.instance.SpawnBlock();
            //enabled = false;
        }
    }

    bool Valid()
    {
        Vector3 vec = transform.position;
        int x = Mathf.RoundToInt(vec.x);
        int y = Mathf.RoundToInt(vec.y);
        transform.position = new Vector3(x, y, 0);
        foreach (Transform children in transform)
        {
            vec = GameObject.Find("Collider").transform.InverseTransformPoint(children.position);
            x = Mathf.RoundToInt(vec.x);
            y = Mathf.RoundToInt(vec.y);
            if (Blocks[x, y] != null) return false;
        }
        pos = transform.position;
        pos = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        return true;
    }
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            Vector3 vec = GameObject.Find("Collider").transform.InverseTransformPoint(children.position);
            int x = Mathf.RoundToInt(vec.x);
            int y = Mathf.RoundToInt(vec.y);
            Blocks[x, y] = children;
        }
    }

    Transform BlockClone()
    {
        GameObject go = Instantiate(gameObject);
        go.transform.position = pos;
        print(Camera.main.transform.InverseTransformPoint(go.transform.position));
        go.transform.rotation = transform.rotation;
        go.transform.parent = GameObject.Find("Collider").transform;
        go.GetComponent<BlockMove>().Clone_Setting(transform);
        return go.transform;
    }

    public void Clone_Setting(Transform originar)
    {
        Color color = new Color(0, 0, 0, 0);
        foreach (Transform children in transform) children.GetComponent<SpriteRenderer>().color = color;
        enabled = false;
        col_Count = 1;
    }
}

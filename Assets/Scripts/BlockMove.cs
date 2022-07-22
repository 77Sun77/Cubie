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
        col_Count = 0;

        rotation = transform.eulerAngles;

    }

    void Update()
    {
        Block_Move();
        if (!cubieRotation.isRotation) isArrival = false;
        else if(isArrival && cubieRotation.isRotation) transform.rotation = Quaternion.Euler(rotation);
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
            Vector3 vec = transform.position;
            int x = Mathf.RoundToInt(vec.x);
            int y = Mathf.RoundToInt(vec.y);
            transform.position = new Vector3(x, y, 0);
            transform.parent = Cubie.transform;
            isArrival = true;
            SpawnManager.instance.SpawnBlock();
            //enabled = false;
        }
        
        
    }
}

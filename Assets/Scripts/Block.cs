using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void Block_Setting(Vector3 rotation, Vector3 spawnPoint)
    {
        transform.rotation = Quaternion.Euler(rotation);
        transform.position = spawnPoint;
        RotationManager.instance.Set_Block(transform);
    }
    void Start()
    {
        
    }

    void Update()
    {
        Block_Move();
    }

    void Block_Move()
    {
        transform.Translate(Vector3.down * 3 * Time.deltaTime);
    }
}

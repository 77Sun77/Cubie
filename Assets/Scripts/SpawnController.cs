using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;
    public List<GameObject> Blocks = new List<GameObject>();
    void Start()
    {
        instance = this;

        BlockSpawn();
    }

    public void BlockSpawn()
    {
        Block.BlockSpawn(Blocks.ToArray());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) BlockSpawn();
    }
}

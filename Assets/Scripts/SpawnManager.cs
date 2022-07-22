using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("Block")]
    public List<GameObject> Blocks = new List<GameObject>();
    public Transform[] SpawnDirection = new Transform[4];
    void Start()
    {
        instance = this;
        SpawnBlock();
    }

    void Update()
    {
        
    }

    public void SpawnBlock()
    {/*
        int direction = Random.Range(0, 4);
        GameObject go = Instantiate(Blocks[Random.Range(0, Blocks.Count)], SpawnDirection[direction]);
        go.transform.localPosition = new Vector2(Random.Range(0, 9), 15);
        go.GetComponent<TetrisBlock>().Set_Direction(direction, SpawnDirection[direction]);*/

        int direction = Random.Range(0, 4);
        GameObject go = Instantiate(Blocks[Random.Range(0, Blocks.Count)], SpawnDirection[direction]);
        go.transform.localPosition = new Vector2(Random.Range(0, 9), 15);
        go.GetComponent<BlockMove>().Set_Direction(SpawnDirection[direction]);

    }
}

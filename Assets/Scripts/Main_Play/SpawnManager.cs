using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("Block")]
    public List<GameObject> Blocks = new List<GameObject>();
    
    void Start()
    {
        instance = this;
        SpawnBlock();
    }

    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        int point = Random.Range(0, 24);
        GameObject block = Instantiate(Blocks[Random.Range(0, Blocks.Count)], GameObject.Find(point.ToString()).transform);
        block.GetComponent<Block>().Block_Setting(new Vector3(0,7,0), point, Block.BlockState.Original);
        RotationManager.instance.Reset_Object();
    }
}

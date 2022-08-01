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
        GameObject block = Instantiate(Blocks[Random.Range(0, Blocks.Count)]);
        Vector3 rotation = new Vector3(0, 0, 15) * Random.Range(0, 24);
        block.GetComponent<Block>().Block_Setting(rotation, new Vector3(0,15,0));
        RotationManager.instance.Reset_Object();
    }
}

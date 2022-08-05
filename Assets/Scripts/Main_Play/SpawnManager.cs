using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("Block")]
    public List<GameObject> Blocks = new List<GameObject>();

    GameObject Block_Past;

    void Start()
    {
        instance = this;
        SpawnBlock();
    }

   

    public void SpawnBlock()
    {
        if(Block_Past)
            Destroy(Block_Past);

        int ranrot = Random.Range(0, 24);
        Block_Past = Instantiate(Blocks[Random.Range(0, Blocks.Count)]);
        Vector3 rotation = new Vector3(0, 0, 15) * ranrot;
        Block_Past.GetComponent<Block>().Block_Setting(rotation, new Vector3(0,15,0));
        RotationManager.instance.Reset_Object();

        NewWorldManager.instance.SpawnBlock(ranrot+RotationManager.instance.offset);
        Debug.Log(ranrot + RotationManager.instance.offset);

        Debug.Log("스폰 매니저 스폰");
    }
    
    
}

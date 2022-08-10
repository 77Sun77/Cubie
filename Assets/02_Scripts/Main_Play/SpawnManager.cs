using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("Block")]
    public List<GameObject> Blocks = new List<GameObject>();

    GameObject Block_Past;

    public int devindex;

    void Start()
    {
        instance = this;
        SpawnBlock();
    }

   

    public void SpawnBlock()
    {
        if(Block_Past)
            Destroy(Block_Past);

        Debug.Log("½ºÆù");

        int ranrot = Random.Range(0, 24);
        // int ranIndex = Random.Range(0, Blocks.Count);
       
        Block_Past = Instantiate(Blocks[devindex]);
        Vector3 rotation = new Vector3(0, 0, 15) * ranrot;
        Block_Past.GetComponent<Block>().Block_Setting(rotation, new Vector3(0,14.65f,0));
        RotationManager.instance.Reset_Object();

        int actualIndex = ranrot + RotationManager.instance.offset;

        if (actualIndex > 24)
        {
            actualIndex = actualIndex % 24;
        }
        
        


        NewWorldManager.instance.SpawnBlock(actualIndex, devindex);
        Debug.Log(actualIndex);

      
    }
    
    
}

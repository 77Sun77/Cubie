using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorldManager : MonoBehaviour
{
    public static NewWorldManager instance;

    public static Transform[,] gridedSlot;

    public static int height = 50;
    public static int width = 26;

    public GameObject[] Tetrinos;

    public int SpawnHeight;
     void Awake()
     {
        instance=this;

        gridedSlot = new Transform[width,height];

     }

   public void SpawnBlock(int spawnPoint)
   {
        GameObject go=Instantiate(Tetrinos[0], new Vector3(spawnPoint+1, -50+ SpawnHeight, 0),Quaternion.identity);
        go.transform.SetParent(GameObject.Find("New World").transform);
        RotationManager.instance.Set_NewBlock(go.GetComponent<NewBlock>());
   }



}
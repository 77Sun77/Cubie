using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorldManager : MonoBehaviour
{
    public static NewWorldManager instance;

    public  GridInfo[,] gridedSlots_Main;
    public  GridInfo[,] gridedSlots_Sub; 
    public  GridInfo[,] gridedSlots_Over;

    public static int height_Main = 50;
    public static int width_Main = 24;

    public static int width_Sub = 5;
    
    public static int width_Over = 5;
 





    public GameObject[] Tetrinos;

    public int SpawnHeight;
     void Awake()
     {
        Debug.Log("»ý¼º");
        gridedSlots_Main = new GridInfo[width_Main, height_Main];
        gridedSlots_Sub = new GridInfo[width_Sub, height_Main];
        gridedSlots_Over = new GridInfo[width_Over, height_Main];

        instance =this;
     }


    public void SpawnBlock(int spawnPoint,int tetrinoIndex)
    {
        GameObject go=Instantiate(Tetrinos[tetrinoIndex], new Vector3(spawnPoint, -50+ SpawnHeight, 0),Quaternion.identity);
        go.transform.SetParent(GameObject.Find("New World").transform);
        RotationManager.instance.Set_NewBlock(go.GetComponent<NewBlock>());
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoIndexer : MonoBehaviour
{
    public GridBlock[] GB;
    int index;

    public GridBlock[,] GB_2;

    private void Awake()
    {
        GB_2 = new GridBlock[NewWorldManager.width_Main, NewWorldManager.height_Main];
    }

    private void Start()
    {
        for (int i = GB.Length; index < 120; i--)
        {
           
            GB[index].v = i/ 23;
            GB[index].h = i % 23;

         

            GB_2[GB[index].h, GB[index].v] = GB[index];
            
              index++;
        }

    }

    void Update()
    {
        //if (NewWorldManager.instance.gridedSlots_Main)
        //{
        //    Debug.Log(0);
        //}
        //else
        //{
        //    Debug.Log(1);
        //}


        for (int x = 0; x < NewWorldManager.width_Main; x++)
        {
            for (int y = 0; y < NewWorldManager.height_Main; y++)
            {
                if (NewWorldManager.instance.gridedSlots_Main[x, y].transform)
                {
                    if (!GB_2[x, y])
                    {
                        //Debug.Log("NullRef");
                    }
                    else
                    {
                        GB_2[x, y].TurnOn();
                    }

                }

            }
        }
    }




}

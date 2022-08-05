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
        GB_2 = new GridBlock[NewWorldManager.width, NewWorldManager.height];
    }

    private void Start()
    {
        for (int i = GB.Length; index < 120; i--)
        {
           
            GB[index].v = i/ 24+1;
            GB[index].h = i % 24;

            if (i % 24 == 0)
            {
                GB[index].v -= 1;
                GB[index].h = 24;
            }

            GB_2[GB[index].h, GB[index].v] = GB[index];
            
              index++;
        }

    }

    void Update()
    {
        for (int x = 0; x < NewWorldManager.width; x++)
        {
            for (int y = 0; y < NewWorldManager.height; y++)
            {
                if (NewWorldManager.gridedSlot[x, y])
                {
                    if (!GB_2[x, y])
                    {
                        Debug.Log("NullRef");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public static RotationManager instance;

    enum Object_Kinds { Cubie, Block };
    Object_Kinds selected_Object;
    Transform Cubie, block;
    void Awake()
    {
        instance = this;
        Cubie = GameObject.Find("Cubie").transform.GetChild(0);
        selected_Object = Object_Kinds.Cubie;
    }

    
    void Update()
    {
        
    }

    public void Set_Block(Transform block)
    {
        this.block = block;
    }

    void CubieRotation(Vector3 direction)
    {
        print("-----------------------");
        printInfo();
        Cubie.RotateAround(Vector3.zero, direction, 15);
        if (direction == new Vector3(0, 0, 1)) // Left (0 -> 23 / 23 -> 22)
        {
            int[] temp = new int[5];
            for (int i = 0; i < 5; i++) temp[i] = Block.Blocks[0, i];
            for(int i=22; i>=0;i--)
            {
                for (int j = 0; j < 5; j++) Block.Blocks[i, j] = Block.Blocks[i + 1, j];
            }
            for (int i = 0; i < 5; i++) Block.Blocks[23,i] = temp[i];
        }
        else // Right (23 -> 0 / 0 -> 1)
        {
            int[] temp = new int[5];
            for (int i = 0; i < 5; i++) temp[i] = Block.Blocks[23, i];
            for (int i = 1; i < 24; i++)
            {
                for (int j = 0; j < 5; j++) Block.Blocks[i, j] = Block.Blocks[i - 1, j];
            }
            for (int i = 0; i < 5; i++) Block.Blocks[0, i] = temp[i];
        }
        printInfo();
    }
    void printInfo()
    {
        string test = "";
        for(int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 5; j++) test += Block.Blocks[i, j].ToString()+", ";
            test+="\n";
        }
        print(test);
    }
    void BlockRotation(Vector3 direction)
    {
        block.RotateAround(block.parent.TransformPoint(Vector3.zero), direction, 90);
    }

    public void OnClick_Left()
    {
        if (selected_Object == Object_Kinds.Cubie) CubieRotation(new Vector3(0, 0, 1));
        else BlockRotation(new Vector3(0, 0, 1));
    }

    public void OnClick_Right()
    {
        if (selected_Object == Object_Kinds.Cubie) CubieRotation(new Vector3(0, 0, -1));
        else BlockRotation(new Vector3(0, 0, -1));
    }

    public void Changed_Object()
    {
        if (selected_Object == Object_Kinds.Cubie) selected_Object = Object_Kinds.Block;
        else selected_Object = Object_Kinds.Cubie;
    }

    public void Reset_Object()
    {
        selected_Object = Object_Kinds.Cubie;
    }
}

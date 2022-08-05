using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public static RotationManager instance;

    enum Object_Kinds { Cubie, Block };
    Object_Kinds selected_Object;
    Transform Cubie, block;

    NewBlock newblock;

    public int offset;

    void Awake()
    {
        instance = this;
        Cubie = GameObject.Find("Cubie").transform;
        selected_Object = Object_Kinds.Cubie;
    }

    public void Set_NewBlock(NewBlock _newBlock)
    {
        newblock= _newBlock;
    }


    public void Set_Block(Transform block)
    {
        this.block = block;
    }

    void CubieRotation(Vector3 direction)
    {
        Cubie.RotateAround(Vector3.zero, direction, 15);
    }
    void BlockRotation(Vector3 direction)
    {
        block.RotateAround(block.parent.TransformPoint(Vector3.zero), direction, 90);
    }

    public void OnClick_Left()
    {
        if (selected_Object == Object_Kinds.Cubie)
        {
            if (!newblock.MoveR())
                return;
            CubieRotation(new Vector3(0, 0, 1));
            offset++;
        }
        else
        {
            if (!newblock.RotR())
                return;
            BlockRotation(new Vector3(0, 0, 1));
            
        }

    }

    public void OnClick_Right()
    {
        if (selected_Object == Object_Kinds.Cubie)
        {
            if (!newblock.MoveL())
                return;

         
            CubieRotation(new Vector3(0, 0, -1));
            
            offset--;

        }
        else
        {
            if (!newblock.RotL())
                return;
            BlockRotation(new Vector3(0, 0, -1));
          
        }

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

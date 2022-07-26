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
        Cubie = GameObject.Find("Cubie").transform;
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
        Cubie.RotateAround(Vector3.zero, direction, 30);
    }
    void BlockRotation(Vector3 direction)
    {
        
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
}

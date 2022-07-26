using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public static RotationManager instance;

    Transform Cubie, block;
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        
    }

    public void Set_Block(Transform block)
    {
        this.block = block;
    }
    public void CubieRotation()
    {

    }
}

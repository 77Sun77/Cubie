using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    public int h;
    public int v;
    public bool isfilled;


   

    [ContextMenu("����")]
    public void TurnOn()
    {
        //isfilled = true;
        Color colur = gameObject.GetComponent<SpriteRenderer>().color;
        colur.a=1;
        gameObject.GetComponent<SpriteRenderer>().color = colur;
       // Debug.Log("����");
    }



}

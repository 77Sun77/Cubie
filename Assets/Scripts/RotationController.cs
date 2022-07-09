using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    Transform target;
    Vector3 mousePos;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!OnMouseClick())
            {
                if(target != null)
                {
                    if(mousePos.x >= 0)
                    {
                        target.RotateAround(transform.TransformPoint(Vector3.zero), new Vector3(0, 0, 1), -90);
                        if (target.tag == "Cubie") TetrisBlock.Grid_Rotation("Right");
                    }
                    else
                    {
                        target.RotateAround(transform.TransformPoint(Vector3.zero), new Vector3(0, 0, 1), 90);
                        if (target.tag == "Cubie") TetrisBlock.Grid_Rotation("Right");
                        
                    }
                }
            }
        }

    }

    bool OnMouseClick()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit)
        {
            target = hit.transform;
            print(hit.transform.gameObject.name);
            return true;
        }
        return false;
    }
}

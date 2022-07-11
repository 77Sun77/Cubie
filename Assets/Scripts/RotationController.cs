using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Transform target;
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
                        
                        if (target.tag == "Cubie")
                        {
                            TetrisBlock.Grid_Rotation("Right");
                            target.RotateAround(transform.TransformPoint(Vector3.zero), new Vector3(0, 0, 1), -90);
                        }                      
                        else
                        {
                            TetrisBlock block = target.GetComponent<TetrisBlock>();
                            target.RotateAround(target.TransformPoint(block.anchorPoint), new Vector3(0, 0, 1), -90);

                        }
                    }
                    else
                    {
                        
                        if (target.tag == "Cubie")
                        {
                            TetrisBlock.Grid_Rotation("Left");
                            target.RotateAround(transform.TransformPoint(Vector3.zero), new Vector3(0, 0, 1), 90);
                        }
                        else
                        {
                            TetrisBlock block = target.GetComponent<TetrisBlock>();
                            target.RotateAround(target.TransformPoint(block.anchorPoint), new Vector3(0, 0, 1), 90);
                        }
                        
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
            
            if (hit.transform.name != "Cubie")
            {
                TetrisBlock block = hit.transform.parent.GetComponent<TetrisBlock>();
                if (!block.enabled) return false;
                else target = hit.transform.parent;
            }
            else target = hit.transform;
            return true;
        }
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlock : MonoBehaviour
{
   
    public bool check;
    public Vector3 anchorPoint;

    float curdelay;
    float falltime = 1f;

   public Vector3 offset = new Vector3(0, -50, 0);

    public Transform[] blockPair;
    public Transform[] GhostPair;


    void OnEnable()
    {
        CheckSideToSide();


    }

    void Start()
    {
        curdelay = Time.time;
    }

    void Update()
    {
        //check = ValidMove();

        //미리 이동시켜놓고 검증결과가 false일시 다시 원상태로 복귀시키는 코드
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveR();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveL();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            RotR();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            RotL();
        }


        
        if (Time.time - curdelay > falltime)
        {
            transform.position += new Vector3(0, -1, 0);
            curdelay = Time.time;
            if (!ValidMove(1))
            {
                //transform.position -= new Vector3(0, -1, 0);
                this.enabled = false;
                AddToGrid();

                //Debug.Log("스폰");
                SpawnManager.instance.SpawnBlock();
            }
        }
        
    }

    public bool MoveR()
   {
        transform.position += new Vector3(1, 0, 0);

        LineUp();
        CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.position -= new Vector3(1, 0, 0);
            LineUp();
            CheckSideToSide();
            return false;
        }
        return true;
   }

   public bool MoveL()
   {
        transform.position += new Vector3(-1, 0, 0);

        LineUp();
        CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.position -= new Vector3(-1, 0, 0);
            LineUp();
            CheckSideToSide();
            return false;


        }
        return true;
   }

    public bool RotR()
   {
        transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), 90);

        LineUp();
        CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), -90);
            LineUp();
            CheckSideToSide();
            return false;

        }
        return true;
    }

    public bool RotL()
   {
        transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), -90);

        LineUp();
        CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), 90);
            LineUp();
            CheckSideToSide();
            return false;

        }
        return true;
    }

    void CheckForLines()
    {
        for (int i = NewWorldManager.height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                //DeleteLine(i);

                //RowDown(i);
            }
            
        }
    }

    //줄 검증
    bool HasLine(int i)
    {
        for (int j = 0; j < NewWorldManager.width; j++)
        {
            if (NewWorldManager.gridedSlot[j, i] == null)
            {
                return false;
            }

        }

        return true;

    }

   

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            if (!children.CompareTag("Tetrino"))
                return;

            Vector3 localLoc = children.transform.position-offset;

            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y);

            //Debug.Log(roundedX + "," + roundedY);


            NewWorldManager.gridedSlot[roundedX, roundedY] = children;
           // Debug.Log("추가");

        }
    }


   public bool ValidMove(int index)
    {
        foreach (Transform children in transform)
        {
            Vector3 localLoc = children.transform.position-offset;


            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y) - index;

            //Debug.Log(roundedX + "," + roundedY);


            //아래로 갔을때 이동제한
            if (localLoc.y-index < 1)
            {
                return false;
            }



            //이미깔려 있는 블럭과 충돌시 움직임을 제한하는 코드
            if (NewWorldManager.gridedSlot[roundedX, roundedY] != null)
                return false;

        }
        return true;

    }
    void CheckSideToSide()
    {
        foreach (Transform children in transform)
        { 
            int roundedX = Mathf.RoundToInt(children.transform.position.x);

            if (roundedX < 1 )
            {
                if (children.CompareTag("Tetrino"))
                {
                    children.transform.position = new Vector3(NewWorldManager.width - 2, children.transform.position.y, 0);

                }


                if (children.CompareTag("Anchor"))
                {
                    transform.position = transform.position = new Vector3(NewWorldManager.width - 2, transform.position.y, 0);
                    LineUp();
                    CheckSideToSide();


                    if (ValidMove(0) == false)
                    {
                        transform.position = new Vector3(1, transform.position.y, 0);
                        LineUp();
                        CheckSideToSide();
                    }

                }


            }
            else if (roundedX > NewWorldManager.width-2)          
            {
                if (children.CompareTag("Tetrino"))
                {
                    children.transform.position = new Vector3(1, children.transform.position.y, 0);
                }


                if (children.CompareTag("Anchor"))
                {
                    transform.position = transform.position = new Vector3(1, transform.position.y, 0);
                    LineUp();
                    CheckSideToSide();

                    if (ValidMove(0) == false)
                    {
                        transform.position = transform.position = new Vector3(NewWorldManager.width - 2, transform.position.y, 0);
                        LineUp();
                        CheckSideToSide();
                    }
                }

            }
        }
    }

    void LineUp()
    {
        for (int i = 0; i < blockPair.Length; i++)
        {
            blockPair[i].transform.position = GhostPair[i].transform.position;
        }
    }


}

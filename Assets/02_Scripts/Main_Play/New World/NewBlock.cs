using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlock : MonoBehaviour
{
    public bool multiAnchor;

    public bool stopMoving;

    public bool check;
     Vector3 anchorPoint;

    float curdelay;
    float falltime = 1f;

   public Vector3 offset = new Vector3(0, -50, 0);

    public Transform[] blockPair;
    public Transform[] GhostPair;

    public Vector3 SubTransformOffset;

    public Transform tranform_Sub;
    void OnEnable()
    {
        CheckSideToSide();


    }

    void Start()
    {

        curdelay = Time.time;
        foreach (Transform children in transform)
        {
            if (children.tag == "Anchor_Spin")
            {
                anchorPoint = children.transform.localPosition;

            }
            
            if (children.CompareTag("Transform_Sub"))
            {
                tranform_Sub = children;
                SubTransformOffset = children.transform.localPosition;

            }

        }


    }

    void CheckMultiAnchor()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Transform_Sub"))
            {
                SubTransformOffset = child.transform.localPosition- Vector3.zero;

            }

        }

     
    }


    void Update()
    {
        CheckSideToSide();
        AddToGrid_Movable();
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

        if (stopMoving)
            return;
        
        if (Time.time - curdelay > falltime)
        {
            transform.position += new Vector3(0, -1, 0);
            curdelay = Time.time;
            if (!ValidMove(1))
            {
                //transform.position -= new Vector3(0, -1, 0);
                this.enabled = false;
                AddToGrid_Static();

                //Debug.Log("스폰");
                SpawnManager.instance.SpawnBlock();
            }
        }
        
    }

    public bool MoveR()
   {
        transform.position += new Vector3(1, 0, 0);

        //LineUp();
        CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.position -= new Vector3(1, 0, 0);
            //LineUp();
           // CheckSideToSide();
            return false;
        }
        return true;
   }

   public bool MoveL()
   {
        transform.position += new Vector3(-1, 0, 0);

        //LineUp();
        CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.position -= new Vector3(-1, 0, 0);
           // LineUp();
            //CheckSideToSide();
            return false;


        }
        return true;
   }

    public bool RotR()
   {
        transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), 90);

        LineUp();
       // CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), -90);
            //LineUp();
           // CheckSideToSide();
            return false;

        }
        return true;
    }

    public bool RotL()
   {
        transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), -90);

       LineUp();
        //CheckSideToSide();

        if (ValidMove(0) == false)
        {
            transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), 90);
            //LineUp();
            //CheckSideToSide();
            return false;

        }
        return true;
    }

    //void CheckForLines()
    //{
    //    for (int i = NewWorldManager.height - 1; i >= 0; i--)
    //    {
    //        if (HasLine(i))
    //        {
    //            //DeleteLine(i);

    //            //RowDown(i);
    //        }
            
    //    }
    //}

    //줄 검증
    //bool HasLine(int i)
    //{
    //    for (int j = 0; j < NewWorldManager.width; j++)
    //    {
    //        if (NewWorldManager.gridedSlots[j, i] == null)
    //        {
    //            return false;
    //        }

    //    }

    //    return true;

    //}

    void AddToGrid_Movable()
    {
        foreach (Transform children in transform)
        {
            if (!children.CompareTag("Tetrino"))
                return;

            Vector3 localLoc = children.transform.position - offset;

            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y);

            //Debug.Log(roundedX + "," + roundedY);


            NewWorldManager.gridedSlots[roundedX, roundedY] = NewWorldManager.GridState.Movable;
            // Debug.Log("추가");

        }
    }

    void AddToGrid_Static()
    {
        foreach (Transform children in transform)
        {
            if (!children.CompareTag("Tetrino"))
                return;

            Vector3 localLoc = children.transform.position-offset;

            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y);

            //Debug.Log(roundedX + "," + roundedY);


            NewWorldManager.gridedSlots[roundedX, roundedY] = NewWorldManager.GridState.Static;
           // Debug.Log("추가");

        }
    }


   public bool ValidMove(int index)
    {
        foreach (Transform children in blockPair)
        {
          
                Vector3 localLoc = children.transform.position - offset;

                int roundedX = Mathf.RoundToInt(localLoc.x);
                int roundedY = Mathf.RoundToInt(localLoc.y) - index;

                //Debug.Log(roundedX + "," + roundedY
                //아래로 갔을때 이동제한
                if (localLoc.y - index < 1)
                {
                    return false;
                }
                //이미깔려 있는 블럭과 충돌시 움직임을 제한하는 코드
                if (NewWorldManager.gridedSlots[roundedX, roundedY] == NewWorldManager.GridState.Static)
                    return false;
            
        }
        return true;

    }
    void CheckSideToSide()
    {
        foreach (Transform children in transform)
        { 
            int roundedX = Mathf.RoundToInt(children.transform.position.x);

            if (roundedX <1 )
            {
                if (children.CompareTag("Tetrino"))
                {
                    children.transform.position = new Vector3(NewWorldManager.width - 2, children.transform.position.y, 0);

                }

                if (children.CompareTag("Anchor_Spin"))
                {
                    if (multiAnchor)
                    {
                        MoveDoubleTF(new Vector3(NewWorldManager.width - 2, transform.position.y, 0));
                    }
                    else
                    {
                        transform.position = new Vector3(NewWorldManager.width - 2, transform.position.y, 0);
                    }

                   
                    if (ValidMove(0) == false)
                    {
                        transform.position = new Vector3(1, transform.position.y, 0);
                    }
                }
            }
            else if (roundedX > NewWorldManager.width-2)          
            {
                if (children.CompareTag("Tetrino"))
                {
                    children.transform.position = new Vector3(1, children.transform.position.y, 0);
                }


                if (children.CompareTag("Anchor_Spin"))
                {
                    transform.position  = new Vector3(1, transform.position.y, 0);
                    
                    LineUp();

                    if (ValidMove(0) == false)
                    {
                        transform.position  = new Vector3(NewWorldManager.width - 2, transform.position.y, 0);
                       
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

    void MoveDoubleTF(Vector3 movePoint)
    {
        List<Transform> tfs = new();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Anchor"))
            {
                tfs.Add(child);
            }

        }

        bool IsAnchorOut(Transform _tf)
        {
            if (_tf.position.x < 1)
            {
                return true;
            }
            return false;
        }


        if (tfs.TrueForAll(IsAnchorOut))
        {
            if ((transform.position - movePoint).sqrMagnitude > (tranform_Sub.position - movePoint).sqrMagnitude)
            {
                transform.position = movePoint - SubTransformOffset;
            }
            else
            {
                transform.position = movePoint;
            }
            LineUp();
        }
        else
        {
            Debug.Log("부족");
        }
    }

   




}

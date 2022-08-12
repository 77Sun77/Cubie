using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        CheckSideState();
        ClearGrid();
        AddToGrid_Movable();
        

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
                this.enabled = false;
                AddToGrid_Static();

               
                SpawnManager.instance.SpawnBlock();
            }
        }
        
    }

    public bool MoveR()
   {
        transform.position += new Vector3(1, 0, 0);

        

        if (ValidMove(0) == false)
        {
            transform.position -= new Vector3(1, 0, 0);
            Debug.Log("빠꾸");
            return false;
        }
        return true;
   }

   public bool MoveL()
   {
        transform.position += new Vector3(-1, 0, 0);

        ClearGrid();
        AddToGrid_Movable();

        if (ValidMove(0) == false)
        {
            transform.position -= new Vector3(-1, 0, 0);
            Debug.Log("빠꾸");

            return false;


        }
        return true;
   }

    public bool RotR()
   {
        transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), 90);

        

        if (ValidMove(0) == false)
        {
            transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), -90);
            LineUp();
           // CheckSideToSide();
            return false;

        }
        return true;
    }

    public bool RotL()
   {
        transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), -90);

       

        if (ValidMove(0) == false)
        {
            transform.RotateAround(transform.TransformPoint(anchorPoint), new Vector3(0, 0, 1), 90);
            LineUp();
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
        foreach (Transform children in blockPair)
        {
            Vector3 localLoc = children.transform.position - offset;

            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y);

            if (roundedX < 0)
            {
                NewWorldManager.instance.gridedSlots_Sub[-roundedX, roundedY].transform = children;
                NewWorldManager.instance.gridedSlots_Sub[-roundedX, roundedY].gridState = GridInfo.GridState.Movable;
            }
           else if (roundedX > NewWorldManager.width_Main-1)
            {
                Debug.Log("오버호출");
                NewWorldManager.instance.gridedSlots_Over[roundedX - 24, roundedY].transform = children;
                NewWorldManager.instance.gridedSlots_Over[roundedX - 24, roundedY].gridState = GridInfo.GridState.Movable;
            }
            else if (0<=roundedX&& roundedX<=NewWorldManager.width_Main-1)
            {
                NewWorldManager.instance.gridedSlots_Main[roundedX , roundedY].transform = children;
                NewWorldManager.instance.gridedSlots_Main[roundedX, roundedY].gridState = GridInfo.GridState.Movable;

            }



        }
    }

    void AddToGrid_Static()
    {
        foreach (Transform children in blockPair)
        {
            Vector3 localLoc = children.transform.position - offset;

            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y);

            //if (roundedX < 0)
            //{
            //    NewWorldManager.instance.gridedSlots_Sub[-roundedX, roundedY] = NewWorldManager.GridState.Static;
            //}
            //else if (roundedX > NewWorldManager.width_Main)
            //{
            //    NewWorldManager.instance.gridedSlots_Over[-roundedX, roundedY] = NewWorldManager.GridState.Static;
            //}
            //else
            {
                
                NewWorldManager.instance.gridedSlots_Main[roundedX , roundedY].gridState = GridInfo.GridState.Static;
            }

        }
    }

    //public  async Task MT()
    //{
    //    ClearGrid();
    //    AddToGrid_Movable();
    //    CheckSideState();
    //    ClearGrid();
    //    AddToGrid_Movable();

    //}



   public  bool ValidMove(int index)
    {
       
        ClearGrid();
        AddToGrid_Movable();
        CheckSideState();
        ClearGrid();
        AddToGrid_Movable();

        foreach (Transform children in blockPair)
        {
                Vector3 localLoc = children.transform.position - offset;

                int roundedX = Mathf.RoundToInt(localLoc.x);
                int roundedY = Mathf.RoundToInt(localLoc.y) - index;

          //  Debug.Log(roundedX+","+ roundedY);

            if (localLoc.y - index < 1)
            {
                return false;
            }

            if (localLoc.y<0)
                return false;

            if (roundedX >= 0 && roundedX <= 23)
            {

                if (NewWorldManager.instance.gridedSlots_Main[roundedX, roundedY].gridState == GridInfo.GridState.Static)
                    return false;
            }
        }
        return true;
    }
    void CheckSideToSide()
    {
        return;
        foreach (Transform children in transform)
        { 
            int roundedX = Mathf.RoundToInt(children.transform.position.x);

            if (roundedX <1 )
            {
                if (children.CompareTag("Tetrino"))
                {
                    children.transform.position = new Vector3(NewWorldManager.width_Main - 2, children.transform.position.y, 0);

                }

                if (children.CompareTag("Anchor_Spin"))
                {
                    if (multiAnchor)
                    {
                        MoveDoubleTF(new Vector3(NewWorldManager.width_Main - 2, transform.position.y, 0));
                    }
                    else
                    {
                        transform.position = new Vector3(NewWorldManager.width_Main - 2, transform.position.y, 0);
                    }

                   
                    if (ValidMove(0) == false)
                    {
                        transform.position = new Vector3(1, transform.position.y, 0);
                    }
                }
            }
            else if (roundedX > NewWorldManager.width_Main-2)          
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
                        transform.position  = new Vector3(NewWorldManager.width_Main - 2, transform.position.y, 0);
                       
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

   
    void ClearGrid()
    {
        for (int y = 0; y < NewWorldManager.height_Main; y++)
        {
            for (int x = 0; x < NewWorldManager.width_Main; x++)
            {
                if (NewWorldManager.instance.gridedSlots_Main[x, y].gridState==GridInfo.GridState.Movable)
                {
                    NewWorldManager.instance.gridedSlots_Main[x, y].transform = null;
                    NewWorldManager.instance.gridedSlots_Main[x, y].gridState = GridInfo.GridState.None;

                }
            }

            for (int x = 0; x < NewWorldManager.width_Sub; x++)
            {
                if (NewWorldManager.instance.gridedSlots_Sub[x, y].gridState == GridInfo.GridState.Movable)
                {
                    NewWorldManager.instance.gridedSlots_Sub[x, y].transform = null;
                    NewWorldManager.instance.gridedSlots_Sub[x, y].gridState = GridInfo.GridState.None;

                }
            }

            for (int x = 0; x < NewWorldManager.width_Over; x++)
            {
                if (NewWorldManager.instance.gridedSlots_Over[x, y].gridState == GridInfo.GridState.Movable)
                {
                    NewWorldManager.instance.gridedSlots_Over[x, y].transform = null;
                    NewWorldManager.instance.gridedSlots_Over[x, y].gridState = GridInfo.GridState.None;

                }
            }

        }       
    }

    

    void CheckSideState()
    {
        for (int y = 0; y < NewWorldManager.height_Main; y++)
        {
            for (int x = 0; x < NewWorldManager.width_Sub; x++)
            {
                if (NewWorldManager.instance.gridedSlots_Sub[x, y].transform!=null)
                {
                    Debug.Log("Sub:" + x +","+y);
                    NewWorldManager.instance.gridedSlots_Sub[x, y].transform.position= new Vector3(24-x, NewWorldManager.instance.gridedSlots_Sub[x, y].transform.position.y, 0);
                }
            }

            for (int x = 0; x < NewWorldManager.width_Over; x++)
            {
                if (NewWorldManager.instance.gridedSlots_Over[x, y].transform)
                {
                    Debug.Log("Over:"+x + "," + y);
                    NewWorldManager.instance.gridedSlots_Over[x, y].transform.position = new Vector3( x, NewWorldManager.instance.gridedSlots_Over[x, y].transform.position.y, 0);
                }
            }


        }
    }


    void CheckAnchor()
    {




    }


}

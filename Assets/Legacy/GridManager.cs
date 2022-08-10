using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//레이캐스트를 딕셔너리로 쓰기위함
//public class Rayhit_couple
//{
//   public Rayhit_couple(RaycastHit2D[] grids, RaycastHit2D[] blocks)
//   {
//        rayhit_grid= grids;
//        rayhit_block= blocks;
//   }
   
//   public RaycastHit2D[] rayhit_grid;
//   public RaycastHit2D[] rayhit_block;


//}



public class GridManager : MonoBehaviour
{
    public static GridManager instance;


    int h=24;
    int v=4;

    public GameObject GO;

    GameObject RotHelper_Parent;

    GameObject[] RotHelpers;

    GridBlock[,] GridBlocks;


    Dictionary<int, GameObject> gridDic = new ();
    Dictionary<int, GameObject> blockDic = new();


    private void Awake()
    {
        instance = this;

        GridBlocks = new GridBlock[h,v];

       
        //RotHelper_Parent = Instantiate(GO, transform);
        //RotHelper_Parent.transform.name = "RotHelper_Parent";

        //RotHelpers= new GameObject[h];
    }

    private void Start()
    {
        //for (int i = 0; i < h; i++)
        //{

        //    RotHelpers[i] = Instantiate(GO, RotHelper_Parent.transform);
        //    RotHelpers[i].transform.Rotate(Vector3.forward * 15 * (i + 1));

        //}
    }
    private void Update()
    {
        //for (int i = 0; i < h; i++)
        //{
        //    Vector3 curdir = RotHelpers[i].transform.up;
        //    if(i%2==0)
        //    ShotRay(transform.position, curdir, 100, Color.red,i);
        //    else
        //        ShotRay(transform.position, curdir, 100, Color.blue, i);
        //}


    }

    void ShotRay(Vector3 _pos,Vector3 _dir,float _dis,Color _col,int arrayNum)
    {
        Debug.DrawRay(_pos, _dir * _dis, _col);

        RaycastHit2D[] rayhits_grid = Physics2D.RaycastAll(_pos, _dir, _dis, LayerMask.NameToLayer("GridBlock"));
        RaycastHit2D[] rayhits_block=Physics2D.RaycastAll(_pos, _dir, _dis, LayerMask.NameToLayer("FallingBlock"));

       // Rayhit_couple rayhit_Couple = new Rayhit_couple(rayhits_grid, rayhits);  
        
       
            gridDic.Clear();
        for (int i = 0; i < rayhits_grid.Length; i++)
        {
            gridDic.Add(arrayNum, rayhits_grid[i].collider.gameObject);

        }
        for (int i = 0; i < rayhits_block.Length; i++)
        {
            blockDic.Add(arrayNum, rayhits_block[i].collider.gameObject);

        }




    }

    public void FillGrid(Collider2D col)
    {

        //GameObject value = col.gameObject;
        ////값으로 키찾는식
        //int key = gridDic.FirstOrDefault((x) => x.Value == value).Key;






        
    }




}

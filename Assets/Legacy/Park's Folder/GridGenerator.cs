using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{


    public GameObject Circle_prefab;
    public GameObject Line_prefab;
    public GameObject Grid_prefab;

    public int circle_num;
    public float circleDistance;
    public float originCircle;

    public float lineLength;

    public int count;

    void Start()
    {
        var Grid_obj=Instantiate(Grid_prefab,Vector3.zero,Quaternion.identity);
        for (int i = 0; i < circle_num; i++)
        {
            var circle_W = Instantiate(Circle_prefab, Vector3.zero, Quaternion.identity);
            circle_W.GetComponent<SpriteRenderer>().sortingOrder = circle_num - i*2;
            circle_W.GetComponent<SpriteRenderer>().color =  new Color(1, 1, 1, 0.5f);
            circle_W.transform.localScale = Vector3.one* originCircle + Vector3.one* i * circleDistance;
            circle_W.transform.SetParent(Grid_obj.transform);

            var circle_B = Instantiate(Circle_prefab, Vector3.zero, Quaternion.identity);
            circle_B.transform.localScale = circle_W.transform.localScale + Vector3.one * lineLength;
            circle_B.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
            circle_B.GetComponent<SpriteRenderer>().sortingOrder = circle_W.GetComponent<SpriteRenderer>().sortingOrder-1;
            circle_B.transform.SetParent(Grid_obj.transform);


        }
        for (int i = 0; i < count; i++)
        {
            var Line_obj = Instantiate(Line_prefab, Vector3.zero, Quaternion.identity);
            Line_obj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
            Line_obj.GetComponent<SpriteRenderer>().sortingOrder = circle_num +1;
            Line_obj.transform.localScale = new Vector3(lineLength / 2, originCircle + circleDistance * circle_num, 0);
            Line_obj.transform.Rotate( new Vector3(0,0,360/count*i));
            Line_obj.transform.SetParent(Grid_obj.transform);
        }
      
      
    }
}

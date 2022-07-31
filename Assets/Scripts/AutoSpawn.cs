using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawn : MonoBehaviour
{
    float angle;
    void Start()
    {
        angle = 15;
        for (int i=0; i<23; i++)
        {
            GameObject go = Instantiate(gameObject, transform.parent);
            go.transform.RotateAround(transform.parent.position, new Vector3(0,0,1), angle);
            angle += 15;
            go.GetComponent<AutoSpawn>().enabled = false;
            go.transform.GetChild(0).parent = GameObject.Find("Lines").transform;
        }    
        transform.parent = GameObject.Find("Lines").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

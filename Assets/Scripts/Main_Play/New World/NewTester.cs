using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTester : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, -50, 0);


    private void Update()
    {
        AddToGrid();
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            if (!children.CompareTag("Tetrino"))
                return;

            Vector3 localLoc = children.transform.position - offset;

            int roundedX = Mathf.RoundToInt(localLoc.x);
            int roundedY = Mathf.RoundToInt(localLoc.y);

            Debug.Log(roundedX + "," + roundedY);


            NewWorldManager.gridedSlot[roundedX, roundedY] = children;
            Debug.Log("Ãß°¡");

        }
    }
}

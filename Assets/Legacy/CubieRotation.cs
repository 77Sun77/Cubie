using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieRotation : MonoBehaviour
{
    public Transform Cubie, Collider;
    Vector3 destination, direction;

    public bool isRotation, colliderRotation;
    void Start()
    {
        Cubie = GameObject.Find("Cubie").transform;
        Collider = GameObject.Find("Collider").transform;

        isRotation = false;
        colliderRotation = false;
    }


    void Update()
    {
        if (isRotation)
        {
            Quaternion rotate = Quaternion.Euler(destination);
            Cubie.rotation = Quaternion.Slerp(Cubie.rotation, rotate, 30*Time.deltaTime);
            if(Vector3.Distance(rotate.eulerAngles, Cubie.eulerAngles) <= 45f && colliderRotation)
            {
                Collider.RotateAround(Vector3.zero, new Vector3(0, 0, 1), direction.z * 90);
                colliderRotation = false;
            }
            if (Vector3.Distance(rotate.eulerAngles, Cubie.eulerAngles)<=0.1f)
            {
                Cubie.rotation = Quaternion.Euler(destination);
                isRotation = false;
            }
        }

    }


    void OnClickBtn(string name)
    {
        if (isRotation) return;
        if (name == "Left") direction = new Vector3(0, 0, 1);
        if (name == "Right") direction = new Vector3(0, 0, -1);
        destination = Cubie.eulerAngles + (direction * 90);
        isRotation = true;
        colliderRotation = true;

    }

    public void Left()
    {
        OnClickBtn("Left");
    }

    public void Right()
    {
        OnClickBtn("Right");
    }
}

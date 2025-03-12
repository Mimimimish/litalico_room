using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        Vector3 pos;

        pos.x = target.transform.position.x;
        pos.y = target.transform.position.y;
        pos.z = target.transform.position.z;
        transform.position = new Vector3(pos.x, pos.y + 3, pos.z + 2);
    }
}

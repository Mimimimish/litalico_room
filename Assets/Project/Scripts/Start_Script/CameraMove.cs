using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        Vector3 pos = target.transform.position;
        
        transform.position = new Vector3(pos.x, pos.y + 3, pos.z + 2);
    }
}

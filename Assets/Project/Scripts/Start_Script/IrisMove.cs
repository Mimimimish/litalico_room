using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisMove : MonoBehaviour
{
    public float test = 0.2f;
    public float test2 = 1.0f;

    public GameObject target;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = target.transform.position;

        transform.position = new Vector3(pos.x + 4.5f, pos.y - test, pos.z + 16.5f -test);

        
        transform.localScale = new Vector3(test2 , test2, 0.00001f);
    }
}

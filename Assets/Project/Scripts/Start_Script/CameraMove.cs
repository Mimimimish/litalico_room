using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject target;
    private int rnd_num;

    void Start()
    {
        rnd_num = Random.Range(0, 5);

        if (rnd_num == 0) {
            target = GameObject.Find("dao");
        }
        else if (rnd_num == 1) {
            target = GameObject.Find("ebi");
        }
        if (rnd_num == 2) {
            target = GameObject.Find("matsu");
        }
        if (rnd_num == 3) {
            target = GameObject.Find("tomo");
        }
        if (rnd_num == 4) {
            target = GameObject.Find("lily");
        }
    }

    void Update()
    {
        Vector3 pos = target.transform.position;
        
        transform.position = new Vector3(pos.x, pos.y + 10, pos.z + 10);
    }
}

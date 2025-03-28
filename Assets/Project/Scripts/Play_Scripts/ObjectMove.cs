using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public Player player;
    public int count = 0;
    public List<GameObject> Objects = new List<GameObject>();

    void Start()
    {
        count = 0;
    }

    void Update()
    {

    }

    public void CountUp()
    {
        count++;

        // countが1になったとき、最初のオブジェクトを非表示にする
        if (count == 1 && Objects.Count > 0)
        {
            Objects[0].SetActive(false);
        }
        if (count == 2 && Objects.Count > 1)
        {
            Objects[1].SetActive(false);
        }
        if (count == 3 && Objects.Count > 2)
        {
            Objects[2].SetActive(false);
        }
    }
}

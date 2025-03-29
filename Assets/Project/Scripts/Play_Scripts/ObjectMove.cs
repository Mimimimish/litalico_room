using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public Player player;
    public int count = 0;
    public List<GameObject> Objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CountUp()
    {
        count++;
    }
}

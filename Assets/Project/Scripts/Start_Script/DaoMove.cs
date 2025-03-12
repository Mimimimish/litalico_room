using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaoMove : MonoBehaviour
{
    const int ACTION_TH = 4; //歩行の閾値
    const float SPEED = 0.5f;

    Animator anim;
    public int actionValue;
    private bool isRotating = false;
    private int rotateDir;

    void Start()
    {
        actionValue = 10;
        anim = GetComponent<Animator>();

        StartCoroutine("DecideAction");
    }

    void Update()
    {
        if (ACTION_TH < actionValue)
        {
            anim.SetBool("isWalking", true);

            Vector3 velocity = transform.rotation * new Vector3(0, 0, SPEED);
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            if (isRotating)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
    }

    IEnumerator DecideAction()
    {
        while(true)
        {
            actionValue = Random.Range(0, 10);
            rotateDir = Random.Range(-1, 1);
            if (actionValue < ACTION_TH && rotateDir != 0)
            {
                isRotating = true;
                for (int i = 0; i < actionValue * 10; i++)
                {
                    transform.Rotate(0, rotateDir, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                isRotating = false;
            }
            yield return new WaitForSeconds(1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMove : MonoBehaviour
{
    const float SPEED = 0.5f;

    Animator anim;
    private bool isWalking = false;
    private int rotateTheta;
    private int rotateDir;

    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine("DecideAction");
    }

    void Update()
    {
        if (isWalking) {
            anim.SetBool("isWalking", true);

            Vector3 velocity = transform.rotation * new Vector3(0, 0, SPEED);
            transform.position += velocity * Time.deltaTime;
        }
        else {
            anim.SetBool("isWalking", false);
        }
    }

    IEnumerator DecideAction()
    {
        while(true) {
            for (int i = 0; i < 3; i++) {
                isWalking = true;
                yield return new WaitForSeconds(1.7f);
                isWalking = false;
                yield return new WaitForSeconds(0.5f);
                isWalking = true;
                yield return new WaitForSeconds(1.3f);

                rotateTheta = 120;
                rotateDir = -1;
                StartCoroutine("RotateModel");
                yield return new WaitForSeconds(3);
            }

            for (int i = 0; i < 4; i++) {
                isWalking = true;
                yield return new WaitForSeconds(1.2f);
                isWalking = false;
                yield return new WaitForSeconds(1.2f);
                isWalking = true;
                yield return new WaitForSeconds(1.8f);

                rotateTheta = 90;
                rotateDir = 1;
                StartCoroutine("RotateModel");
                yield return new WaitForSeconds(2);
            }
        }
    }

    IEnumerator RotateModel()
    {
        isWalking = true;

        for (int i = 0; i < rotateTheta; i++) {
            transform.Rotate(0, rotateDir, 0);
            yield return new WaitForSeconds(0.01f);
        }
        isWalking = false;
    }
}

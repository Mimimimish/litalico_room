using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public TalkScript currentTalkScript;
    public Transform talkPosition;
    public Transform defaultPosition;
    public float speed = 5f;

    void Update()
    {
        if (currentTalkScript != null)
        {
            if (currentTalkScript.isTalking)
            {
                MoveCamera(talkPosition.position);
            }
            else
            {
                MoveCamera(defaultPosition.position);
            }
        }
    }

    void MoveCamera(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPos, 
            speed * Time.deltaTime
        );
    }
}

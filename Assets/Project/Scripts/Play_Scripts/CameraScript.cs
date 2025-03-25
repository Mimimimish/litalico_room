using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Player currentTalkScript;
    public Transform originalPosition; // 会話開始前のカメラ位置を保存
    public Vector3 talkPosition;
    public float speed = 5f;
    private bool hasSavedPosition = false; // 位置を保存したかどうか

    void Update()
    {
        if (currentTalkScript != null)
        {
            if (currentTalkScript.isTalking)
            {
                if (!hasSavedPosition)
                {
                    hasSavedPosition = true;
                }
                MoveCamera(talkPosition);
            }
            if (currentTalkScript.isTalking == false)
            {
                if (hasSavedPosition == true)
                {
                    MoveCameraReset(originalPosition.position); // 会話終了時に元の位置へ戻る
                }
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
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MoveCameraReset(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPos, 
            speed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(40f, 0, 0);
    }

    public void SetTalkPosition(Vector3 position)
    {
        talkPosition = position;
    }
}

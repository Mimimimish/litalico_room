using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5f;//カメラ移動速度
    public Player talkScript; // TalkScriptの参照
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (talkScript != null && talkScript.isTalking)
        {
            // isTalkingがtrueなら移動を禁止
            return;
        }

        Move();
    }

    void Move()
{
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    moveDirection = new Vector3(x, 0, z).normalized;
    Vector3 nextPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

    // Raycast で壁を検知（前方1m以内に壁があるかチェック）
    if (!Physics.Raycast(transform.position, moveDirection, 1f))
    {
        transform.position = nextPosition;
    }
}

}

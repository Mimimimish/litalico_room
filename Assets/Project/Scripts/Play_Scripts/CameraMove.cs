using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Player talkScript; // TalkScriptの参照
    public Transform playerTransform; // プレイヤーのTransform
    public float maxDistance = 3f; // カメラとプレイヤーの最大許容距離

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

        // 現在の位置関係を保ったまま新しい位置を計算
        Vector3 desiredPos = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        if (playerTransform != null)
        {
            Vector3 offset = desiredPos - playerTransform.position;
            if (offset.magnitude > maxDistance)
            {
                offset = offset.normalized * maxDistance;
                desiredPos = playerTransform.position + offset;
            }
        }

        transform.position = desiredPos;
    }
}

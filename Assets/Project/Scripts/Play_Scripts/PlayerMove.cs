using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;//移動速度
    public float rotationSpeed = 10f;//キャラクターの回転速度

    public TalkScript talkScript; // TalkScriptの参照

    private Rigidbody rb;//物理エンジンの利用
    private Vector3 moveDirection = Vector3.zero;//moveDirectionがキャラクターの移動方向を保持する変数

    private bool canMove = true; // 移動可能かどうかのフラグ
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;//マウスカーソルの固定
        Cursor.visible = false;//マウスカーソルの非表示
    }

    void Update()
    {
        if (talkScript != null && talkScript.isTalking)
        {
            // isTalkingがtrueなら移動を禁止
            return;
        }
         if (!canMove) return; // ぶつかったら移動を禁止

        Move();
        RotateCharacter();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");//A/D または ←/→
        float z = Input.GetAxis("Vertical");//W/S または ↑/↓ 

        moveDirection = new Vector3(x, 0, z).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotateCharacter()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle")) // Obstacleタグの物にぶつかったら
            {
                canMove = false; // 移動禁止
            }
        }
         void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            canMove = true; // 移動再開
        }
    }
}
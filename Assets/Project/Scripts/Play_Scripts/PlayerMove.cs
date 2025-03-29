using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    public TalkScript talkScript; // TalkScriptの参照

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

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
        RotateCharacter();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

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
}

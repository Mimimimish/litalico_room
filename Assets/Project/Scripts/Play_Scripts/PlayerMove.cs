using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSensitivity = 100f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Look();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(x, 0, z);
    }

    public void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}

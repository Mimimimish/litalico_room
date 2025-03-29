using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    private Vector3 pivotOffset = new Vector3(-1f, 0f, 0f); // 中心軸を(x=-1, y=0, z=0)に設定
    private bool isOpen = false;
    private bool hasOperated = false; // すでに開閉動作が行われたかを判定
    private float rotationAngle = -80f; // Z軸方向に回転
    private float rotationSpeed = 2f; // 回転速度
    private Coroutine currentRotationCoroutine = null; // 現在の回転処理を管理

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen && !hasOperated) // 一度だけ動作する
        {
            hasOperated = true; // 以降トリガーが再び呼ばれないようにする
            if (currentRotationCoroutine != null)
            {
                StopCoroutine(currentRotationCoroutine);
            }
            currentRotationCoroutine = StartCoroutine(RotateDoor(true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen && !hasOperated) // 一度だけ動作する
        {
            hasOperated = true;
            if (currentRotationCoroutine != null)
            {
                StopCoroutine(currentRotationCoroutine);
            }
            currentRotationCoroutine = StartCoroutine(RotateDoor(false));
        }
    }

    IEnumerator RotateDoor(bool open)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0f, 0f, open ? rotationAngle : -rotationAngle);

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            transform.position = transform.position + transform.rotation * pivotOffset - transform.rotation * pivotOffset;
            yield return null;
        }

        isOpen = open;
        currentRotationCoroutine = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Player talkScript; // TalkScriptの参照
    public Transform playerTransform; // プレイヤーのTransform
    public Vector3 offset = new Vector3(0, 5, -10); // 通常カメラオフセット
    public Vector3 altOffset = new Vector3(0, 8, -5); // 代替視点オフセット
    public LayerMask wallLayer; // 壁のレイヤー

    private bool isUsingAltOffset = false; // どちらの視点を使っているか
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
            // isTalkingがtrueなら追跡を停止
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUsingAltOffset = !isUsingAltOffset; // オフセットを切り替え
        }

        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (playerTransform == null)
        {
            return;
        }

        // 使用するオフセットを選択
        Vector3 currentOffset = isUsingAltOffset ? altOffset : offset;

        // プレイヤーの位置に対する理想的なカメラ位置
        Vector3 desiredPos = playerTransform.position + currentOffset;

        // 壁との衝突を確認
        Vector3 direction = desiredPos - playerTransform.position;
        float distance = direction.magnitude;
        RaycastHit hit;

        // 壁に当たる場合は、その壁の手前まで移動
        if (Physics.Raycast(playerTransform.position, direction.normalized, out hit, distance, wallLayer))
        {
            desiredPos = hit.point - direction.normalized * 0.2f; // 壁の手前に配置
        }

        // カメラをスムーズに移動
        transform.position = Vector3.Lerp(transform.position, desiredPos, moveSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Player talkScript; // TalkScriptの参照
    public Transform playerTransform; // プレイヤーのTransform
    public Vector3 offset = new Vector3(0, 5, -10); // カメラとプレイヤーの距離をx, y, zで指定
    public LayerMask wallLayer; // 壁のレイヤー

    private Vector3 moveDirection = Vector3.zero;
    private List<MeshRenderer> transparentWalls = new List<MeshRenderer>();

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

        FollowPlayer();
        CheckWalls();
    }

    void FollowPlayer()
    {
        if (playerTransform == null)
        {
            return;
        }

        // プレイヤーの位置に追従
        Vector3 desiredPos = playerTransform.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, moveSpeed * Time.deltaTime);
    }

    void CheckWalls()
    {
        // 以前透明にした壁を元に戻す
        foreach (MeshRenderer rend in transparentWalls)
        {
            rend.enabled = true;
        }
        transparentWalls.Clear();

        // カメラとプレイヤーの間にある壁を透明にする
        Vector3 direction = playerTransform.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, wallLayer);

        foreach (RaycastHit hit in hits)
        {
            MeshRenderer rend = hit.collider.GetComponent<MeshRenderer>();
            if (rend != null)
            {
                rend.enabled = false; // Mesh Rendererをオフにする
                transparentWalls.Add(rend);
            }
        }
    }
}

using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("現在の会話対象プレイヤー")]
    public Player currentTalkScript;

    [Header("カメラの各ポジション")]
    public Transform defaultPosition;
    public Transform originalPosition;
    public Transform playerTransform;

    [Header("座標オフセット")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public Vector3 altOffset = new Vector3(0, 8, -5);
    public float offsetX = 0f;
    public float offsetY = 3f;
    public float offsetZ = -4f;

    [Header("設定値")]
    public float speed = 5f;
    public LayerMask wallLayer;
    public LayerMask objectMask;

    private bool isUsingAltOffset = false;
    private bool hasSavedPosition = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (currentTalkScript != null && currentTalkScript.isTalking)
        {
            if (!hasSavedPosition && originalPosition != null)
            {
                originalPosition.position = transform.position;
                hasSavedPosition = true;
            }

            Vector3 targetPos = currentTalkScript.transform.position + new Vector3(offsetX, offsetY, offsetZ);
            MoveCamera(targetPos);
            PerformRaycast(currentTalkScript.transform);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isUsingAltOffset = !isUsingAltOffset;
            }

            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (playerTransform == null) return;

        Vector3 currentOffset = isUsingAltOffset ? altOffset : offset;
        Vector3 desiredPos = playerTransform.position + currentOffset;

        Vector3 direction = desiredPos - playerTransform.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(playerTransform.position, direction.normalized, out RaycastHit hit, distance, wallLayer))
        {
            desiredPos = hit.point - direction.normalized * 0.2f;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        transform.LookAt(playerTransform.position);
    }

    void MoveCamera(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MoveCameraReset(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(40f, 0, 0);
    }

    public void SetTalkPosition(Vector3 position)
    {
        Transform tempTransform = new GameObject("TempTalkPosition").transform;
        tempTransform.position = position;
    }

    void PerformRaycast(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude, objectMask))
        {
            MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }

    public bool IsUsingAltOffset()
    {
        return isUsingAltOffset;
    }
}

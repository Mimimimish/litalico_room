using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float angleDegrees = -30f;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    public bool isplayer;
    [SerializeField]
    private TalkChecker talkChecker;
    public bool isTalking { get; set; }
    public CameraScript cameraScript;
    public bool isPlayerMoving { get; private set; }

    public ObjectMove objectmove;

    void Start()
    {
        talkChecker = GetComponent<TalkChecker>();
        isplayer = true;
    }

    void Update()
    {
        if (isplayer)
        {
            PlayerMove();
            RotateCharacter();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (talkChecker.talkableNPC != null)
            {
                DialogueManager_play.instance.StartDialogue(talkChecker.talkableNPC);
                isTalking = true;
                objectmove.CountUp();
            }
        }

        if (isTalking && talkChecker.talkableNPC != null)
        {
            LookAtNPC();
        }
    }

    void PlayerMove()
    {
        isPlayerMoving = false;
        moveDirection = Vector3.zero;

        if (cameraScript != null && cameraScript.GetComponent<CameraMove>().IsUsingAltOffset())
        {
            // altOffset中の操作方法：Wで-X方向へ進む
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += Vector3.left; // -X方向
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection += Vector3.right; // +X方向
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection += Vector3.back; // -Z方向
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += Vector3.forward; // +Z方向
            }

            moveDirection.Normalize();
            isPlayerMoving = moveDirection.magnitude > 0.01f;

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // 向いてる方向を変える
            if (isPlayerMoving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            // 通常の移動処理
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            moveDirection = new Vector3(x, 0, z).normalized;
            isPlayerMoving = moveDirection.magnitude > 0.01f;

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            if (isPlayerMoving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void RotateCharacter()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void LookAtNPC()
    {
        if (talkChecker.talkableNPC == null) return;

        Vector3 direction = (talkChecker.talkableNPC.transform.position - transform.position).normalized;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void EndConversation()
    {
        isTalking = false;

        if (talkChecker != null)
        {
            talkChecker.SetTalkEndFlag(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dao") || other.CompareTag("Ebi") || other.CompareTag("Tomo") || other.CompareTag("Matsu"))
        {
            float angleDeg = angleDegrees;

            Vector3 otherXZ = new Vector3(other.transform.position.x, 0, other.transform.position.z);
            Vector3 thisXZ = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            Vector3 midPosition = Vector3.Lerp(otherXZ, thisXZ, 0.5f);
            Vector3 dir = otherXZ - thisXZ;
            Vector3 perpendicular = new Vector3(-dir.z, 0, dir.x).normalized;
            if (Physics.Raycast(midPosition, perpendicular, 5.5f))
            {
                perpendicular = -perpendicular;
                angleDeg = -angleDeg;
            }
            Quaternion rotation = Quaternion.AngleAxis(angleDeg, Vector3.up);
            Vector3 rotatedDir = rotation * perpendicular;
            Vector3 talkPos = midPosition + rotatedDir * 5.5f;
            talkPos.y = this.transform.position.y;
            Vector3 lookTarget = midPosition;

            cameraScript?.SetTalkPosition(talkPos, lookTarget);
        }
    }
}

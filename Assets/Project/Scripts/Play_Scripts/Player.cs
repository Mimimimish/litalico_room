using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    public bool isplayer;
    [SerializeField]
    private TalkChecker talkChecker;
    public bool isTalking { get; set; }
    public CameraScript cameraScript;
    public bool isPlayerMoving { get; private set; }

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
                DialogueManager.instance.StartDialogue(talkChecker.talkableNPC);
                isTalking = true;
            }
        }

        if (isTalking && talkChecker.talkableNPC != null)
        {
            LookAtNPC();
        }
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = new Vector3(x, 0, z).normalized;
        isPlayerMoving = moveDirection.magnitude > 0.01f;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Vector3 npcPosition = other.transform.position;
            Vector3 talkPos = new Vector3(npcPosition.x, 1f, npcPosition.z - 5f);

            if (cameraScript != null)
            {
                cameraScript.SetTalkPosition(talkPos);
            }
        }
    }
}

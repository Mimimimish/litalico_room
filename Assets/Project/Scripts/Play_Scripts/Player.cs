using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float angleDegrees = -30f;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject GoalObject;

    [SerializeField] public bool isplayer;
    [SerializeField] private TalkChecker talkChecker;
    public bool isTalking { get; set; }
    public CameraScript cameraScript;
    public bool isPlayerMoving { get; private set; }

    public ObjectMove objectmove;

    [Header("フェード処理")]
    public CircleFade circleFade;

    public bool isGoalTriggered { get; set; }
    private bool sceneLoaded = false;

    [Header("アニメーション")]
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        talkChecker = GetComponent<TalkChecker>();
        isplayer = true;
        GoalObject.SetActive(false); 
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
            if (talkChecker.talkableNPC != null && animator != null && !animator.GetBool("isWalking"))
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

        if (circleFade != null && circleFade.fadeOut && isGoalTriggered && !sceneLoaded)
        {
            sceneLoaded = true;
            SceneManager.LoadScene("celebrate");
        }
        if (objectmove.count >=5)
        {
            GoalObject.SetActive(true);
        }
    }

    void PlayerMove()
    {
        isPlayerMoving = false;
        moveDirection = Vector3.zero;

        if (cameraScript != null && cameraScript.GetComponent<CameraMove>().IsUsingAltOffset())
        {
            if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.left;
            if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.right;
            if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.back;
            if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.forward;
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            moveDirection = new Vector3(x, 0, z);
        }

        moveDirection.Normalize();
        isPlayerMoving = moveDirection.magnitude > 0.01f;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (isPlayerMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", isPlayerMoving);
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
        talkChecker?.SetTalkEndFlag();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("🎯 Goalに到達");
            if (objectmove.count >= 5)
            {
                Debug.Log("🎯 カウント5達成");
                if (!isGoalTriggered)
                {
                    isGoalTriggered = true;
                    Debug.Log("🎯 フェードアウト要求");
                }
            }
            return;
        }

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

            cameraScript?.SetTalkPosition(talkPos); // 修正：引数は1つに
        }
    }
}

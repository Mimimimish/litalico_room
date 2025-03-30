using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveRadius = 10f;
    public float stopTime = 2f;
    public LayerMask obstacleLayer;
    public float rayDistance = 2f;
    public bool isMoving { get; set; }

    public Player player;
    public float rotationSpeed = 5f;
    private bool isCoroutineRunning = false;

    public Transform postTalkTarget;
    private bool hasMovedToTarget = false;
    private bool useNavMesh = true;

    private float stuckTimer = 0f;
    private float stuckDuration = 0.1f;
    private Vector3 lastPosition;

    public float fallbackMoveSpeed = 2f;
    public float fallbackStopDistance = 0.2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(RandomMove());
    }

    void Update()
    {
        if (player != null && player.isTalking)
        {
            if (agent.enabled) agent.isStopped = true;
            isMoving = false;
            LookAtPlayer();
        }
        else
        {
            if (!hasMovedToTarget && postTalkTarget != null)
            {
                string npcTag = gameObject.tag;
                TalkChecker checker = player.GetComponent<TalkChecker>();
                if (checker != null && checker.GetTalkEndFlagByTag(npcTag))
                {
                    StopAllCoroutines();

                    // NavMeshAgent を再有効化して目的地をセット
                    if (!agent.enabled)
                    {
                        agent.enabled = true;
                        Debug.Log($"{gameObject.name}：NavMeshAgent を再有効化しました。");
                    }

                    agent.isStopped = false;
                    hasMovedToTarget = true;
                    useNavMesh = true;
                    stuckTimer = 0f;
                    lastPosition = transform.position;

                    Vector3 finalDestination = postTalkTarget.position;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(postTalkTarget.position, out hit, 5f, NavMesh.AllAreas))
                    {
                        finalDestination = hit.position;
                    }
                    agent.SetDestination(finalDestination);
                    isMoving = true;
                }
            }

            if (hasMovedToTarget)
            {
                if (useNavMesh)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    {
                        isMoving = false;
                        hasMovedToTarget = false;
                        agent.isStopped = true;
                        return;
                    }

                    if (Vector3.Distance(transform.position, lastPosition) < 0.01f)
                    {
                        stuckTimer += Time.deltaTime;
                        if (stuckTimer >= stuckDuration)
                        {
                            Debug.Log($"{gameObject.name}：NavMesh から手動移動に切り替え");
                            agent.isStopped = true;
                            agent.enabled = false;  // ★ 無効化
                            useNavMesh = false;
                        }
                    }
                    else
                    {
                        stuckTimer = 0f;
                        lastPosition = transform.position;
                    }
                }
                else
                {
                    MoveToTargetManually();
                }
            }

            if (!hasMovedToTarget)
            {
                CheckObstacle();
            }
        }
    }

    void LookAtPlayer()
    {
        if (player == null || !player.isTalking) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveToTargetManually()
    {
        Vector3 targetPos = postTalkTarget.position;
        targetPos.y = transform.position.y;

        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, fallbackMoveSpeed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPos) < fallbackStopDistance)
        {
            isMoving = false;
            hasMovedToTarget = false;
        }
    }

    IEnumerator RandomMove()
    {
        isCoroutineRunning = true;

        while (!player.isTalking)
        {
            if (!isMoving)
            {
                Vector3 randomDestination = GetRandomPoint(transform.position, moveRadius);
                if (agent.enabled)
                {
                    agent.SetDestination(randomDestination);
                    isMoving = true;
                }
            }

            yield return new WaitUntil(() => agent.remainingDistance < 0.5f);
            isMoving = false;
            yield return new WaitForSeconds(stopTime);
        }

        isCoroutineRunning = false;
    }

    Vector3 GetRandomPoint(Vector3 center, float range)
    {
        Vector3 randomPoint = center + new Vector3(
            Random.Range(-range, range),
            0,
            Random.Range(-range, range)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

    void CheckObstacle()
    {
        if (player != null && player.isTalking) return;

        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        if (Physics.Raycast(ray, rayDistance, obstacleLayer))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        if (player != null && player.isTalking) return;

        Vector3 newDirection = Quaternion.Euler(0, Random.Range(90, 270), 0) * transform.forward;
        Vector3 newDestination = transform.position + newDirection * moveRadius;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, moveRadius, NavMesh.AllAreas))
        {
            if (agent.enabled)
            {
                agent.SetDestination(hit.position);
            }
        }
    }
}

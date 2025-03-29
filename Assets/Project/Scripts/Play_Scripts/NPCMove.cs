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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(RandomMove());
    }

    void Update()
    {
        if (player != null && player.isTalking)
        {
            agent.isStopped = true;
            isMoving = false;
            LookAtPlayer();
        }
        else
        {
            agent.isStopped = false;
            if (!isCoroutineRunning)
            {
                StartCoroutine(RandomMove());
            }
        }

        CheckObstacle();
    }

    void LookAtPlayer()
    {
        if (player == null || !player.isTalking) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator RandomMove()
    {
        isCoroutineRunning = true;

        while (!player.isTalking)
        {
            if (!isMoving)
            {
                Vector3 randomDestination = GetRandomPoint(transform.position, moveRadius);
                agent.SetDestination(randomDestination);
                isMoving = true;
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
            agent.SetDestination(hit.position);
        }
    }
}

using System.Collections;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    public float targetY = 1.02f; // Target Y position
    public float speed = 1.5f;    // Speed of movement

    void Start()
    {
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        transform.position = targetPosition; // Ensure it reaches the exact target position
    }
}

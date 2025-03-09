using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Nec_Move : MonoBehaviour
{
    public Player player;
    public float rotationSpeed = 5f;
    public float viewAngle = 80f;

    private Transform npcParent;

    void Start()
    {
        npcParent = transform.parent;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && npcParent != null)
        {
            Vector3 direction = (player.transform.position - npcParent.position).normalized;
            direction.y = 0;

            float angle = Vector3.Angle(Vector3.forward, direction);

            if (angle <= viewAngle / 2)
            {
                RotateTowardsPlayer(direction);
            }
        }
    }

    void RotateTowardsPlayer(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        npcParent.rotation = Quaternion.Slerp(npcParent.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

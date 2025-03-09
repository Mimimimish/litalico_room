using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public Animator animatorTalking;
    public Player player;

    void Start()
    {
        animator = GetComponent<Animator>();
        animatorTalking = GetComponent<Animator>();
        player = GetComponent<Player>(); 
    }

    void Update()
    {
        if (player.isPlayerMoving)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (player.isTalking)
        {
            animatorTalking.SetBool("Talking", true);
        }
        else
        {
            animatorTalking.SetBool("Talking", false);
        }
    }
}

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
            animator.SetBool("isWalking", true);
        }
        else if (player.isTalking)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
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

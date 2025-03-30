using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public Animator animatorWalking; // Walkingアニメーション用
    public Animator animatorTalking; // Talkingアニメーション用
    public NPCMove npcMove;
    public Player player;

    void Start()
    {
        npcMove = GetComponent<NPCMove>();
    }

    void Update()
    {
        if (player != null && player.isTalking)
        {
            // 会話中
            animatorWalking.SetBool("isWalking", false);
            animatorTalking.SetBool("Talking", true);
        }
        else
        {
            animatorTalking.SetBool("Talking", false);

            // 移動中ならWalkingをtrueに、それ以外はfalse
            animatorWalking.SetBool("isWalking", npcMove.isMoving);
        }
    }
}

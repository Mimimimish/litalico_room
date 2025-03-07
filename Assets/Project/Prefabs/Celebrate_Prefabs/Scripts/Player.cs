using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    public bool isplayer;
    [SerializeField]
    private TalkChecker talkChecker;

    void Start()
    {
        talkChecker = GetComponent<TalkChecker>();
    }
    void Update()
    {
        if(isplayer == true)
        {
            PLayerMove();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(talkChecker.talkableNPC != null)
            {
                DialogueManager.instance.StartDialogue(talkChecker.talkableNPC);
            }
        }
    }
    void PLayerMove()
    {
        float moveX = Input.GetAxis("Horizontal"); // 左右の入力 (← →)
        float moveZ = Input.GetAxis("Vertical");   // 前後の入力 (↑ ↓)
        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World); // 世界座標基準で移動
    }
}

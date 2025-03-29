using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkChecker : MonoBehaviour
{
    [Header("【現在Talk可能なNPC一覧】")]
    public NPC talkableNPC;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Dao") || col.CompareTag("Ebi") || col.CompareTag("Tomo") || col.CompareTag("Matsu"))
        {
            NPC target = col.gameObject.GetComponent<NPC>();
            if (target != null)
            {
                talkableNPC = target;
                target.talkIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Dao") || col.CompareTag("Ebi") || col.CompareTag("Tomo") || col.CompareTag("Matsu"))
        {
            NPC target = col.gameObject.GetComponent<NPC>();
            if (target != null)
            {
                target.talkIcon.SetActive(false);
                if (talkableNPC == target)
                {
                    talkableNPC = null;
                }
            }
        }
    }
}

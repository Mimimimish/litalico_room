using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkChecker : MonoBehaviour
{
    [Header("【現在Talk可能なNPC一覧】")]
    public NPC talkableNPC;

    // 会話終了後のフラグ（各タグごと）
    public bool daoTalkEnded = false;
    public bool ebiTalkEnded = false;
    public bool tomoTalkEnded = false;
    public bool matsuTalkEnded = false;

    private string lastTag = "";

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Dao") || col.CompareTag("Ebi") || col.CompareTag("Tomo") || col.CompareTag("Matsu"))
        {
            NPC target = col.gameObject.GetComponent<NPC>();
            if (target != null)
            {
                talkableNPC = target;
                target.talkIcon.SetActive(true);
                lastTag = col.tag; // 最後に話していたNPCのタグを保存
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Dao") || col.CompareTag("Ebi") || col.CompareTag("Tomo") || col.CompareTag("Matsu"))
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

    public void SetTalkEndFlag()
    {
        switch (lastTag)
        {
            case "Dao":
                daoTalkEnded = true;
                break;
            case "Ebi":
                ebiTalkEnded = true;
                break;
            case "Tomo":
                tomoTalkEnded = true;
                break;
            case "Matsu":
                matsuTalkEnded = true;
                break;
        }
    }

    public bool GetTalkEndFlagByTag(string tag)
    {
        switch (tag)
        {
            case "Dao":
                return daoTalkEnded;
            case "Ebi":
                return ebiTalkEnded;
            case "Tomo":
                return tomoTalkEnded;
            case "Matsu":
                return matsuTalkEnded;
            default:
                return false;
        }
    }
}

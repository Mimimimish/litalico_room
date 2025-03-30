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
    public bool ponyoTalkEnded = false;

    public string lastTag = "";

    private void OnTriggerEnter(Collider col)
    {
        string tag = col.tag;

        if (IsTalkableTag(tag))
        {
            // 解禁されているかを確認
            if (IsTalkAllowed(tag))
            {
                NPC target = col.gameObject.GetComponent<NPC>();
                if (target != null)
                {
                    talkableNPC = target;
                    target.talkIcon.SetActive(true);
                    lastTag = tag;
                }
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        string tag = col.tag;

        if (IsTalkableTag(tag))
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

    // 解禁フラグを立てる関数
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
            case "Ponyo":
                ponyoTalkEnded = true;
                break;
        }
    }

    // 現在のタグが話しかけ対象か確認
    private bool IsTalkableTag(string tag)
    {
        return tag == "Dao" || tag == "Ebi" || tag == "Tomo" || tag == "Matsu" || tag == "Ponyo";
    }

    // 指定タグが話しかけ可能かどうかを判定
    public bool IsTalkAllowed(string tag)
    {
        switch (tag)
        {
            case "Ponyo":
                return true;
            case "Dao":
                return ponyoTalkEnded;
            case "Ebi":
                return daoTalkEnded;
            case "Matsu":
                return ebiTalkEnded;
            case "Tomo":
                return matsuTalkEnded;
            default:
                return false;
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
            case "Ponyo":
                return ponyoTalkEnded;
            default:
                return false;
        }
    }
}

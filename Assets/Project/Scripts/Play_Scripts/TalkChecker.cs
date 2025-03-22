using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkChecker : MonoBehaviour
{
    [Header("【現在Talk可能なNPC一覧】")]
    public NPC talkableNPC;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("NPC"))
        {
            NPC target = col.gameObject.GetComponent<NPC>();
            talkableNPC = target;
            target.talkIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("NPC"))
        {
            NPC target = col.gameObject.GetComponent<NPC>();
            target.talkIcon.SetActive(false);
            if (talkableNPC == target)
            {
                talkableNPC = null;
            }
        }
    }
}



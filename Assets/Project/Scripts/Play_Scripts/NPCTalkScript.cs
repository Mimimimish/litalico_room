using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCTalkScript : MonoBehaviour
{
    public TalkScript TalkEvent;
    public TextMeshProUGUI TalkStartText;
    public GameObject TalkPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TalkEvent != null && TalkStartText != null && TalkPanel != null)
        {
            if (TalkEvent.isInsideCollider)
            {
                TalkStartText.gameObject.SetActive(true);
                TalkPanel.SetActive(true);
            }
            else
            {
                TalkStartText.gameObject.SetActive(false);
                TalkPanel.SetActive(false);
            }
            if (TalkEvent.isTalking)
            {
                TalkStartText.gameObject.SetActive(false);
                TalkPanel.SetActive(false);
            }
        }
    }
}

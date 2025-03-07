using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject UI_Dialogue;
    public TextMeshProUGUI dialogName;
    public TextMeshProUGUI dialogText;
    public Player player;
    public string talkerName;
    // キュー：
    Queue<string> sentences = new Queue<string>();
    void Awake()
    {
        talkerName = null;
        UI_Dialogue.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartDialogue(NPC npc)
    {
        player.isplayer = false;
        sentences.Clear();
        talkerName = npc.dialogData.TalkerName;
        foreach(string sentence in npc.dialogData.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        UI_Dialogue.SetActive(true);
        DisplaySentence();
    }
    public void DisplaySentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogName.text = talkerName;
        dialogText.text = sentence;
    }
    public void EndDialogue()
    {
        UI_Dialogue.SetActive(false);
        player.isplayer = true;
    }

    public void OnClick()
    {
        DisplaySentence();
    }
}

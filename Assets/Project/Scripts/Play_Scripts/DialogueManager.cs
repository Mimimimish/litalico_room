using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject UI_Dialogue;
    public GameObject UI_Dialogue2;
    public TextMeshProUGUI dialogName;
    public TextMeshProUGUI dialogText;
    public Player player;
    public float typingSpeed = 0.05f;
    public float autoNextDelay = 2f;
    public string talkerName;

    private Queue<string> sentences = new Queue<string>();
    private Coroutine typingCoroutine;
    private bool useSecondUI = false;

    void Awake()
    {
        talkerName = null;
        UI_Dialogue.SetActive(false);
        UI_Dialogue2.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(NPC npc, bool useSecondUI = false)
    {
        player.isplayer = false;
        player.isTalking = true;
        sentences.Clear();
        talkerName = npc.dialogData.TalkerName;
        this.useSecondUI = useSecondUI;

        foreach (string sentence in npc.dialogData.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (useSecondUI)
        {
            UI_Dialogue2.SetActive(true);
            DisplaySentence();
        }
        else
        {
            UI_Dialogue.SetActive(true);
            UI_Dialogue2.SetActive(true);
            DisplaySentence();
        }
    }

    public void DisplaySentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
            dialogName.text = talkerName;
            dialogText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        
        yield return new WaitForSeconds(autoNextDelay);
        DisplaySentence();
    }

    public void EndDialogue()
    {
        UI_Dialogue.SetActive(false);
        UI_Dialogue2.SetActive(false);
        player.isplayer = true;
        player.EndConversation();
    }

    public void OnClick()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = sentences.Count > 0 ? sentences.Peek() : "";
            typingCoroutine = null;
        }
        else
        {
            DisplaySentence();
        }
    }
}

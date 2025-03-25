using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkScript : MonoBehaviour
{
    public Collider TalkCollider;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    
    private string[] conversationLines =
    {
        "Hello there!",
        "How are you doing?",
        "I'm doing great, thanks for asking!",
        "I hope you're having a good day!",
        "Goodbye!"
    };

    public float textSpeed = 0.05f; 
    public float textDisplayInterval = 2.0f; 

    public bool isTalking { get; private set; }

    public bool isInsideCollider { get; private set; }
    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        if (dialogueText != null)
        {
            dialogueText.text = "";
            dialogueText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isInsideCollider && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartCoroutine(StartConversation());
        }
        else if (isTalking && isTyping && Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            dialogueText.text = conversationLines[currentLine];
            isTyping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collider");
        if (other.CompareTag("NPC"))
        {
            isInsideCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isInsideCollider = false;
        }
    }

    IEnumerator StartConversation()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(true); 
            dialogueText.text = "";
        }

        isTalking = true;
        currentLine = 0;

        while (currentLine < conversationLines.Length)
        {
            yield return StartCoroutine(TypeDialogue(conversationLines[currentLine]));
            yield return new WaitForSeconds(textDisplayInterval);
            currentLine++;
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        if (dialogueText != null)
        {
            dialogueText.text = "";
            dialogueText.gameObject.SetActive(false); 
        }

        isTalking = false;
    }

    IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }
}

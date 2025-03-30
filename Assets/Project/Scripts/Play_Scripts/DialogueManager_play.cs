using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_play : MonoBehaviour
{
    public static DialogueManager_play instance;

    [Header("【UI】")]
    public GameObject UI_Dialogue;
    public GameObject UI_Dialogue2;
    public TextMeshProUGUI dialogName;
    public TextMeshProUGUI dialogText;

    [Header("【プレイヤー制御】")]
    public Player player;

    [Header("【タイピング設定】")]
    public float typingSpeed = 0.05f;
    public float autoNextDelay = 2f;

    [Header("【音声設定】")]
    public AudioSource voiceSource;
    public AudioClip voiceClip;
    public AudioClip BGMClip;

    [Header("【フェード制御】")]
    public CircleFade circleFade;

    private Queue<string> sentences = new Queue<string>();
    private Coroutine typingCoroutine;
    private string talkerName;
    private bool useSecondUI = false;
    private bool talkFinished = false;

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

        StartCoroutine(WaitForFadeIn());
    }

    IEnumerator WaitForFadeIn()
    {
        yield return new WaitUntil(() => circleFade != null && circleFade.fadeIn);

        // ✅ フェードイン後に BGM 再生
        if (BGMClip != null)
        {
            voiceSource.clip = BGMClip;
            voiceSource.loop = true;
            voiceSource.Play();
        }
    }

    public void StartDialogue(NPC npc, bool useSecondUI = false)
    {
        player.isplayer = false;
        player.isTalking = true;
        talkFinished = false;

        sentences.Clear();
        talkerName = npc.dialogData.TalkerName;
        this.useSecondUI = useSecondUI;

        foreach (string sentence in npc.dialogData.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        // ✅ BGM を止める
        if (voiceSource.isPlaying && voiceSource.clip == BGMClip)
        {
            voiceSource.Stop();
        }

        if (useSecondUI)
        {
            UI_Dialogue2.SetActive(true);
        }
        else
        {
            UI_Dialogue.SetActive(true);
            UI_Dialogue2.SetActive(true);
        }

        DisplaySentence();
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
            voiceSource.Stop();
        }

        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogName.text = talkerName;
        dialogText.text = "";

        // ✅ 会話音を再生（voiceClip）
        if (voiceClip != null)
        {
            voiceSource.clip = voiceClip;
            voiceSource.loop = true;
            voiceSource.Play();
        }

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        voiceSource.Stop();
        typingCoroutine = null;

        yield return new WaitForSeconds(autoNextDelay);
        DisplaySentence();
    }

    public void EndDialogue()
    {
        if (talkFinished) return;
        talkFinished = true;

        UI_Dialogue.SetActive(false);
        UI_Dialogue2.SetActive(false);
        player.isplayer = true;
        player.EndConversation();

        voiceSource.Stop();

        // ✅ BGMを再開
        StartCoroutine(PlayBGM());
    }

    IEnumerator PlayBGM()
    {
        yield return new WaitForSeconds(0.05f);

        if (voiceSource.clip == BGMClip && voiceSource.isPlaying) yield break;

        voiceSource.loop = true;
        voiceSource.clip = BGMClip;
        voiceSource.Play();
    }

    public void OnClick()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = sentences.Count > 0 ? sentences.Peek() : "";
            voiceSource.Stop();
            typingCoroutine = null;
        }
        else
        {
            DisplaySentence();
        }
    }
}

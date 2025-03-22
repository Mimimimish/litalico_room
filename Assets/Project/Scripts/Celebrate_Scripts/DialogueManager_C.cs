using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_C : MonoBehaviour
{
    public static DialogueManager_C instance;
    public bool talk_finish_C = false;
    
    [SerializeField, Header("【別スクリプトの参照】")]
    public CircleFade circleFade;
    
    [SerializeField, Header("【会話のUI】")]
    public GameObject UI_Dialogue_C;
    public GameObject Next_bottun_C;
    public TextMeshProUGUI dialogName_C;
    public TextMeshProUGUI dialogText_C;

    [SerializeField, Header("【会話情報データ】")]
    public DialogData dialogData_C;
    public string talkerName_C;

    [SerializeField, Header("【会話音の設定】")]
    public AudioSource voiceSource_C;  // 会話音の AudioSource
    public AudioClip voiceClip_C;      // 再生する音声クリップ

    [SerializeField, Header("[BGM]")]
    public AudioClip BGMClip_C;      // BGM音声クリップ
    // キュー：
    Queue<string> sentences_C = new Queue<string>();
    private Coroutine typingCoroutine_C; // 現在のタイピングコルーチンを管理

    void Awake()
    {
        talkerName_C = null;
        UI_Dialogue_C.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(WaitForCondition());
    }

    void Update()
    {
        // Nextボタンがアクティブのときに左クリックしたら次の文章を表示
        if (Next_bottun_C.activeSelf && Input.GetMouseButtonDown(0))
        {
            DisplaySentence();
        }
    }

    IEnumerator WaitForCondition()
    {
        yield return new WaitUntil(() => circleFade.fadeIn == false);
        StartDialogue(); // 条件が満たされたら実行
    }

    public void StartDialogue()
    {
        sentences_C.Clear();
        talkerName_C = dialogData_C.TalkerName;
        foreach (string sentence in dialogData_C.Sentences)
        {
            sentences_C.Enqueue(sentence);
        }
        UI_Dialogue_C.SetActive(true);
        DisplaySentence();
    }

    public void DisplaySentence()
    {
        Next_bottun_C.SetActive(false);
        if (sentences_C.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences_C.Dequeue();
        dialogName_C.text = talkerName_C;

        // もし既にコルーチンが動いていたら停止する
        if (typingCoroutine_C != null)
        {
            StopCoroutine(typingCoroutine_C);
            voiceSource_C.Stop(); // 音も止める
        }

        // タイピングエフェクトを開始
        typingCoroutine_C = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText_C.text = ""; // テキストをリセット

        // 会話音を再生（ループON）
        if (voiceClip_C != null)
        {
            voiceSource_C.clip = voiceClip_C;
            voiceSource_C.loop = true; // ループ再生
            voiceSource_C.Play();
        }

        foreach (char letter in sentence.ToCharArray()) // 1文字ずつ表示
        {
            dialogText_C.text += letter;
            yield return new WaitForSeconds(0.10f); // 文字が表示される間隔
        }
        Next_bottun_C.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        // 文章の表示が終わったら音を止める
        voiceSource_C.Stop();
        typingCoroutine_C = null; // コルーチン終了
    }

    public void EndDialogue()
    {
        UI_Dialogue_C.SetActive(false);
        voiceSource_C.Stop(); // 会話終了時に音も止める
        talk_finish_C = true;
        //StartCoroutine(BGMStart());
    }
    IEnumerator BGMStart()
    {
        yield return new WaitForSeconds(0.05f);
        voiceSource_C.clip = BGMClip_C;
        voiceSource_C.loop = true; // ループ再生
        voiceSource_C.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_C : MonoBehaviour
{
    public static DialogueManager_C instance;
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
    
    // キュー：
    Queue<string> sentences_C = new Queue<string>();
    private Coroutine typingCoroutine; // 現在のタイピングコルーチンを管理

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
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // タイピングエフェクトを開始
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText_C.text = ""; // テキストをリセット

        foreach (char letter in sentence.ToCharArray()) // 1文字ずつ表示
        {
            dialogText_C.text += letter;
            yield return new WaitForSeconds(0.10f); // 文字が表示される間隔（0.05秒）
        }
        Next_bottun_C.SetActive(true);
        typingCoroutine = null; // コルーチン終了
    }

    public void EndDialogue()
    {
        UI_Dialogue_C.SetActive(false);
    }
}

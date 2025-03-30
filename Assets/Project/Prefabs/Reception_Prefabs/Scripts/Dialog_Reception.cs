using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // ← 追加：シーン遷移のため

public class Dialog_Reception : MonoBehaviour
{
    public static Dialog_Reception instance;
    public bool talk_finish_C = false;

    [SerializeField, Header("【別スクリプトの参照】")]
    public CircleFade_Reception circleFade_reception;

    [SerializeField, Header("【会話のUI】")]
    public GameObject UI_Dialogue_C;
    public GameObject Next_bottun_C;
    public TextMeshProUGUI dialogName_C;
    public TextMeshProUGUI dialogText_C;

    [SerializeField, Header("【会話情報データ】")]
    public DialogData dialogData_C;
    public string talkerName_C;

    [SerializeField, Header("【会話音の設定】")]
    public AudioSource voiceSource_C;
    public AudioClip voiceClip_C;

    [SerializeField, Header("[BGM]")]
    public AudioSource BGMSource_C;
    public AudioClip BGMClip_C;

    Queue<string> sentences_C = new Queue<string>();
    private Coroutine typingCoroutine_C;

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
        if (Next_bottun_C.activeSelf && Input.GetMouseButtonDown(0))
        {
            DisplaySentence();
        }
    }

    IEnumerator WaitForCondition()
    {
        yield return new WaitUntil(() => circleFade_reception.fadeIn == true);
        BGMSource_C.clip = BGMClip_C;
        BGMSource_C.Play();
        yield return new WaitForSeconds(0.8f);
        StartDialogue();
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

        if (typingCoroutine_C != null)
        {
            StopCoroutine(typingCoroutine_C);
            voiceSource_C.Stop();
        }

        typingCoroutine_C = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText_C.text = "";

        if (voiceClip_C != null)
        {
            voiceSource_C.clip = voiceClip_C;
            voiceSource_C.loop = true;
            voiceSource_C.Play();
        }

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText_C.text += letter;
            yield return new WaitForSeconds(0.10f);
        }

        Next_bottun_C.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        voiceSource_C.Stop();
        typingCoroutine_C = null;
    }

    public void EndDialogue()
    {
        if (talk_finish_C) return;

        talk_finish_C = true;
        UI_Dialogue_C.SetActive(false);
        voiceSource_C.Stop();
        StartCoroutine(FadeOutAndStop(BGMSource_C, 4.0f));
        StartCoroutine(LoadNextSceneAfterDelay(4.0f)); // ← 追加：フェード後にシーン遷移
    }

    IEnumerator FadeOutAndStop(AudioSource BGMSource, float duration)
    {
        float startVolume = BGMSource.volume;

        while (BGMSource.volume > 0)
        {
            BGMSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        BGMSource.Stop();
        BGMSource.volume = startVolume;
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Play"); // ← ここで"Play"シーンをロード
    }

    IEnumerator BGMStart()
    {
        yield return new WaitForSeconds(0.05f);

        if (voiceSource_C.clip == BGMClip_C && voiceSource_C.isPlaying) yield break;
        voiceSource_C.loop = false;
        voiceSource_C.clip = BGMClip_C;
        voiceSource_C.Play();
    }
}

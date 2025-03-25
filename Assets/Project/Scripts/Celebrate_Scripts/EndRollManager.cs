using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndRollManager : MonoBehaviour
{
    [SerializeField, Header("【別スクリプトの参照】")]
    public CircleFade circleFade;

    [SerializeField, Header("【EndrollのUI】")]
    public GameObject blackBack;
    public TextMeshProUGUI endrollPrefab; // クローンの元となるテキストUI
    public Transform spawnPoint;  // テキストが生成される位置
    public Transform deletePoint; // テキストが削除される位置
    public Transform centerPoint; // 「おめでとう！！」の最終位置

    [SerializeField, Header("【Endrollデータ】")]
    public EndrollData endrollData; // ScriptableObjectで管理するデータ

    [SerializeField, Header("【スクロール設定】")]
    public float scrollSpeed = 50f; // テキストが上に流れる速度
    public float spawnInterval = 3.0f; // 文字を出す間隔（秒）

    void Start()
    {
        blackBack.SetActive(false);
        StartCoroutine(Endroll());
    }

    IEnumerator Endroll()
    {
        yield return new WaitUntil(() => circleFade.fadeOut == true);
        blackBack.SetActive(true);

        foreach (string text in endrollData.endrollTexts)
        {
            CreateEndrollText(text);
            yield return new WaitForSeconds(spawnInterval);
        }

        // すべてのエンドロールが終わったら10秒待機
        yield return new WaitForSeconds(10f);
        
        // 最後のメッセージを表示
        ShowFinalMessage("卒業おめでとう！！");
    }

    void CreateEndrollText(string textContent)
    {
        // クローンを作成し、親をblackBackに設定
        TextMeshProUGUI newText = Instantiate(endrollPrefab, spawnPoint.position, Quaternion.identity, blackBack.transform);
        newText.text = textContent; // テキスト内容を設定
        StartCoroutine(MoveAndDestroy(newText));
    }

    IEnumerator MoveAndDestroy(TextMeshProUGUI text)
    {
        while (text.transform.position.y < deletePoint.position.y)
        {
            text.transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(text.gameObject); // 画面上部を超えたら削除
    }

    void ShowFinalMessage(string message)
    {
        TextMeshProUGUI finalText = Instantiate(endrollPrefab, spawnPoint.position, Quaternion.identity, blackBack.transform);
        finalText.text = message;
        finalText.fontSize = 80; // 大きめの文字サイズに設定
        finalText.alignment = TextAlignmentOptions.Center;

        StartCoroutine(MoveToCenter(finalText));
    }

    IEnumerator MoveToCenter(TextMeshProUGUI text)
    {
        while (text.transform.position.y < centerPoint.position.y)
        {
            text.transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // ぴったり中央で固定
        text.transform.position = centerPoint.position;
    }
}

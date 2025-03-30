using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraMove : MonoBehaviour
{
    private List<GameObject> targets = new List<GameObject>();
    private int currentTargetIndex = 0;

    void Start()
    {
        // ターゲットをリストに追加
        targets.Add(GameObject.Find("dao"));
        targets.Add(GameObject.Find("ebi"));
        targets.Add(GameObject.Find("matsu"));
        targets.Add(GameObject.Find("tomo"));
        targets.Add(GameObject.Find("lily"));

        // 初期ターゲットを設定
        if (targets.Count > 0)
        {
            currentTargetIndex = Random.Range(0, targets.Count);
        }
    }

    void Update()
    {
        // ボタン入力でターゲットを切り替え (例: キーボードのスペースキー)
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
        }

        // 現在のターゲットに基づいてカメラ位置を更新
        if (targets.Count > 0 && targets[currentTargetIndex] != null)
        {
            Vector3 pos = targets[currentTargetIndex].transform.position;
            transform.position = new Vector3(pos.x, pos.y + 10.5f, pos.z + 12.5f);
        }
    }
}

using UnityEngine;

public class SpeechBubbleAnimation : MonoBehaviour
{
    private Vector3 originalScale; // 元のサイズを保持

    void Start()
    {
        originalScale = transform.localScale; // 現在のサイズを保存
        transform.localScale = Vector3.zero; // 最初は小さく

        // LeanTween で拡大、最大サイズを元のサイズに設定
        LeanTween.scale(gameObject, originalScale, 0.5f).setEase(LeanTweenType.easeOutBack);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFader : MonoBehaviour
{
    public Image image;            // フェードさせたいImage
    public float duration = 2f;    // フェードインの時間（秒）

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            image.color = color;
            yield return null;
        }
    }
}

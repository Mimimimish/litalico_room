using UnityEngine;
using UnityEngine.UI;

public class LogoDisplay : MonoBehaviour
{
    public Image image;
    public float fadeSpeed = 0.5f;  // フェードスピード
    private Material imageMaterial;
    private float fadeAmount = 0;  // フェード進行度
    private float timer = 0f;  // 経過時間を記録するタイマー
    private bool fadeStarted = false;  // フェードが開始されたかどうか

    void Start()
    {
        imageMaterial = new Material(image.material);
        image.material = imageMaterial; 

        imageMaterial.SetFloat("_FadeAmount", 0f);
    }

    void Update()
    {
        if (timer < 2f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!fadeStarted)
            {
                fadeStarted = true;
            }

            if (fadeStarted && fadeAmount < 1)
            {
                fadeAmount += fadeSpeed * Time.deltaTime;
                fadeAmount = Mathf.Clamp01(fadeAmount);
                imageMaterial.SetFloat("_FadeAmount", fadeAmount);
            }
        }
    }
}

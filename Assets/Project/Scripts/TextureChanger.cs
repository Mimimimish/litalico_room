using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material targetMaterial; // Albedoを変更するマテリアル
    public Texture blinkTexture;    // Blink用のテクスチャ
    public Texture talkTexture;     // Talk用のテクスチャ
    public Texture smileTexture;     // Talk用のテクスチャ
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (targetMaterial == null)
        {
            Debug.LogError("targetMaterial が設定されていません！");
        }
        else
        {
            Debug.Log("targetMaterial: " + targetMaterial.name);
        }

        targetMaterial.mainTexture = blinkTexture; // 初期テクスチャ
    }


    // Animation Event で呼び出す
    public void ChangeToBlinkTexture()
    {
        targetMaterial.mainTexture = blinkTexture;
        Debug.Log("blink");
        animator.Play("blink"); // blinkアニメーションを再生
    }
    public void ChangeToTalkTexture()
    {
        targetMaterial.mainTexture = talkTexture;
        Debug.Log("talk");
        animator.Play("talk"); // talkアニメーションを再生
    }
    public void ChangeToSmileTexture()
    {
        Debug.Log("smile");
        targetMaterial.mainTexture = smileTexture;
        Debug.Log("Current Texture: " + targetMaterial.mainTexture.name);
        animator.Play("smile"); // smileアニメーションを再生
    }

}

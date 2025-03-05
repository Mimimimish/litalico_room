using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material targetMaterial; // Albedoを変更するマテリアル
    public Texture blinkTexture;    // Blink用のテクスチャ
    public Texture talkTexture;     // Talk用のテクスチャ
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetMaterial.mainTexture = blinkTexture; // 初期テクスチャ
    }

    // Animation Event で呼び出す
    public void ChangeToTalkTexture()
    {
        targetMaterial.mainTexture = talkTexture;
        animator.Play("talk"); // talkアニメーションを再生
    }
}

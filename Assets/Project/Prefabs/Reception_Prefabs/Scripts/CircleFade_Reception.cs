using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFade_Reception : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeDuration = 2.0f;
    public bool fadeIn = false;
    public bool fadeOut = false;
    [SerializeField, Header("【別スクリプトの参照】")]
    public Dialog_Reception dialog_Reception;

    private void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            float radius = Mathf.Lerp(0, 0.8f, time / fadeDuration);
            fadeMaterial.SetFloat("_Radius", radius);
            time += Time.deltaTime;
            yield return null;
        }
        fadeMaterial.SetFloat("_Radius", 0.8f);
        fadeIn = true;
    }
    IEnumerator FadeOut()
    {
        yield return new WaitUntil(() => dialog_Reception.talk_finish_C == true);
        float time = 0;
        while (time < fadeDuration)
        {
            float radius = Mathf.Lerp(1.0f, 0f, time / fadeDuration);
            fadeMaterial.SetFloat("_Radius", radius);
            time += Time.deltaTime;
            yield return null;
        }
        fadeMaterial.SetFloat("_Radius", 0f);
        fadeOut = true;
    }
}


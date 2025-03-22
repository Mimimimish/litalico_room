using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFade : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeDuration = 2.0f;
    public bool fadeIn = true;
    public DialogueManager_C dialogueManager_C;

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
            float radius = Mathf.Lerp(0, 1f, time / fadeDuration);
            fadeMaterial.SetFloat("_Radius", radius);
            time += Time.deltaTime;
            yield return null;
        }
        fadeMaterial.SetFloat("_Radius", 1f);
        fadeIn = false;
    }
    IEnumerator FadeOut()
    {
        yield return new WaitUntil(() => dialogueManager_C.talk_finish_C == true);
        float time = 0;
        while (time < fadeDuration)
        {
            float radius = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadeMaterial.SetFloat("_Radius", radius);
            time += Time.deltaTime;
            yield return null;
        }
        fadeMaterial.SetFloat("_Radius", 0f);
    }
}

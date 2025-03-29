using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCircleFade : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeDuration = 2.0f;
    public bool fadeIn = true;

    private bool isFadingOut = false;

    private void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(WaitForStartInput());
    }

    IEnumerator WaitForStartInput()
    {
        yield return new WaitForSeconds(5);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isFadingOut)
            {
                StartCoroutine(FadeOut());
                isFadingOut = true;
                break;
            }
            yield return null;
        }
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
